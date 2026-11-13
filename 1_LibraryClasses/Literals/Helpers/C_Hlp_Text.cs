using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Multi-token text helpers: capitalization, letter/digit runs, lines, tokens, reading time.
    /// Word units as <see cref="C_Word"/>: <see cref="C_Hlp_Word"/>. Raw word strings: <see cref="C_Hlp_String.GetWords"/>.
    /// <para><i>[ES] Texto multi-token: capitalización, runs de letras/dígitos, líneas, tokens, lectura.
    /// Palabras <see cref="C_Word"/>: <see cref="C_Hlp_Word"/>. Strings: <see cref="C_Hlp_String.GetWords"/>.</i></para>
    /// </summary>
    public static class C_Hlp_Text
    {
#region PUBLIC METHODS — reading time

        /// <summary>
        /// Counts words separated by whitespace.
        /// <para><i>[ES] Cuenta palabras separadas por espacios en blanco.</i></para>
        /// </summary>
        /// <summary>
        /// Number of word units. Delegates to <see cref="C_Hlp_Word.GetNumWords"/>.
        /// <para><i>[ES] Número de unidades palabra. Delega en <see cref="C_Hlp_Word.GetNumWords"/>.</i></para>
        /// </summary>
        public static int GetNumWords(string text, int minWordLength = 1)
            => C_Hlp_Word.GetNumWords(text, minWordLength);

        /// <summary>
        /// Number of letter-only runs. / Número de runs solo de letras.
        /// </summary>
        public static int GetNumWordsOnlyLetters(string text, int minWordLength = 1)
            => GetWordsOnlyLetters(text, minWordLength).Length;

        /// <summary>
        /// Number of digit-only runs. / Número de runs solo de dígitos.
        /// </summary>
        public static int GetNumWordsOnlyDigits(string text)
            => GetWordsOnlyDigits(text).Length;

        /// <summary>
        /// Returns the estimated reading time in minutes (default ~250 words/minute).
        /// <para><i>[ES] Devuelve el tiempo de lectura estimado en minutos (por defecto ~250 palabras/minuto).</i></para>
        /// </summary>
        public static double CalculateReadingTime(string text, double wordsPerMinute = 250.0)
        {
            return C_Hlp_Word.GetNumWords(text) / wordsPerMinute;
        }

        /// <summary>
        /// Returns the estimated reading time in ticks.
        /// <para><i>[ES] Devuelve el tiempo de lectura estimado en ticks.</i></para>
        /// </summary>
        public static long CalculateReadingTimeTicks(string text, double wordsPerMinute = 250.0)
        {
            return (long)(CalculateReadingTime(text, wordsPerMinute) * 60 * TimeSpan.TicksPerSecond);
        }

#endregion

#region PUBLIC METHODS — capitalize

        /// <summary>
        /// Capitalizes the first letter of each whitespace-separated word (title case for words longer than <paramref name="minWordLength"/>).
        /// <para><i>[ES] Pone en mayúscula la primera letra de cada palabra separada por espacios (title case si supera <paramref name="minWordLength"/>).</i></para>
        /// </summary>
        /// <example>
        /// <code>
        /// string s = C_Hlp_Text.CapitalizeWords("hello world"); // "Hello World"
        /// </code>
        /// </example>
        public static string CapitalizeWords(string input, int minWordLength = 1)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > minWordLength)
                    words[i] = textInfo.ToTitleCase(words[i]);
            }

            return string.Join(" ", words);
        }

#endregion

#region PUBLIC METHODS — words (letters only)

        /// <summary>
        /// Extracts letter-only word runs as a <c>string[]</c>.
        /// <para><i>[ES] Extrae secuencias de solo letras como <c>string[]</c>.</i></para>
        /// </summary>
        /// <param name="text">Input text. / Texto de entrada.</param>
        /// <param name="minWordLength">
        /// Minimum length of a word to keep (1 = keep single letters).
        /// <para><i>[ES] Longitud mínima de palabra a conservar (1 = conserva letras sueltas).</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// string[] words = C_Hlp_String.GetWords("Hello, world!", minWordLength: 1);
        /// </code>
        /// </example>
        public static string[] GetWordsOnlyLetters(string text, int minWordLength = 1)
        {
            return GetWordsOnlyLettersList(text, minWordLength).ToArray();
        }

        /// <summary>
        /// Extracts letter-only word runs (digits are not words here; use <see cref="C_Hlp_String.GetWordList"/> for numbers as units).
        /// <para><i>[ES] Extrae secuencias de solo letras como <see cref="List{T}"/>.</i></para>
        /// </summary>
        public static List<string> GetWordsOnlyLettersList(string text, int minWordLength = 1)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(text))
                return result;

            StringBuilder word = new StringBuilder();
            int minLen = minWordLength - 1;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsLetter(c))
                {
                    word.Append(c);
                }
                else if (word.Length != 0)
                {
                    if (word.Length > minLen)
                        result.Add(word.ToString());
                    word.Clear();
                }
            }

            if (word.Length != 0 && word.Length > minLen)
                result.Add(word.ToString());

            return result;
        }

        /// <summary>
        /// Extracts runs of decimal digits as a <c>string[]</c>.
        /// <para><i>[ES] Extrae secuencias de dígitos decimales como <c>string[]</c>.</i></para>
        /// </summary>
        public static string[] GetWordsOnlyDigits(string text)
            => GetWordsOnlyDigitsList(text).ToArray();

        /// <summary>
        /// Extracts runs of decimal digits as a <see cref="List{T}"/>.
        /// <para><i>[ES] Extrae secuencias de dígitos decimales como <see cref="List{T}"/>.</i></para>
        /// </summary>
        public static List<string> GetWordsOnlyDigitsList(string text)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(text))
                return result;

            var digits = new System.Text.StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsDigit(c))
                {
                    digits.Append(c);
                }
                else if (digits.Length != 0)
                {
                    result.Add(digits.ToString());
                    digits.Clear();
                }
            }
            if (digits.Length != 0)
                result.Add(digits.ToString());
            return result;
        }

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

#region PUBLIC METHODS — words and digits

#endregion

#region PUBLIC METHODS — lines

        /// <summary>
        /// Splits text into lines (handles <c>\r\n</c>, <c>\n</c>, <c>\r</c>).
        /// <para><i>[ES] Parte el texto en líneas (soporta <c>\r\n</c>, <c>\n</c>, <c>\r</c>).</i></para>
        /// </summary>
        /// <param name="text">Input text. / Texto de entrada.</param>
        /// <param name="removeEmptyEntries">
        /// When <see langword="true"/>, empty lines are omitted.
        /// <para><i>[ES] Si es <see langword="true"/>, se omiten las líneas vacías.</i></para>
        /// </param>
        /// <returns>Array of lines. / Array de líneas.</returns>
        /// <example>
        /// <code>
        /// string[] lines = C_Hlp_Text.SplitLines("a\nb\nc");
        /// </code>
        /// </example>
        public static string[] SplitLines(string text, bool removeEmptyEntries = true)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<string>();

            StringSplitOptions options = removeEmptyEntries
                ? StringSplitOptions.RemoveEmptyEntries
                : StringSplitOptions.None;

            return text.Split(new[] { "\r\n", "\r", "\n" }, options);
        }

#endregion

    }
}
