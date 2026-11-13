using System;
using System.Collections.Generic;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Static helpers for words: identity, classification, parse, and extraction as <see cref="C_Word"/>.
    /// String token extraction lives in <see cref="C_Hlp_String"/>.
    /// <para><i>[ES] Ayudas de palabras: identidad, clasificación, parseo y extracción como <see cref="C_Word"/>.
    /// Tokens string en <see cref="C_Hlp_String"/>.</i></para>
    /// </summary>
    public static class C_Hlp_Word
    {
#region PUBLIC METHODS — identity

        public static bool IsWord(string? text)
            => C_Hlp_String.IsWord(text);

        public static string? AsWordOrNull(string? text)
            => IsWord(text) ? text : null;

        public static string RequireWord(string? text, string? paramName = null)
        {
            if (!IsWord(text))
                throw new ArgumentException("Value must be a single non-empty word without whitespace.", paramName ?? nameof(text));
            return text!;
        }

#endregion

#region PUBLIC METHODS — classification

        public static bool IsDigits(string? word) => C_Hlp_String.IsDigits(word);
        public static bool IsInteger(string? word) => C_Hlp_String.IsInteger(word);
        public static bool IsDecimal(string? word) => C_Hlp_String.IsDecimal(word);
        public static bool IsNumber(string? word) => C_Hlp_String.IsNumber(word);
        public static bool IsOnlyLetters(string? word) => C_Hlp_String.IsOnlyLetters(word);
        public static bool IsOnlyLettersAndNumbers(string? word) => C_Hlp_String.IsOnlyLettersAndNumbers(word);
        public static bool IsOnlyLettersAndNumbersAndUnderscore(string? word) => C_Hlp_String.IsOnlyLettersAndNumbersAndUnderscore(word);
        public static bool IsEmoji(string? word) => C_Hlp_String.IsEmoji(word);
        public static bool ContainsEmoji(string? word) => C_Hlp_String.ContainsEmoji(word);
        public static bool ContainsDigit(string? word) => C_Hlp_String.ContainsDigit(word);
        public static bool ContainsLetter(string? word) => C_Hlp_String.ContainsLetter(word);
        public static bool ContainsLetterOrDigit(string? word) => C_Hlp_String.ContainsLetterOrDigit(word);
        public static bool ContainsWhiteSpace(string? word) => C_Hlp_String.ContainsWhiteSpace(word);

#endregion

#region PUBLIC METHODS — parse

        public static bool TryToInt(string? word, out int value)
            => C_Hlp_String.TryToInt(word, out value);

        public static bool TryToLong(string? word, out long value)
            => C_Hlp_String.TryToLong(word, out value);

        public static bool TryToDouble(string? word, out double value)
            => C_Hlp_String.TryToDouble(word, out value);

        public static bool TryToDecimal(string? word, out decimal value)
            => C_Hlp_String.TryToDecimal(word, out value);

#endregion

#region PUBLIC METHODS — extract from text

        /// <summary>
        /// Extracts word units from text as <see cref="C_Word"/>[].
        /// <para><i>[ES] Extrae unidades palabra del texto como <see cref="C_Word"/>[].</i></para>
        /// </summary>
        public static C_Word[] GetWords(string text, int minWordLength = 1)
        {
            string[] raw = C_Hlp_String.GetWords(text, minWordLength);
            var result = new C_Word[raw.Length];
            for (int i = 0; i < raw.Length; i++)
                result[i] = new C_Word(raw[i]);
            return result;
        }

        /// <summary>
        /// Extracts word units from text as <see cref="List{T}"/> of <see cref="C_Word"/>.
        /// <para><i>[ES] Extrae unidades palabra como lista de <see cref="C_Word"/>.</i></para>
        /// </summary>
        public static List<C_Word> GetWordList(string text, int minWordLength = 1)
        {
            string[] raw = C_Hlp_String.GetWords(text, minWordLength);
            var result = new List<C_Word>(raw.Length);
            for (int i = 0; i < raw.Length; i++)
                result.Add(new C_Word(raw[i]));
            return result;
        }

        public static int GetNumWords(string text, int minWordLength = 1)
            => C_Hlp_String.GetWords(text, minWordLength).Length;

#endregion

#region PUBLIC METHODS — factory

        public static bool TryCreate(string? text, out C_Word? word)
            => C_Word.TryCreate(text, out word);

        public static C_Word Create(string text)
            => new C_Word(RequireWord(text));

#endregion
    }
}
