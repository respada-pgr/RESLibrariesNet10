using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace _2_LibraryUtils.Text
{
    public static class C_Mng_Text
    {
        /// <summary>
        /// Returns the time in minutes it takes to read the text. 
        /// On average, a person reads between 200 and 300 words per minute(wpm)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="wordsPerMinute"></param>
        /// <returns></returns>
        public static double CalculateReadingTime(string text, double wordsPerMinute = 250.0)
        {
            return CountWords(text) / wordsPerMinute;
        }

        /// <summary>
        /// Returns the time in ticks it takes to read the text. 
        /// A tick represents the number of 100 nanosecond intervals elapsed since January 1, 0001). 
        /// On average, a person reads between 200 and 300 words per minute(wpm)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="wordsPerMinute"></param>
        /// <returns></returns>
        public static long CalculateReadingTimeTicks(string text, double wordsPerMinute = 250.0)
        {
            return (long)(CalculateReadingTime(text, wordsPerMinute) * 60 * TimeSpan.TicksPerSecond);
        }

        static int CountWords(string text)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                result = words.Length;
            }
            return result;
        }

        public static string CapitalizeWords(string input, int minWordLength = 1)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                // Si la palabra tiene más de 2 caracteres, convierte la primera letra a mayúscula
                if (words[i].Length > minWordLength)
                {
                    words[i] = textInfo.ToTitleCase(words[i]);
                }
            }

            return string.Join(" ", words);
        }
        

        public static string[] getWords(string text, int minWordLenght = 1)
        {
            return getListWords(text, minWordLenght).ToArray();
        }

        public static string[] getWords2(string text)
        {
            string[] result = Regex.Split(text, @"\W|_"); //Hace el split por cualquier cualquier 'no palabra' o '_'

            return result;
        }

        public static List<string> getListWords(string text, int minWordLenght = 1)
        {

            List<string> result = new List<string>();

            StringBuilder txt = new StringBuilder(text);
            StringBuilder word = new StringBuilder("");
            char c = ' ';

            minWordLenght -= 1;


            for (int i = 0; i < text.Length; i++)
            {
                c = txt[i];

                if (Char.IsLetter(c))
                {
                    word.Append(c);
                }
                else if (word.Length != 0)
                {
                    string w = word.ToString();

                    if (int.TryParse(w, out _))
                    {
                        result.Add(w);
                    }
                    else if (word.Length > minWordLenght)
                    {
                        result.Add(w);
                    }

                    word = new StringBuilder("");
                }
            }

            return result;
        }

        public static List<string> getListWordsAndDigits(string text, int minWordLenght = 1)
        {

            List<string> result = new List<string>();

            StringBuilder txt = new StringBuilder(text);
            StringBuilder word = new StringBuilder("");
            char c = ' ';
            bool? isNumber = null;

            minWordLenght -= 1;


            for (int i = 0; i < text.Length; i++)
            {
                c = txt[i];
                if (Char.IsDigit(c))
                {
                    if (isNumber == null)
                    {
                        word.Append(c);
                        isNumber = true;
                    }
                    else if (isNumber == true)
                    {
                        word.Append(c);
                    }
                    else
                    {
                        if (word.Length > minWordLenght)
                        {
                            result.Add(word.ToString());
                        }
                        word = new StringBuilder(c.ToString());
                        isNumber = true;
                    }
                }
                else if (Char.IsLetter(c))
                {
                    if (isNumber == null)
                    {
                        word.Append(c);
                        isNumber = false;
                    }
                    else if (isNumber == false)
                    {
                        word.Append(c);
                    }
                    else
                    {
                        result.Add(word.ToString());
                        word = new StringBuilder(c.ToString());
                        isNumber = false;
                    }
                }
                else if (word.Length != 0)
                {
                    string w = word.ToString();

                    if (int.TryParse(w, out _))
                    {
                        result.Add(w);
                    }
                    else if (word.Length > minWordLenght)
                    {
                        result.Add(w);
                    }

                    word = new StringBuilder("");
                    isNumber = null;
                }
            }

            if (word.Length != 0)
            {
                string w = word.ToString();

                if (int.TryParse(w, out _))
                {
                    result.Add(w);
                }
                else if (word.Length > minWordLenght)
                {
                    result.Add(w);
                }

                word = new StringBuilder("");
                isNumber = null;
            }

            return result;
        }

        public static string[] getWordsAndDigits(string text, int minWordLenght = 1)
        {
            return getListWordsAndDigits(text, minWordLenght).ToArray();
        }

        
    }
}
