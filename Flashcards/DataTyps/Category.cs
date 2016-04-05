using System;
using System.Collections.Generic;
using System.Linq;
using Flashcards.Classes;
using System.IO;
using System.Xml.Linq;

namespace Flashcards.DataTyps
{
    public class Category
    {
        public string Id { get; private set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name.Replace(@"&//60", "<").Replace("&//62", ">");
            }
            set
            {
                _name = value.Replace("<", @"&//60").Replace(">", @"&//62");
            }
        }
        public string ImagePath { get; set; }

        //Settings
        public bool RoleBothCardSides { get; set; }
        public bool ShowPage { get; set; }
        public int CardAmountPerSession { get; set; }


        public bool CreateCategory()
        {
            if (!String.IsNullOrWhiteSpace(_name))
            {
                Id = _name.GetHashCode().ToString("x");  //Guid.NewGuid().ToString().GetHashCode().ToString("x")
                string dir = DataManagement.RootDirectory + "\\" + Id;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                    CreateCardFile(dir);
                    if (!String.IsNullOrWhiteSpace(ImagePath))
                    {
                        var imageFormat = ImagePath.Count(x => x == '.');
                        var imagepath = (!String.IsNullOrWhiteSpace(ImagePath)) ? dir + "\\" + Id + "." + ImagePath.Split('.')[imageFormat] : "";
                        ImageHandling.ImageToStock(this.ImagePath, imagepath);
                        ImagePath = imagepath;
                    }
                    else
                        ImagePath = "";

                    CategoryToXML();
                    return true;
                }
                //Kategorie existiert bereits
            }
            return false;   //der Name ist das Minimum was eine Kategorie haben muss
        }


        //Add neu Category to Stock
        private void CategoryToXML()
        {
            XElement xml = new XElement("Category", new XAttribute("Key", Id), new XAttribute("Name", _name), new XAttribute("Image", ImagePath), new XAttribute("RolebothCardsides", (RoleBothCardSides == true) ? "1" : "0"), new XAttribute("ShowPage", (ShowPage == true) ? "1" : "0"), new XAttribute("CardAmountPerSession", CardAmountPerSession.ToString()));
            if (File.Exists(DataManagement.CategoryFile))
            {
                XDocument doc = XDocument.Load(DataManagement.CategoryFile);
                doc.Elements("Categories").First().Add(xml);
                doc.Save(DataManagement.CategoryFile);
            }
        }

        public static List<Category> GetCategories()
        {
            List<Category> items = new List<Category>();
            if (File.Exists(DataManagement.CategoryFile))
            {
                XDocument doc = XDocument.Load(DataManagement.CategoryFile);
                var inDocument = doc.Element("Categories").Elements("Category").ToList();
                foreach (var element in inDocument)
                {
                    Category item = new Category();
                    item.Id = element.Attribute("Key").Value.ToString();
                    item.Name = element.Attribute("Name").Value.ToString();
                    item.ImagePath = element.Attribute("Image").Value.ToString();
                    item.RoleBothCardSides = (element.Attribute("RolebothCardsides").Value.ToString().Equals("1")) ? true : false;
                    item.ShowPage = (element.Attribute("ShowPage").Value.ToString().Equals("1")) ? true : false;
                    item.CardAmountPerSession = Convert.ToInt32(element.Attribute("CardAmountPerSession").Value);
                    items.Add(item);
                }
            }
            return items;
        }

        public static void DeleteCategory(string id)
        {
            if (File.Exists(DataManagement.CategoryFile))
            {
                XDocument doc = XDocument.Load(DataManagement.CategoryFile);
                var item = doc.Element("Categories").Elements("Category").Where(x => x.Attribute("Key").Value == id);
                item.Remove();
                doc.Save(DataManagement.CategoryFile);

                //Ordner + enthaltene Dateien löschen
                var dir = new DirectoryInfo(DataManagement.RootDirectory + "\\" + id);
                dir.Delete(true);
            }
        }

        private void CreateCardFile(string directoryPath)
        {
            string fileName = directoryPath + "\\cards.xml";
            XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            document.Add(new XElement("Cards"));
            document.Save(fileName);
        }

        public void UpdateCategory(string id)
        {
            string file = DataManagement.RootDirectory + "\\categories.xml";
            if (File.Exists(file))
            {
                XDocument document = XDocument.Load(file);
                var elements = document.Element("Categories").Elements("Category").ToList(); //Key
                var element = elements.Where(x => x.Attribute("Key").Value == id).SingleOrDefault();

                if (element != null)
                {
                    element.Attribute("Name").Value = _name;
                    element.Attribute("RolebothCardsides").Value = (RoleBothCardSides == true) ? "1" : "0";
                    element.Attribute("ShowPage").Value = (ShowPage == true) ? "1" : "0";
                    element.Attribute("CardAmountPerSession").Value = CardAmountPerSession.ToString();

                    var files = Directory.GetFiles(DataManagement.RootDirectory + "\\" + id).ToList();

                    if (!ImagePath.Contains(id) && !String.IsNullOrEmpty(ImagePath))    //bestehndes Bild ersetzten
                    {
                        string targetPath = DataManagement.RootDirectory + "\\" + id + "\\" + id + "." + ImagePath.Split('.').Last();
                        foreach (var item in files)
                        {
                            if (item.StartsWith(DataManagement.RootDirectory + "\\" + id + "\\" + id + "."))
                            {
                                File.Delete(item);
                                break;
                            }
                        }
                        ImageHandling.ImageToStock(ImagePath, targetPath);
                        element.Attribute("Image").Value = targetPath;
                    }

                    if (String.IsNullOrEmpty(ImagePath))    //bestehendes Bild löschen
                    {
                        foreach(var item in files)
                        {
                            if (item.StartsWith(DataManagement.RootDirectory + "\\" + id + "\\" + id + "."))
                            {
                                element.Attribute("Image").Value = "";
                                File.Delete(item);
                                break;
                            }
                        }
                    }

                    document.Save(file);
                }
            }
        }
    }
}

