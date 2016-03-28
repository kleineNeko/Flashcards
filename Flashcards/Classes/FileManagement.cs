using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Flashcards.DataTyps;

namespace Flashcards
{
    public class FileManagement 
    {

        #region Directory-Management
        /// <summary>
        /// Filtert zugelassene Bilddateien aus einem Verzeichnis
        /// </summary>
        /// <param name="directoryPath">zu filterndes Verzeichnis</param>
        /// <returns>Liste mit Pfaden für Bilddateien</returns>
        public List<string> GetImageFiles(string directoryPath)
        {
            List<string> pictureFiles = new List<string>();
            try {
                string[] files = Directory.GetFiles(directoryPath);                
                foreach (string file in files)
                {
                    string f = file.ToUpper();
                    if (f.EndsWith("GIF") || f.EndsWith("BMP") || f.EndsWith("JPG") || f.EndsWith("PNG") || f.EndsWith("TIFF"))
                    {
                        pictureFiles.Add(file);
                    }
                }
                return pictureFiles;
            }
            catch 
            {
                return pictureFiles;
            }
        }


        //wird nicht benutzt
        public void GetFileDirectories()
        {
            List<DirectoryTree> tree = new List<DirectoryTree>();
            var drives = GetDirectorysPerDrive();
            CleanEmptyPaths(drives);           
            
        }

        /// <summary>
        /// noch nicht eingesetzt:
        /// Bereinigt die erste Ebene des Verzeichnisbaums: Verweise die keinen Unterweis in die zweite Ebene Haben und kein Bild enthalten fliegen raus
        /// </summary>
        /// <param name="directorys"></param>
        /// <returns></returns>
        private bool CleanEmptyPaths(Dictionary<string, List<DirectoryTree>> directorys)
        {
            bool done = true;
            foreach(var key in directorys.Keys)
            {
                int k = directorys[key].Count;
                var trees = directorys[key];
                for (int i = 0; i< k;i++) 
                {
                    List<DirectoryTree> list = new List<DirectoryTree>();
                    foreach(var path in trees[i].Subdirectorys)
                    {
                        DirectoryTree sub;
                        bool isTrue = HasSubDirectories(path, out sub);
                        if(isTrue || sub.HasImage)
                        {
                            list.Add(sub);
                        }
                    }
                    if(list.Count == 0)
                    {
                        trees.Remove(trees[i]);
                        CleanEmptyPaths(directorys);
                    }
                    
                    if (list.Count != trees[i].Subdirectorys.Count())
                    {
                        var item = trees[i];
                        trees.Remove(item);

                        item.Subdirectorys = new string[list.Count];
                        item.Subdirectorys= list.Select(x => x.Path).ToArray();
                        trees.Add(item);
                        CleanEmptyPaths(directorys);
                    }
                }
            }
            return done;
        }

        /// <summary>
        /// Speichert die Directorys unter Zuordnung zu ihrem Laufwerk in einem Diktionary,
        /// selektiert dabei jene Pfade aus, die weder über Unterkategorien noch über eine zugelassene Bilddatei verfügen
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, List<DirectoryTree>> GetDirectorysPerDrive() 
        {
            Dictionary<string, List<DirectoryTree>> directorys = new Dictionary<string, List<DirectoryTree>>();
            var drives = GetAllDrives();

            foreach(var drive in drives)
            {
                try
                {
                    string[] items = Directory.GetDirectories(drive);
                    List<DirectoryTree> list = new List<DirectoryTree>();
                    foreach(var item in items)
                    {
                        DirectoryTree tree;
                        bool istrue = HasSubDirectories(item, out tree);
                        if(istrue || tree.HasImage)
                        {
                            list.Add(tree);
                        }
                    }
                    if(list.Count!= 0)
                    {
                        directorys.Add(drive, list);
                    }
                }
                catch { }                
            }
            return directorys;
        }

        /// <summary>
        /// Ruft alle Laufwerke auf dem System ab
        /// </summary>
        /// <returns>Namen der Laufwerke des Systems</returns>
        private List<string> GetAllDrives()
        {
            return DriveInfo.GetDrives().Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Prüft ob ein Verzeichnis weiter Verzweigt ist und ob es mindestens eine Bilddatei enthält
        /// </summary>
        /// <param name="directory">Pfad des zu üprüfenden Verzeichnisses</param>
        /// <param name="item"></param>
        /// <returns>true wenn das Verzeichnis weitere Verzweigungen aufweist</returns>
        public bool HasSubDirectories(string directory, out DirectoryTree item)
        {
            item = new DirectoryTree();
            bool hasChilds = false;
            try
            {
                item.Subdirectorys = Directory.GetDirectories(directory);
                item.HasImage = DirectoryHasImageFiles(directory);
                if (item.HasImage || item.Subdirectorys.Length != 0)
                {
                    item.Path = directory;
                    item.DirectoryName = Path.GetFileName(directory);
                    item.Parent = Directory.GetParent(directory).ToString();
                }
                hasChilds = (item.Subdirectorys.Length != 0) ? true : false;
            }
            catch { }
            return hasChilds;
        }

        /// <summary>
        /// Prüft ob ein Verzeichnis über mindestens eine Bilddatei verfügt
        /// </summary>
        /// <param name="directoryPath">zu prüfendes Verzeichnis</param>
        /// <returns>true wenn das Verzeichnis eine Bilddatei enthält</returns>
        public bool DirectoryHasImageFiles(string directoryPath)
        {
            bool hasImageFile = false;
            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    string f = file.ToUpper();
                    if (f.EndsWith("GIF") || f.EndsWith("BMP") || f.EndsWith("ICO") || f.EndsWith("JPG") || f.EndsWith("PNG") || f.EndsWith("WDP") || f.EndsWith("TIFF"))
                    {
                        hasImageFile = true;
                        break;
                    }
                }
            }
            catch
            {
                return hasImageFile;
            }
            return hasImageFile;
        }
        #endregion

    }
}
