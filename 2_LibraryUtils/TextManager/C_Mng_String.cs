using _2_LibraryUtils.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace _2_LibraryUtils.Text
{
    public static class C_Mng_String
    {
        public static bool IsNumber(char character)
        {
            switch (character)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsNumber(string text)
        {
            return long.TryParse(text, out _);
        }

        public static bool IsOnlyLetters(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }

        public static bool IsOnlyLettersAndNumbers(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z0-9]+$");
        }

        public static bool IsOnlyLettersAndNumbersAndUnderscore(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z0-9_]+$");
        }

        public static bool IsDateTime(string text, string format, out DateTime dateTime)
        {
            dateTime = new DateTime();

            if (IsNumber(text) && text.Length == format.Length)
            {
                try
                {
                    char c = 'y';
                    int i = format.IndexOf(c);
                    string sYear = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iYear = int.Parse(sYear);
                    if (iYear < 1)
                        return false;

                    c = 'M';
                    i = format.IndexOf(c);
                    string sMonth = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iMonth = int.Parse(sMonth);
                    if (iMonth > 12 || iMonth < 1)
                        return false;

                    c = 'd';
                    i = format.IndexOf(c);
                    string sDay = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iDay = int.Parse(sDay);
                    if (iDay > 31 || iDay < 1)
                        return false;

                    c = 'h';
                    i = format.IndexOf(c);
                    string sHour = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iHour = int.Parse(sHour);
                    if (iHour > 24 || iHour < 1)
                        return false;

                    c = 'm';
                    i = format.IndexOf(c);
                    string sMinute = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iMinute = int.Parse(sMinute);
                    if (iMinute > 60 || iMinute < 1)
                        return false;

                    c = 's';
                    i = format.IndexOf(c);
                    string sSecond = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iSecond = int.Parse(sSecond);
                    if (iSecond > 60 || iSecond < 1)
                        return false;

                    c = 'f';
                    i = format.IndexOf(c);
                    string sMilisecond = i >= 0 ? text.Substring(i, CountChars(text, c, true, i)).TrimStart('0') : "0";
                    int iMilisecond = int.Parse(sMilisecond);
                    if (iMilisecond > 60 || iMilisecond < 1)
                        return false;

                    dateTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMilisecond);
                    return true;
                }
                catch (Exception ex)
                {
                    C_DebugLogger.Add(ex, "C_Mng_Files.IsDateTime(string text, string format, out DateTime dateTime)");
                    return false;
                }

            }

            return false;
        }

        public static bool IsDateTime(string text, string[] formats, out DateTime dateTime)
        {
            bool result = false;
            dateTime = new DateTime();

            foreach (string format in formats)
            {
                result = IsDateTime(text, format, out dateTime);
                if (result)
                    return true;
            }

            return result;
        }

        public static bool GetDateTime(string text, string format, out DateTime dateTime)
        {
            dateTime = new DateTime();

            try
            {
                int i = 0;
                string sYear = "";
                string sMonth = "";
                string sDay = "";
                string sHour = "";
                string sMinute = "";
                string sSecond = "";
                string sMilisecond = "";

                foreach (char c in text.ToCharArray())
                {
                    if (IsNumber(c))
                    {
                        switch (format[i])
                        {
                            case 'y':
                                if (!(c == '0' && sYear == ""))
                                    sYear += c;
                                i++;
                                break;
                            case 'M':
                                if (!(c == '0' && sMonth == ""))
                                    sMonth += c;
                                i++;
                                break;
                            case 'd':
                                if (!(c == '0' && sDay == ""))
                                    sDay += c;
                                i++;
                                break;
                            case 'h':
                                if (!(c == '0' && sHour == ""))
                                    sHour += c;
                                i++;
                                break;
                            case 'm':
                                if (!(c == '0' && sMinute == ""))
                                    sMinute += c;
                                i++;
                                break;
                            case 's':
                                if (!(c == '0' && sSecond == ""))
                                    sSecond += c;
                                i++;
                                break;
                            case 'f':
                                if (!(c == '0' && sMilisecond == ""))
                                    sMilisecond += c;
                                i++;
                                break;
                            default:
                                return false;
                        }
                    }
                    else if (c == format[i])
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }

                    if (i >= format.Length)
                    {
                        int iYear = int.Parse(sYear);
                        if (iYear < 1)
                            return false;

                        int iMonth = int.Parse(sMonth);
                        if (iMonth > 12 || iMonth < 1)
                            return false;

                        int iDay = int.Parse(sDay);
                        if (iDay > 31 || iDay < 1)
                            return false;

                        int iHour = int.Parse(sHour);
                        if (iHour > 24 || iHour < 1)
                            return false;

                        int iMinute = int.Parse(sMinute);
                        if (iMinute > 60 || iMinute < 1)
                            return false;

                        int iSecond = int.Parse(sSecond);
                        if (iSecond > 60 || iSecond < 1)
                            return false;

                        int iMilisecond = int.Parse(sMilisecond);
                        if (iMilisecond > 60 || iMilisecond < 1)
                            return false;

                        dateTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMilisecond);
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_Mng_String.GetDateTime(string text, string format, out DateTime dateTime)");
                return false;
            }

            return false;
        }

        public static bool GetDateTime(string text, string[] formats, out DateTime dateTime)
        {
            bool result = false;
            dateTime = new DateTime();

            foreach (string format in formats)
            {
                result = GetDateTime(text, format, out dateTime);
                if (result)
                    return true;
            }

            return result;
        }        

        /// <summary>
        /// Returns an empty string if the input string is null or empty; otherwise, returns the original string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrNullToEmpty(string str)
        {
            string q = str;

            if (string.IsNullOrEmpty(q))
            {
                q = string.Empty;
            }

            return q;
        }

        /// <summary>
        /// Converts the first character of a string to uppercase. If the string is null or empty, it throws an ArgumentException.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string FirstCharToUpper(string word)
        {

            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Word is Is Null Or Empty");
            else
            {
                string result = word[1].ToString().ToUpper() + word.Substring(1);
                return result;
            }
        }        

        public static string Clean_RNT(string str)
        {
            string q = Clean_RN(str).Trim();
            return q;
        }

        public static byte[] ConvertToByteArray(string str)
        {
            //int NumberChars = str.Length;
            //byte[] bytes = new byte[NumberChars / 2];
            //for (int i = 0; i < NumberChars; i += 2)
            //    bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            //return bytes;

            byte[] bytes = Encoding.ASCII.GetBytes(str);
            return bytes;
        }

        /// <summary>
        /// Count the number of occurrences of a character in a string starting from a specific index
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toFind"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int CountChars(string source, char toFind, int startIndex = 0)
        {
            int count = 0;

            for (int n = startIndex; n < source.Length; n++)
            {
                if (source[n] == toFind)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Count the number of occurrences of a character in a string starting from a specific index, with an option to stop after the first occurrence
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toFind"></param>
        /// <param name="onlyFirst"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int CountChars(string source, char toFind, bool onlyFirst = false, int startIndex = 0)
        {
            int count = 0;

            for (int n = startIndex; n < source.Length; n++)
            {
                if (source[n] == toFind)
                {
                    count++;
                }
                else if (count != 0)
                {
                    break;
                }
            }

            return count;
        }

        //&lt; < 
        //&gt; > 
        //&erio; Y
        //&quot; "
        //&apos; '
        public static string Clean_RN(string str)
        {
            string q = str;
            q = q.Replace("\r\n", " ");
            q = q.Replace("\n\r", " ");
            q = q.Replace("\r", " ");
            q = q.Replace("\n", " ");

            return q;
        }

        public static string ImportFromByteArray(byte[] bytes)
        {
            //StringBuilder str = new StringBuilder(bytes.Length * 2);
            //foreach (byte b in bytes)
            //    str.AppendFormat("{0:x2}", b);

            //return str.ToString();

            string str = Encoding.ASCII.GetString(bytes);
            return str;
        }

        public static string ImportFromByteArray2(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string EncodeToBase64(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        public static string DecodeFromBase64(string source)
        {
            byte[] bytes = Convert.FromBase64String(source);
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }
    }
}
