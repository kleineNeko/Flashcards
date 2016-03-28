using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Flashcards.Classes
{
    //public static string DirectoryRoot;

    //neue klasse die sich darum kümmert das ordner uns speicherdaten angelegt werden
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

        public static bool MainFolder()
        {
            var dir = Directory.GetCurrentDirectory();
            string path = dir +@"\data";
            bool exisits = Directory.Exists(path);
            if(exisits == false)
            {
                Directory.CreateDirectory(path);
                RootDirectory = path;
                AddCategoryFile();
                if(Directory.Exists(path))
                    return true;
            }
            else
            {
                RootDirectory = path;
                return true;
            }

            return false;
        }

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
    }
}
