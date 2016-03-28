using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Flashcards
{
    class ExceptionHandling
    {
        

        public ExceptionHandling()
        {

        }

        public ExceptionHandling(string message, string className, DateTime timestamp, string cause, string target, string path)
        {
            List<string> entry = CreateEntry(timestamp, className, message, cause, target, path);
            WriteExceptionEntry(entry);
            SendMessage(message);
        }

        /// <summary>
        /// Gibt eine Fehlermeldung beim Nutzer aus
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            System.Windows.MessageBox.Show(message);
        }
        
        /// <summary>
        /// Methode welche die Aufgetretene Exception protocolliert
        /// </summary>
        private void WriteExceptionEntry(List<string> entry)
        {
            string filePath = @Directory.GetCurrentDirectory().ToString() + @"\ErrorLog.txt";            
            try
            {
                if (File.Exists(filePath))
                {                   
                    File.AppendAllLines(filePath, entry, Encoding.UTF8);
                }
                else
                {

                }                
            }
            catch { SendMessage("Es ist ein Fehler in ExceptionHandling aufgetretn."); }
        }

        /// <summary>
        /// Stellt einen Eintrag für den ErrorLog zusammen und gibt die Zusammenstellung zurück
        /// </summary>
        /// <param name="time"></param>
        /// <param name="className"></param>
        /// <param name="exMessage"></param>
        /// <param name="cause"></param>
        /// <param name="target"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<string> CreateEntry(DateTime time,string className, string exMessage, string cause, string target, string path)
        {
            List<string> entry = new List<string>();
            entry.Add("Exception: " + time.ToString() + " in der Klasse " + className);
            entry.Add(exMessage);
            entry.Add(cause);
            entry.Add(target);
            entry.Add(path);
            entry.Add("________________________________________________________________________________________________________");
            return entry;
        }

        /// <summary>
        /// Erstellt eine Datei in der aufgetretene Fehler protocolliert werden.
        /// </summary>
        private void CreateErrorLogFile()
        {
            FileInfo file = new FileInfo(@Directory.GetCurrentDirectory().ToString() + @"\ErrorLog.txt");
            FileStream fs = file.Create();
            fs.Close();            
        }
    }
}
