using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Classes
{
    public static class StringBuilderExtended
    {       

        public static List<StringBuilder> SplitAtComma(string line)
        {
            List<StringBuilder> parts = new List<StringBuilder>();
            StringBuilder part = new StringBuilder();
            int length = line.Length;
            for(int i =0; i < length; i++)
            {
                if(line[i] == ',')
                {
                    parts.Add(part);
                    part = new StringBuilder();
                }
                else
                    part.Append(line[i]);
            }
            return parts;
        }

        public static byte[] ToByteArray(StringBuilder line)
        {
            int length = line.Length;
            List<Byte> bytes = new List<byte>();
            StringBuilder number = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                if (line[i] == ' ')
                {
                    bytes.Add(Convert.ToByte(number.ToString()));
                    number.Clear();
                }
                else
                    number.Append(line[i]);
            }
            if (number.Length > 0)
                bytes.Add(Convert.ToByte(number.ToString()));
            return (bytes.Count > 0) ? bytes.ToArray() : null;
        }

        /// <summary>
        /// Holt die Daten aus den "[daten]" Datensetzten
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static List<StringBuilder> GetEntries(StringBuilder dataset)
        {
            List<StringBuilder> entries = new List<StringBuilder>();
            StringBuilder entrie = new StringBuilder();
            int length = dataset.Length;

            for(int i = 0; i < length; i++)
            {
                if (dataset[i] == '[')
                {
                    entrie = new StringBuilder();
                    continue;
                }                    
                if (dataset[i] == ']')
                {
                    entries.Add(entrie);
                    continue;
                }
                entrie.Append(dataset[i]);
            }

            return entries;
        }
    }
}
