using Flashcards.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Flashcards.DataTyps
{
    public class Card
    {
        public string Id;
        public string BoundArticle ="";

        private string _question ="";
        public string Question
        {
            get
            {
                return _question.Replace(@"&//60", "<").Replace("&//62", ">");
            }
            set
            {
                _question = value.Replace("<", @"&//60").Replace(">", @"&//62");
            }
        }

        private string _answer ="";
        public string Answer
        {
            get
            {
                return _answer.Replace(@"&//60", "<").Replace("&//62", ">");
            }
            set
            {
                _answer = value.Replace("<", @"&//60").Replace(">", @"&//62");
            }
        }

        public string ImageForFront ="";
        public string ImageForBack="";

        //Gradings
        public byte Grading { get; set; }
        public int CardValue { get; set; }
        public int Counter { get; set; }

        public bool CreateCard(string subjectId)
        {
            var file = GetCategoryFile(subjectId);
            if(file != null)
            {
                this.Id = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                this.Grading = (byte)EnumCardGrading.Unused;
                this.Counter = 0;
                this.CardValue = 0;

                bool used = CheckGeneratedId(this.Id, file);
                //prüfen ob Id schon vergeben ist
                if (!used)
                {
                    SaveImages(DataManagement.RootDirectory + "\\" + subjectId);
                    CardToXML(file);
                    return true;
                }
                CreateCard(subjectId);                
            }
            return false;
        }

        private void CardToXML(string file)
        {
            XElement xml = new XElement("Card", new XAttribute("Key", Id), new XAttribute("Grading", Grading.ToString()), new XAttribute("CardValue", CardValue.ToString()), new XAttribute("Counter", Counter.ToString()), new XAttribute("Article",BoundArticle),
                new XElement("Question", _question, new XAttribute("Image", ImageForFront)), new XElement("Answer", _answer, new XAttribute("Image", ImageForBack)));
            
            if (File.Exists(file))
            {
                XDocument doc = XDocument.Load(file);
                doc.Elements("Cards").First().Add(xml);
                doc.Save(file);
            }
        }

        private void SaveImages(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory))
            {
                if (!String.IsNullOrWhiteSpace(ImageForFront) && !ImageForFront.Contains("_c0."))
                {
                    var imageFormat = ImageForFront.Count(x => x == '.');
                    string path = targetDirectory + "\\" + Id + "_c0." + ImageForFront.Split('.')[imageFormat];
                    ImageHandling.ImageToStock(ImageForFront, path);
                    ImageForFront = path;
                }
                if (!String.IsNullOrWhiteSpace(ImageForBack) && !ImageForBack.Contains("_c1."))
                {
                    var imageFormat = ImageForBack.Count(x => x == '.');
                    string path = targetDirectory + "\\" + Id + "_c1." + ImageForBack.Split('.')[imageFormat];
                    ImageHandling.ImageToStock(ImageForBack, path);
                    ImageForBack = path;
                }
            }
        }

        private string GetCategoryFile(string subjectId)
        {
            string dir = DataManagement.RootDirectory + "\\" + subjectId + "\\cards.xml";
            if (File.Exists(dir))
            {
                return dir;
            }
            return null;
        }

        private bool CheckGeneratedId(string id, string file)
        {
            XDocument document = XDocument.Load(file);
            try
            {
                var elements = document.Element("Cards").Elements("Card").ToList(); //Key
                var element = elements.Where(x => x.Attribute("Key").Value == id).SingleOrDefault();
                if (element == null)
                    return false;
            }
            catch { }

            return true;
        }

        public static List<Card> GetCards(string categoryId)
        {
            List<Card> items = new List<Card>();
            var path = DataManagement.RootDirectory + "\\" + categoryId + "\\cards.xml";
            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                var inDocument = doc.Element("Cards").Elements("Card").ToList();
                foreach (var element in inDocument)
                {
                    Card item = new Card();
                    item.Id = element.Attribute("Key").Value.ToString();
                    item.CardValue = Convert.ToInt32(element.Attribute("CardValue").Value);
                    item.Counter = Convert.ToInt32(element.Attribute("Counter").Value);
                    item.Grading = Convert.ToByte(element.Attribute("Grading").Value);
                    item.BoundArticle = element.Attribute("Article").Value.ToString();
                    item.Question = element.Element("Question").Value.ToString();
                    item.ImageForFront = element.Element("Question").Attribute("Image").Value.ToString();
                    item.Answer = element.Element("Answer").Value.ToString();
                    item.ImageForBack = element.Element("Answer").Attribute("Image").Value.ToString();
                    items.Add(item);
                }
            }
            return items;
        }
        
        public static void DeleteCard(string cardId, string categoryId)
        {
            XDocument doc = XDocument.Load(DataManagement.RootDirectory+"\\"+categoryId+ "\\cards.xml");
            var item = doc.Element("Cards").Elements("Card").Where(x => x.Attribute("Key").Value == cardId);

            //zur Karte gespeicherte Bilddateien löschen
            var frontImage = item.First().Element("Question").Attribute("Image").Value.ToString();
            var backImage = item.First().Element("Answer").Attribute("Image").Value.ToString();
            DeleteFrontAndBackImage(frontImage, backImage);

            item.Remove();
            doc.Save(DataManagement.RootDirectory + "\\" + categoryId + "\\cards.xml");
        }

        /// <summary>
        /// Löscht Bilder die den Fragen/Antworten einer Karte zugeordnet wurden.
        /// Prüft ob ein solches Bild angegeben wurden
        /// </summary>
        /// <param name="front">Pfadangabe zum Bild für die Frageseite</param>
        /// <param name="back">Pfadangabe zum Bild für die Antwortseite</param>
        private static void DeleteFrontAndBackImage(string front, string back)
        {
            if (!String.IsNullOrWhiteSpace(front))
            {
                File.Delete(front);
            }

            if (!String.IsNullOrWhiteSpace(back))
            {
                File.Delete(back);
            }
        }
        
        public bool UpdateCard(string subjectId)
        {
            string file = DataManagement.RootDirectory + "\\" + subjectId + "\\cards.xml";
            
            if (File.Exists(file))
            {
                XDocument document = XDocument.Load(file);
                var elements = document.Element("Cards").Elements("Card").ToList(); //Key
                var element = elements.Where(x=> x.Attribute("Key").Value == this.Id).SingleOrDefault();

                if(element != null)
                {
                    element.Attribute("CardValue").Value = this.CardValue.ToString();
                    element.Attribute("Grading").Value = this.Grading.ToString();
                    element.Attribute("Counter").Value = this.Counter.ToString();
                    element.Attribute("Article").Value = (!String.IsNullOrWhiteSpace(this.BoundArticle)) ? this.BoundArticle : "";
                    element.Element("Question").Value = (!String.IsNullOrWhiteSpace(this.Question)) ? this.Question.Replace("<", @"&//60").Replace(">", @"&//62") : "";                    
                    element.Element("Answer").Value = (!String.IsNullOrWhiteSpace(this.Answer)) ? this.Answer.Replace("<", @"&//60").Replace(">", @"&//62") : "";

                    string directory = DataManagement.RootDirectory + "\\" + subjectId;
                    if (!String.IsNullOrWhiteSpace(ImageForFront) || !String.IsNullOrWhiteSpace(ImageForBack))  //wenn es min. ein Bild gibt
                    {
                        var files = Directory.GetFiles(directory);
                        string front = directory + "\\" + subjectId + "_0.";
                        string back = directory + "\\" + subjectId + "_1.";
                        foreach (var item in files)
                        {
                            //delete Images if neccessary, verwendung von StartsWith, da das Bildformat nicht bekannt ist
                            if (!ImageForFront.StartsWith(front) && !String.IsNullOrWhiteSpace(ImageForFront) && item.StartsWith(front))
                                File.Delete(item);
                            if (!ImageForBack.StartsWith(back) && !String.IsNullOrWhiteSpace(ImageForBack) && item.StartsWith(back))
                                File.Delete(item);
                        }
                    }

                    SaveImages(directory);
                    element.Element("Answer").Attribute("Image").Value = (!String.IsNullOrWhiteSpace(this.ImageForBack)) ? this.ImageForBack : "";
                    element.Element("Question").Attribute("Image").Value = (!String.IsNullOrWhiteSpace(this.ImageForFront)) ? this.ImageForFront : "";

                    document.Save(file);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
