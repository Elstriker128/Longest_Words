using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Longest_Word_Individual_Excercise_Harder_Version
{
    internal class InOut
    {
        /// <summary>
        /// This method prints all the longest words that are in both files
        /// </summary>
        /// <param name="fout">the filename of the result file</param>
        /// <param name="final">an array of all the longest words that are in both files</param>
        /// <param name="Amount1">a Dictionary that shows the amounts of the longest words in the first file</param>
        /// <param name="Amount2">a Dictionary that shows the amounts of the longest words in the second file</param>
        /// <param name="k">array's element count</param>
        public static void WriteData(string fout, string[] final, Dictionary<string, int> Amount1, Dictionary<string, int> Amount2, int k)
        {
            string dashes = new string('-', 66);
            string[] lines = new string[k + 3];
            lines[0] = dashes;
            lines[1] = String.Format(" | {0,-15} | {1,-20} | {2, -20} | ",
                   "Žodis", "Kiekis faile Knyga1", "Kiekis faile Knyga2");
            for (int i = 0; i < k; i++)
            {
                if (Amount1.ContainsKey(final[i]) && Amount2.ContainsKey(final[i]))
                {
                    lines[i + 2] = String.Format(" | {0,-15} | {1,-20} | {2, -20} | ", final[i], Amount1[final[i]], Amount2[final[i]]);
                }
            }
            lines[k + 2] = dashes;
            File.AppendAllLines(fout, lines, Encoding.UTF8);
        }
        /// <summary>
        /// This method prints all the longest words that are only in the Knyga1 file
        /// </summary>
        /// <param name="fout">the filename of the result file</param>
        /// <param name="found">an array of all the longest words that are only in the Knyga1 file</param>
        /// <param name="Amount3">a Dictionary that shows the amounts of the longest words in the first file</param>
        /// <param name="z">array's element count</param>
        public static void WriteSingleData(string fout, string[] found, Dictionary<string, int> Amount3, int z)
        {
            string dashes = new string('-', 43);
            string[] lines = new string[z + 2];
            lines[0] = String.Format(" | {0,-15} | {1,-20} | ",
                   "Žodis", "Kiekis faile Knyga1");
            for (int i = 0; i < z; i++)
            {
                if (Amount3.ContainsKey(found[i]))
                {
                    lines[i + 1] = String.Format(" | {0,-15} | {1,-20} | ", found[i], Amount3[found[i]]);
                }
            }
            lines[z + 1] = dashes;
            File.AppendAllLines(fout, lines, Encoding.UTF8);
        }
        /// <summary>
        /// This method converts the reading line by line of a data file into a singular string
        /// </summary>
        /// <param name="fin">the filename of a data file</param>
        /// <returns>a singular string of all the data file's lines</returns>
        public static string ConvertToString(string fin)
        {
            string result=string.Empty;
            string line;
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    result += line;
                }
            }            
            return result;
        }
        /// <summary>
        /// This method outputs the connected data files' string onto a file
        /// </summary>
        /// <param name="fout">the filename of a data output file</param>
        /// <param name="whole">the singular string of all the data files' lines</param>
        public static void WriteString(string fout, string whole)
        {
            using(StreamWriter sw = new StreamWriter(fout))
            {
                sw.WriteLine(whole);
            }
        }
    }
}
