namespace _1_LibraryClasses.Random
{
    /// <summary>
    /// Helpers for random values (alphabetic tokens, and related utilities).
    /// <para><i>[ES] Ayudas para valores aleatorios (tokens alfabéticos y utilidades relacionadas).</i></para>
    /// </summary>
    public static class C_Hlp_Random
    {
#region PUBLIC METHODS — string

        /// <summary>
        /// Returns a random alphabetic string of the given length (A–Z, a–z only; no trailing underscore).
        /// <para><i>[ES] Devuelve una cadena alfabética aleatoria de la longitud dada (solo A–Z, a–z; sin guion bajo final).</i></para>
        /// </summary>
        /// <param name="length">
        /// Number of letters (default 8). Negative values are treated as 0.
        /// <para><i>[ES] Número de letras (por defecto 8). Los valores negativos se tratan como 0.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// string token = C_Hlp_Random.Alpha(8);           // e.g. "aBkXpQzW"
        /// string prefix = C_Hlp_Random.Alpha(8) + "_";  // e.g. "aBkXpQzW_"
        /// </code>
        /// </example>
        public static string Alpha(int length = 8)
        {
            if (length < 0)
                length = 0;

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] buffer = new char[length];
            for (int i = 0; i < length; i++)
                buffer[i] = chars[System.Random.Shared.Next(chars.Length)];
            return new string(buffer);
        }

#endregion
    }
}
