using System.Collections.Generic;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Helpers for a single <see cref="string"/>: validation, cleaning and simple transforms.
    /// <para>
    /// For words, lines and multi-token text, use <see cref="C_Hlp_Text"/>.
    /// </para>
    /// <para><i>[ES] Ayudas sobre una sola <see cref="string"/>: validación, limpieza y transformaciones simples.
    /// Palabras/líneas: <see cref="C_Hlp_Text"/>.</i></para>
    /// </summary>
    public static class C_Hlp_String
    {
#region PUBLIC METHODS — validation

        // Digit = one character 0-9
        // Digits = string of only digits
        // Number = any numeric form (integer or decimal)
        // Integer / Decimal = specific number kinds

        /// <summary>
        /// True if the character is a decimal digit <c>0</c>–<c>9</c>.
        /// <para><i>[ES] True si el carácter es un dígito decimal <c>0</c>–<c>9</c>.</i></para>
        /// </summary>
        public static bool IsDigit(char character)
            => character >= '0' && character <= '9';

        /// <summary>
        /// True if non-empty and every character is a decimal digit (no sign, no decimal point).
        /// <para><i>[ES] True si no está vacío y solo hay dígitos (sin signo ni punto decimal).</i></para>
        /// </summary>
        public static bool IsDigits(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            for (int i = 0; i < text.Length; i++)
            {
                if (!IsDigit(text[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// True if the text is an integer (<see cref="long.TryParse(string, out long)"/>).
        /// Allows optional sign; does not require pure digits only.
        /// <para><i>[ES] True si es un entero parseable. Admite signo opcional.</i></para>
        /// </summary>

        /// <summary>
        /// True if the text contains at least one decimal digit.
        /// <para><i>[ES] True si el texto contiene al menos un dígito decimal.</i></para>
        /// </summary>
        public static bool ContainsDigit(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            for (int i = 0; i < text.Length; i++)
            {
                if (IsDigit(text[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the text contains at least one letter.
        /// <para><i>[ES] True si el texto contiene al menos una letra.</i></para>
        /// </summary>
        public static bool ContainsLetter(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the text contains at least one letter or digit.
        /// <para><i>[ES] True si contiene al menos una letra o un dígito.</i></para>
        /// </summary>
        public static bool ContainsLetterOrDigit(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if the text contains any whitespace.
        /// <para><i>[ES] True si el texto contiene algún espacio en blanco.</i></para>
        /// </summary>
        public static bool ContainsWhiteSpace(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                    return true;
            }
            return false;
        }

        public static bool IsInteger(string? text)
            => long.TryParse(text, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out _);

        /// <summary>
        /// True if the text is a decimal number (not necessarily with a fractional part).
        /// Uses invariant culture (<c>.</c> as decimal separator).
        /// <para><i>[ES] True si es un número decimal (invariant, separador <c>.</c>).</i></para>
        /// </summary>
        public static bool IsDecimal(string? text)
            => decimal.TryParse(text, System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out _);

        /// <summary>
        /// True if the text represents any number (integer or decimal).
        /// Same as <see cref="IsDecimal"/> for practical parsing.
        /// <para><i>[ES] True si representa cualquier número (entero o decimal).</i></para>
        /// </summary>
        public static bool IsNumber(string? text)
            => IsDecimal(text);

        /// <summary>
        /// Tries to parse as <see cref="int"/> (invariant).
        /// <para><i>[ES] Intenta convertir a <see cref="int"/> (invariant).</i></para>
        /// </summary>
        public static bool TryToInt(string? text, out int value)
            => int.TryParse(text, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out value);

        /// <summary>
        /// Tries to parse as <see cref="long"/> (invariant).
        /// <para><i>[ES] Intenta convertir a <see cref="long"/> (invariant).</i></para>
        /// </summary>
        public static bool TryToLong(string? text, out long value)
            => long.TryParse(text, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out value);

        /// <summary>
        /// Tries to parse as <see cref="double"/> (invariant).
        /// <para><i>[ES] Intenta convertir a <see cref="double"/> (invariant).</i></para>
        /// </summary>
        public static bool TryToDouble(string? text, out double value)
            => double.TryParse(text, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out value);

        /// <summary>
        /// Tries to parse as <see cref="decimal"/> (invariant).
        /// <para><i>[ES] Intenta convertir a <see cref="decimal"/> (invariant).</i></para>
        /// </summary>
        public static bool TryToDecimal(string? text, out decimal value)
            => decimal.TryParse(text, System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out value);

        public static bool IsOnlyLetters(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }

        public static bool IsOnlyLettersAndNumbers(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            return Regex.IsMatch(text, @"^[a-zA-Z0-9]+$");
        }

        public static bool IsOnlyLettersAndNumbersAndUnderscore(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            return Regex.IsMatch(text, @"^[a-zA-Z0-9_]+$");
        }

        /// <summary>
        /// True if the text is a single word: non-empty and contains no whitespace.
        /// <para><i>[ES] True si es una sola palabra: no vacío y sin espacios en blanco.</i></para>
        /// </summary>
        public static bool IsWord(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                    return false;
            }
            return true;
        }

#endregion

#region PUBLIC METHODS — emoji

        public static bool IsEmoji(char character)
        {
            int v = character;
            if (v >= 0x2600 && v <= 0x27BF)
                return true;
            return false;
        }

        public static bool IsEmoji(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            bool any = false;
            foreach (var rune in text.EnumerateRunes())
            {
                any = true;
                if (!IsEmojiRune(rune))
                    return false;
            }
            return any;
        }

        public static bool ContainsEmoji(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            foreach (var rune in text.EnumerateRunes())
            {
                if (IsEmojiRune(rune))
                    return true;
            }
            return false;
        }

        private static bool IsEmojiRune(Rune rune)
        {
            int v = rune.Value;
            if (v == 0x200D || v == 0xFE0F || v == 0xFE0E)
                return true;
            if (v >= 0x1F300 && v <= 0x1FAFF)
                return true;
            if (v >= 0x1F600 && v <= 0x1F64F)
                return true;
            if (v >= 0x1F680 && v <= 0x1F6FF)
                return true;
            if (v >= 0x2600 && v <= 0x27BF)
                return true;
            if (v >= 0x2300 && v <= 0x23FF)
                return true;
            if (v >= 0x2B50 && v <= 0x2B55)
                return true;
            return false;
        }

#endregion

#region PUBLIC METHODS — normalize / clean

        public static string NullToEmpty(string? str)
            => string.IsNullOrEmpty(str) ? string.Empty : str;

        public static string CleanCrLf(string? str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str.Replace("\r\n", " ").Replace('\r', ' ').Replace('\n', ' ');
        }

        public static string CleanCrLfAndTabs(string? str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return CleanCrLf(str).Replace('\t', ' ');
        }

        public static string FirstCharToUpper(string? word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            if (word.Length == 1)
                return char.ToUpperInvariant(word[0]).ToString();
            return char.ToUpperInvariant(word[0]) + word.Substring(1);
        }

        public static int CountChars(string source, char toFind, int startIndex = 0)
            => CountChars(source, toFind, onlyFirst: false, startIndex);

        public static int CountChars(string source, char toFind, bool onlyFirst, int startIndex = 0)
        {
            if (string.IsNullOrEmpty(source) || startIndex >= source.Length)
                return 0;
            int count = 0;
            for (int i = Math.Max(0, startIndex); i < source.Length; i++)
            {
                if (source[i] == toFind)
                {
                    count++;
                    if (onlyFirst)
                        break;
                }
            }
            return count;
        }

#endregion

#region PUBLIC METHODS — word tokens (strings)

        /// <summary>
        /// Extracts word units (letter runs + digit runs) as <c>string[]</c>.
        /// <para><i>[ES] Extrae unidades palabra (letras + dígitos) como <c>string[]</c>.</i></para>
        /// </summary>
        public static string[] GetWords(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).ToArray();

        /// <summary>
        /// Extracts word units (letter runs + digit runs) as <see cref="List{T}"/> of strings.
        /// <para><i>[ES] Extrae unidades palabra (letras + dígitos) como lista de string.</i></para>
        /// </summary>
        public static List<string> GetWordList(string text, int minWordLength = 1)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(text))
                return result;

            StringBuilder word = new StringBuilder();
            int minLen = minWordLength - 1;
            bool? isNumber = null;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsDigit(c))
                {
                    if (isNumber == false)
                    {
                        if (word.Length > minLen)
                            result.Add(word.ToString());
                        word.Clear();
                    }
                    isNumber = true;
                    word.Append(c);
                }
                else if (char.IsLetter(c))
                {
                    if (isNumber == true)
                    {
                        result.Add(word.ToString());
                        word.Clear();
                    }
                    isNumber = false;
                    word.Append(c);
                }
                else if (word.Length != 0)
                {
                    string w = word.ToString();
                    if (int.TryParse(w, out _))
                        result.Add(w);
                    else if (word.Length > minLen)
                        result.Add(w);

                    word.Clear();
                    isNumber = null;
                }
            }

            if (word.Length != 0)
            {
                string w = word.ToString();
                if (int.TryParse(w, out _))
                    result.Add(w);
                else if (word.Length > minLen)
                    result.Add(w);
            }

            return result;
        }

#endregion

    }
}
