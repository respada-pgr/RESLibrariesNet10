using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
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
        public static bool IsOnlyDigits(string? text)
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
        /// True if the text represents any number (integer or decimal).
        /// Same as <see cref="IsDecimal"/> for practical parsing.
        /// <para><i>[ES] True si representa cualquier número (entero o decimal).</i></para>
        /// </summary>
        public static bool IsNumber(string? text)
            => IsDecimal(text);

        /// <summary>
        /// True if the text is an integer (<see cref="long.TryParse(string, out long)"/>).
        /// Allows optional sign; does not require pure digits only.
        /// <para><i>[ES] True si es un entero parseable. Admite signo opcional.</i></para>
        /// </summary>
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

        public static bool IsRomanNumber(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            // Simple implementation - you might want to use a more robust Roman numeral validation
            return Regex.IsMatch(text, @"^(I|V|X|L|C|D|M)+$", RegexOptions.IgnoreCase);
        }

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

        #region PUBLIC METHODS — contains

        /// <summary>
        /// True if the text contains at least one decimal digit.
        /// <para><i>[ES] True si el texto contiene al menos un dígito decimal.</i></para>
        /// </summary>
        public static bool ContainsOnlyDigits(string? text)
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
        public static bool ContainsOnlyLetterOrDigit(string? text)
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

        #endregion

        #region PUBLIC METHODS — format

        public static string From(int value) => value.ToString(CultureInfo.InvariantCulture);
        public static string From(long value) => value.ToString(CultureInfo.InvariantCulture);
        public static string From(float value) => value.ToString(CultureInfo.InvariantCulture);
        public static string From(double value) => value.ToString(CultureInfo.InvariantCulture);
        public static string From(decimal value) => value.ToString(CultureInfo.InvariantCulture);

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

        #region PUBLIC METHODS — Get (words, tokens, numbers)

        public static string[] GetWords(string text, int minWordLength = 1)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<string>();

            return text.Trim().Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(w => w.Length >= minWordLength).ToArray();
        }

        /// <summary>
        /// Extracts word units (letter runs + digit runs) as <see cref="List{T}"/> of strings.
        /// <para><i>[ES] Extrae unidades palabra (letras + dígitos) como lista de string.</i></para>
        /// </summary>
        public static List<string> GetWordList(string text, int minWordLength = 1)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            return GetWords(text, minWordLength).ToList();
        }

        public static string[] GetWordsOnlyLetters(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsOnlyLetters(w)).ToArray();


        public static List<string> GetWordsOnlyLettersList(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsOnlyLetters(w)).ToList();


        public static string[] GetWordsOnlyDigits(string text)
            => GetWordList(text).Where(w => IsOnlyDigits(w)).ToArray();


        public static List<string> GetWordsOnlyDigitsList(string text)
        {
            return GetWordList(text).Where(w => IsOnlyDigits(w)).ToList();
        }

        public static string[] GetWordsOnlyLettersAndDigits(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsOnlyLettersAndNumbers(w)).ToArray();

        public static List<string> GetWordsOnlyLettersAndDigitsList(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsOnlyLettersAndNumbers(w)).ToList();

        public static string[] GetWordsOnlyLettersAndDigitsAndUnderscore(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsOnlyLettersAndNumbersAndUnderscore(w)).ToArray();

        public static List<string> GetWordsOnlyLettersAndDigitsAndUnderscoreList(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsOnlyLettersAndNumbersAndUnderscore(w)).ToList();

        public static string[] GetNumbers(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsNumber(w)).ToArray();

        public static List<string> GetNumberList(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Where(w => IsNumber(w)).ToList();

        public static int GetNumWords(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Count;

        public static int GetNumWordsOnlyLetters(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Count(w => IsOnlyLetters(w));

        public static int GetNumWordsOnlyDigits(string text)
            => GetWordList(text).Count(w => IsOnlyDigits(w));

        public static int GetNumWordsOnlyLettersAndDigits(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Count(w => IsOnlyLettersAndNumbers(w));

        public static int GetNumWordsOnlyLettersAndDigitsAndUnderscore(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Count(w => IsOnlyLettersAndNumbersAndUnderscore(w));

        public static int GetNumNumbers(string text, int minWordLength = 1)
            => GetWordList(text, minWordLength).Count(w => IsNumber(w));

        public static string[] GetRomanNumbers(string text)
            => GetWordList(text).Where(w => IsRomanNumber(w)).ToArray();

        public static List<string> GetRomanNumbersList(string text)
            => GetWordList(text).Where(w => IsRomanNumber(w)).ToList();

        /// <summary>
        /// Splits text into tokens on non-word characters or underscore (<c>\W|_</c>).
        /// <para><i>[ES] Parte el texto en tokens por caracteres no-palabra o guion bajo (<c>\W|_</c>).</i></para>
        /// </summary>
        /// <remarks>
        /// Differs from <see cref="C_Hlp_String.GetWords"/>: this uses a regex split and may include empty entries.
        /// <para><i>[ES] Diferente de <see cref="C_Hlp_String.GetWords"/>: usa split por regex y puede incluir entradas vacías.</i></para>
        /// </remarks>
        public static string[] GetTokens(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<string>();

            return Regex.Split(text, @"\W|_");
        }

        #endregion
    }
}
