using System;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Static helpers for a single <see cref="char"/> and <see cref="C_Char"/> factory.
    /// <para><i>[ES] Ayudas de <see cref="char"/> y factoría de <see cref="C_Char"/>.</i></para>
    /// </summary>
    public static class C_Hlp_Char
    {
#region PUBLIC METHODS — classification

        public static bool IsDigit(char c) => char.IsDigit(c);
        public static bool IsAsciiDigit(char c) => c >= '0' && c <= '9';
        public static bool IsLetter(char c) => char.IsLetter(c);
        public static bool IsLetterOrDigit(char c) => char.IsLetterOrDigit(c);
        public static bool IsWhiteSpace(char c) => char.IsWhiteSpace(c);
        public static bool IsUpper(char c) => char.IsUpper(c);
        public static bool IsLower(char c) => char.IsLower(c);
        public static bool IsPunctuation(char c) => char.IsPunctuation(c);
        public static bool IsSymbol(char c) => char.IsSymbol(c);
        public static bool IsControl(char c) => char.IsControl(c);
        public static bool IsSeparator(char c) => char.IsSeparator(c);
        public static bool IsNumber(char c) => char.IsNumber(c);

#endregion

#region PUBLIC METHODS — convert

        public static char ToUpper(char c) => char.ToUpperInvariant(c);
        public static char ToLower(char c) => char.ToLowerInvariant(c);

        public static bool TryParse(string? text, out char value)
        {
            if (string.IsNullOrEmpty(text))
            {
                value = default;
                return false;
            }
            value = text[0];
            return true;
        }

        public static bool TryToInt(char c, out int value)
        {
            if (IsAsciiDigit(c))
            {
                value = c - '0';
                return true;
            }
            value = 0;
            return false;
        }

        public static int ToInt(char c)
        {
            if (!TryToInt(c, out int value))
                throw new FormatException("Character is not an ASCII digit.");
            return value;
        }

#endregion

#region PUBLIC METHODS — factory

        public static C_Char Create(char value) => new C_Char(value);

        public static C_Char Create(string text)
        {
            if (!TryParse(text, out char c))
                throw new ArgumentException("Text is empty; cannot create a character.", nameof(text));
            return new C_Char(c);
        }

        /// <summary>
        /// Tries to create from the first character of text; <see langword="null"/> if empty.
        /// </summary>
        public static C_Char? TryCreate(string? text)
            => TryParse(text, out char c) ? new C_Char(c) : null;

        public static C_Char Create(C_Char? source) => new C_Char(source);

#endregion
    }
}
