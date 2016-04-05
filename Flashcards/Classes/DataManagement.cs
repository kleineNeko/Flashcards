using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Flashcards.Classes
{
    //Kümmert sich darum dass Ordner uns Speicherdaten angelegt werden
    public class DataManagement
    {

        public static string RootDirectory { get; private set; }
        public static string CategoryFile
        {
            get
            {
                return RootDirectory + "\\categories.xml"; 
            }
        }

        /// <summary>
        /// Prüft ob der Ordner für die Kategorien und damit Karten & Artikel vorhanden ist und ob es eine entsprechende
        /// Verzeichnisdatei gibt. Fehlt etwas wird es neu angelegt.
        /// </summary>
        /// <returns>gibt an ob Ordner und Verzeichnisdatei vorhanden sind</returns>
        public static bool MainFolder()
        {
            bool stableSetup = false;

            var dir = Directory.GetCurrentDirectory();
            string path = dir +@"\data";
            bool exisits = Directory.Exists(path);
            if(exisits == false)
            {
                Directory.CreateDirectory(path);
                RootDirectory = path;
                AddCategoryFile();
                if (Directory.Exists(path))
                {
                    stableSetup = true;
                }
            }
            else
            {
                RootDirectory = path;
                CheckForCategoryFile();
                stableSetup = true;
            }            

            return stableSetup;
        }

        /// <summary>
        /// Erstellt eine Verzeichnisdatei für die Kategorien
        /// </summary>
        private static void AddCategoryFile()
        {
            string fileName = RootDirectory + "\\categories.xml";
            XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            document.Add(new XElement("Categories"));
            document.Save(fileName);
            //FileStream stream = new FileStream(fileName, FileMode.Create);
            //stream.Close();
        }

        public static string ByteArrayToString(byte[] array)
        {
            StringBuilder entry = new StringBuilder();

            foreach(var item in array)
            {
                entry.Append(item + " ");
            }
            return entry.ToString().Trim();
        }

        /// <summary>
        /// Überprüft ob die Verzeichnisdatei für die Kategorien im Erwarteten Verzeichnis liegt,
        /// wenn nicht wird sie angelegt
        /// </summary>
        private static void CheckForCategoryFile()
        {
            var exists = File.Exists(CategoryFile);
            if (!exists)
            {
                AddCategoryFile();
            }
        }
    }
}
