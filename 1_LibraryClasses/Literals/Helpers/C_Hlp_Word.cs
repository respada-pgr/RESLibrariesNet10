using System;
using System.Collections.Generic;
using System.Text;

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

        

        #endregion

    }
}
