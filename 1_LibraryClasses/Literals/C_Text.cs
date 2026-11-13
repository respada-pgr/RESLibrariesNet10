using System;
using System.Collections.Generic;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Mutable multi-word text extending <see cref="C_String"/>.
    /// Word extraction via <see cref="C_Hlp_Word"/> / <see cref="C_Hlp_String"/>;
    /// lines, capitalization and reading time via <see cref="C_Hlp_Text"/>.
    /// <para><i>[ES] Texto multi-palabra mutable que amplía <see cref="C_String"/>.
    /// Palabras: <see cref="C_Hlp_Word"/> / <see cref="C_Hlp_String"/>;
    /// líneas y lectura: <see cref="C_Hlp_Text"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var t = new C_Text("Hello 123 world");
    /// C_Word[] words = t.Words;
    /// t.CapitalizeWords().CleanCrLf();
    /// </code>
    /// </example>
    public class C_Text : C_String
    {
#region CONSTRUCTORS

        public C_Text()
        {
        }

        public C_Text(string? value)
            : base(value)
        {
        }

        public C_Text(C_String? source)
            : base(source)
        {
        }

        /// <summary>Copy constructor. / Constructor de copia.</summary>
        public C_Text(C_Text? source)
            : base(source)
        {
        }

#endregion

#region PUBLIC PROPERTIES — words

        /// <summary>
        /// Word units as <see cref="C_Word"/> (letters + digits, min length 1).
        /// <para><i>[ES] Unidades palabra como <see cref="C_Word"/> (letras + dígitos).</i></para>
        /// </summary>
        public C_Word[] Words => C_Hlp_Word.GetWords(this.Value, 1);

        /// <summary>
        /// Number of word units. / Número de unidades palabra.
        /// </summary>
        public int NumWords => C_Hlp_Word.GetNumWords(this.Value, 1);

#endregion

#region PUBLIC METHODS — words

        /// <summary>
        /// Word units as <see cref="C_Word"/>[].
        /// <para><i>[ES] Unidades palabra como <see cref="C_Word"/>[].</i></para>
        /// </summary>
        public C_Word[] GetWords(int minWordLength = 1)
            => C_Hlp_Word.GetWords(this.Value, minWordLength);

        /// <summary>
        /// Word units as <see cref="List{T}"/> of <see cref="C_Word"/>.
        /// <para><i>[ES] Unidades palabra como lista de <see cref="C_Word"/>.</i></para>
        /// </summary>
        public List<C_Word> GetWordList(int minWordLength = 1)
            => C_Hlp_Word.GetWordList(this.Value, minWordLength);

        /// <summary>
        /// Word tokens as raw <c>string[]</c>.
        /// <para><i>[ES] Tokens palabra como <c>string[]</c>.</i></para>
        /// </summary>
        public string[] GetWordStrings(int minWordLength = 1)
            => C_Hlp_String.GetWords(this.Value, minWordLength);

        /// <summary>
        /// Word tokens as <see cref="List{T}"/> of strings.
        /// <para><i>[ES] Tokens palabra como lista de string.</i></para>
        /// </summary>
        public List<string> GetWordStringList(int minWordLength = 1)
            => C_Hlp_String.GetWordList(this.Value, minWordLength);

        public int GetNumWords(int minWordLength = 1)
            => C_Hlp_Word.GetNumWords(this.Value, minWordLength);

        public string[] GetWordsOnlyLetters(int minWordLength = 1)
            => C_Hlp_Text.GetWordsOnlyLetters(this.Value, minWordLength);

        public List<string> GetWordsOnlyLettersList(int minWordLength = 1)
            => C_Hlp_Text.GetWordsOnlyLettersList(this.Value, minWordLength);

        public int GetNumWordsOnlyLetters(int minWordLength = 1)
            => C_Hlp_Text.GetNumWordsOnlyLetters(this.Value, minWordLength);

        public string[] GetWordsOnlyDigits()
            => C_Hlp_Text.GetWordsOnlyDigits(this.Value);

        public List<string> GetWordsOnlyDigitsList()
            => C_Hlp_Text.GetWordsOnlyDigitsList(this.Value);

        public int GetNumWordsOnlyDigits()
            => C_Hlp_Text.GetNumWordsOnlyDigits(this.Value);

        public string[] GetTokens()
            => C_Hlp_Text.GetTokens(this.Value);

        public string[] SplitLines(bool removeEmptyEntries = true)
            => C_Hlp_Text.SplitLines(this.Value, removeEmptyEntries);

#endregion

#region PUBLIC METHODS — transform in-place

        public C_Text CapitalizeWords(int minWordLength = 1)
        {
            this.Value = C_Hlp_Text.CapitalizeWords(this.Value, minWordLength);
            return this;
        }

        public new C_Text CleanCrLf()
        {
            base.CleanCrLf();
            return this;
        }

        public new C_Text CleanCrLfAndTabs()
        {
            base.CleanCrLfAndTabs();
            return this;
        }

        public new C_Text FirstCharToUpper()
        {
            base.FirstCharToUpper();
            return this;
        }

        public new C_Text NullToEmpty()
        {
            base.NullToEmpty();
            return this;
        }

#endregion

#region PUBLIC METHODS — reading time

        public double CalculateReadingTime(double wordsPerMinute = 250.0)
            => C_Hlp_Text.CalculateReadingTime(this.Value, wordsPerMinute);

        public long CalculateReadingTimeTicks(double wordsPerMinute = 250.0)
            => C_Hlp_Text.CalculateReadingTimeTicks(this.Value, wordsPerMinute);

#endregion

#region PUBLIC METHODS

        public new C_Text Clone()
            => new C_Text(this);

        public override string ToString()
            => this.Value;

        public new string Info()
            => $"C_Text Length={this.Length}, NumWords={this.NumWords}, Value=\"{this.Value}\"";

#endregion

#region OPERATORS

        public static implicit operator C_Text(string? s)
            => new C_Text(s);

        public static implicit operator string(C_Text t)
            => t?.Value ?? string.Empty;

#endregion
    }
}
