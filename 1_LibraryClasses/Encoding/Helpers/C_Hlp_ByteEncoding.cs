using System.Text;

namespace _1_LibraryClasses.Encoding
{
    /// <summary>
    /// Encoding helpers whose primary input is a <c>byte[]</c> (to string, hex or Base64).
    /// <para>
    /// For operations whose primary input is a <see cref="string"/>, use <see cref="C_Hlp_StringEncoding"/>.
    /// </para>
    /// <para><i>[ES] Ayudas de codificación cuyo input principal es un <c>byte[]</c> (a string, hex o Base64).
    /// Si el input principal es <see cref="string"/>, use <see cref="C_Hlp_StringEncoding"/>.</i></para>
    /// </summary>
    public static class C_Hlp_ByteEncoding
    {
#region PUBLIC METHODS — to string

        /// <summary>
        /// Decodes UTF-8 bytes to a string.
        /// <para><i>[ES] Decodifica bytes UTF-8 a cadena.</i></para>
        /// </summary>
        public static string ToUtf8String(byte[]? bytes)
        {
            if (bytes is null || bytes.Length == 0)
                return string.Empty;
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Decodes ASCII bytes to a string.
        /// <para><i>[ES] Decodifica bytes ASCII a cadena.</i></para>
        /// </summary>
        public static string ToAsciiString(byte[]? bytes)
        {
            if (bytes is null || bytes.Length == 0)
                return string.Empty;
            return Encoding.ASCII.GetString(bytes);
        }

#endregion

#region PUBLIC METHODS — hex

        /// <summary>
        /// Converts bytes to a continuous uppercase hex string (no separators).
        /// <para><i>[ES] Convierte bytes a cadena hexadecimal continua en mayúsculas (sin separadores).</i></para>
        /// </summary>
        /// <example>
        /// <code>
        /// string hex = C_Hlp_ByteEncoding.ToHex(new byte[] { 0x0A, 0xFF }); // "0AFF"
        /// </code>
        /// </example>
        public static string ToHex(byte[]? bytes)
        {
            if (bytes is null || bytes.Length == 0)
                return string.Empty;
            return BitConverter.ToString(bytes).Replace("-", "");
        }

#endregion

#region PUBLIC METHODS — Base64 (bytes)

        /// <summary>
        /// Encodes bytes to a Base64 string.
        /// <para><i>[ES] Codifica bytes a una cadena Base64.</i></para>
        /// </summary>
        public static string ToBase64(byte[]? bytes)
        {
            if (bytes is null || bytes.Length == 0)
                return string.Empty;
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodes a Base64 string to bytes.
        /// <para><i>[ES] Decodifica una cadena Base64 a bytes.</i></para>
        /// </summary>
        public static byte[] FromBase64(string? base64)
        {
            if (string.IsNullOrEmpty(base64))
                return Array.Empty<byte>();
            return Convert.FromBase64String(base64);
        }

#endregion
    }
}
