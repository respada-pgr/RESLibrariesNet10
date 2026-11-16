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

        public static C_Word[] GetWords(string text, int minWordLength = 1)
            => C_Hlp_String.GetWords(text, minWordLength).Select(w => new C_Word(w)).ToArray();

        public static List<C_Word> GetWordList(string text, int minWordLength = 1)
            => C_Hlp_String.GetWords(text, minWordLength).Select(w => new C_Word(w)).ToList();


        

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
