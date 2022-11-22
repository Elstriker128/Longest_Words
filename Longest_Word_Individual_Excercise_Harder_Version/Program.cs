using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Longest_Word_Individual_Excercise_Harder_Version
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding.GetEncoding(1257);
            Console.OutputEncoding = Encoding.GetEncoding(1257);
            int k = 0;
            int z;
            string[] final = new string[11];
            const string Duom1 = "Knyga1.txt";
            const string Duom2 = "Knyga2.txt";
            const string Rez = "Rodikliai.txt";
            const string Tex = "ManoKnyga.txt";
            if (File.Exists(Rez))
            {
                File.Delete(Rez);
            }
            else if (File.ReadAllText(Duom1).Length == 0)
            {
                Console.WriteLine("Jokių pradinių duomenų");
            }
            char[] punctuation = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t' };           
            TaskUtils.AddLongestWords(Duom1, Duom2, punctuation, ref final, ref k);
            TaskUtils.AddLongestWords(Duom2, Duom1, punctuation, ref final, ref k);
            TaskUtils.Sort(final, k);
            Dictionary<string, int> Amount1 = TaskUtils.Amount(Duom1, final, punctuation);
            Dictionary<string, int> Amount2 = TaskUtils.Amount(Duom2, final, punctuation);
            if(final!=null)
            {
                InOut.WriteData(Rez, final, Amount1, Amount2, k);
            }
            else
            {
                Console.WriteLine("Error: No longest words that are in both files");
            }

            string[] found = TaskUtils.AddSingleLongestWords(Duom1, Duom2, punctuation, final, out z);
            TaskUtils.Sort(found, z);
            Dictionary<string, int> Amount3 = TaskUtils.Amount(Duom1, found, punctuation);
            if (found != null)
            {
                InOut.WriteSingleData(Rez, found, Amount3, z);
            }
            else
            {
                Console.WriteLine("Error: No longest words that are in Knyga1 file but not in the Knyga2 file");
            }

            string text1 = InOut.ConvertToString(Duom1);
            string text2 = InOut.ConvertToString(Duom2);
            string finalCombo = TaskUtils.Combination(text1, text2);
            if(finalCombo!=null)
            {
                InOut.WriteString(Tex, finalCombo);
            }
            else
            {
                Console.WriteLine("Error: Full text output in ManoKnyga file");
            }
        }
    }
}

