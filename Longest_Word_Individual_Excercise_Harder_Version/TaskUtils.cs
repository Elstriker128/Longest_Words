using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.CodeDom;

namespace Longest_Word_Individual_Excercise_Harder_Version
{
    internal class TaskUtils
    {
        /// <summary>
        /// This method finds the longest word in a line
        /// </summary>
        /// <param name="line">the given line</param>
        /// <param name="punctuation">the given array of punctuation signs</param>
        /// <returns>the longest word in a line</returns>
        private static string LongestWord(string line, char[] punctuation)
        {
            string[] parts = line.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
            string longestWord = "";
            foreach (string word in parts)
            {
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
            return longestWord;
        }
        /// <summary>
        /// This method finds all the longest words in all the lines that are in both files
        /// </summary>
        /// <param name="fin1">the filename of the first file</param>
        /// <param name="fin2">the filename of the second file</param>
        /// <param name="punctuation">the given array of punctuation signs</param>
        /// <param name="list">a string array that holds all the longest words</param>
        /// <param name="k">list array element count</param>
        public static void AddLongestWords(string fin1, string fin2, char[] punctuation, ref string[] list, ref int k)
        {
            string max;
            string line1, line2;
            using (StreamReader reader1 = new StreamReader(fin1, Encoding.UTF8))
            {
                using (StreamReader reader2 = new StreamReader(fin2, Encoding.UTF8))
                {
                    while ((line1 = reader1.ReadLine()) != null && (line2 = reader2.ReadLine()) != null)
                    {
                        max = TaskUtils.LongestWord(line1, punctuation);
                        string[] parts1 = line1.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in parts1)
                        {
                            if (word.Length == max.Length && line2.ToLower().Contains(word.ToLower()) && k <= 10 && !list.Contains(word.ToLower()))
                            {
                                list[k] = word.ToLower();
                                k++;
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This method finds all the longest words in all the lines that are only located in the Knyga1.txt file, 
        /// but not in the Knyga2.txt file
        /// </summary>
        /// <param name="fin1">the filename of the first file</param>
        /// <param name="fin2">the filename of the second file</param>
        /// <param name="punctuation">the given array of punctuation signs</param>
        /// <param name="z">array element count</param>
        /// <param name="list">array of all longest the words in both files</param>
        /// <returns>an array that holds all the longest words in all the lines that are only located in the Knyga1.txt file</returns>
        public static string[] AddSingleLongestWords(string fin1, string fin2, char[] punctuation, string[] list, out int z)
        {
            string max;
            string line1, line2;
            string[] found = new string[11];
            z = 0;

            using (StreamReader reader1 = new StreamReader(fin1, Encoding.UTF8))
            {
                using (StreamReader reader2 = new StreamReader(fin2, Encoding.UTF8))
                {
                    while ((line1 = reader1.ReadLine()) != null && (line2 = reader2.ReadLine()) != null)
                    {
                        max = TaskUtils.LongestWord(line1, punctuation);
                        string[] parts1 = line1.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in parts1)
                        {
                            if (line2.ToLower().Contains(word.ToLower()))
                            {
                                continue;
                            }
                            else if (word.Length == max.Length && !list.Contains(word.ToLower()) && !line2.ToLower().Contains(word.ToLower()) && z <= 10 && !found.Contains(word.ToLower()))
                            {
                                found[z] = word.ToLower();
                                z++;
                                break;
                            }
                        }
                    }
                }
            }
            return found;
        }
        /// <summary>
        /// This method counts how many times all the longest words were repeated in the given file
        /// </summary>
        /// <param name="fin">the filename of a file</param>
        /// <param name="list">an array of all the longest words</param>
        /// <param name="punctuation">the given array of punctuation signs</param>
        /// <returns>all the amounts of all the longest words in a single file</returns>
        public static Dictionary<string, int> Amount(string fin, string[] list, char[] punctuation)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string line;
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in parts)
                    {
                        if (list.Contains(word.ToLower()))
                        {
                            result.Add(word, 0);
                            result[word]++;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// This method sorts the found lists of all the longest words according to their lenght
        /// </summary>
        /// <param name="list"></param>
        /// <param name="k"></param>
        public static void Sort(string[] list, int k)
        {
            for (int i = 0; i < k - 1; i++)
            {
                if (list[i].Length.CompareTo(list[i + 1].Length) < 0)
                {
                    string current = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = current;
                }
            }
        }
        /*
        Second part of the excercise
        */
        /// <summary>
        /// This method puts the text from all the data files into one data file
        /// </summary>
        /// <param name="text1">the text from one data file</param>
        /// <param name="text2">the text from another data file</param>
        /// <returns>a combination of two data file strings</returns>
        public static string Combination(string text1, string text2)
        {
            string main = text1;
            string side = text2;
            string output = "";
            while (main != "")
            {
                string word = "";
                word = Regex.Match(side, @"\w+", RegexOptions.IgnoreCase).Value;

                int index = main.IndexOf(word);
                if (index == -1)
                {
                    output += main + " ";
                    break;
                }
                else
                {
                    output += main.Substring(0, index);
                    main = main.Remove(0, index + word.Length);
                    Match match = Regex.Match(main, @"\w+");
                    if (match.Success)
                    {
                        main = main.Remove(0, match.Index);
                    }
                    else
                    {
                        main = "";
                    }
                }
                string temp = main;
                main = side;
                side = temp;
            }
            output += side;
            return output;
        }
    }
}
