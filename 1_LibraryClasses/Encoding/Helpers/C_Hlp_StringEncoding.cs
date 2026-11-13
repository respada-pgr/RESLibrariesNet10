using System.Text;

namespace _1_LibraryClasses.Encoding
{
    /// <summary>
    /// Encoding helpers whose primary input is a <see cref="string"/> (to bytes or Base64 text).
    /// <para>
    /// For operations whose primary input is <c>byte[]</c>, use <see cref="C_Hlp_ByteEncoding"/>.
    /// </para>
    /// <para><i>[ES] Ayudas de codificación cuyo input principal es una <see cref="string"/> (a bytes o Base64).
    /// Si el input principal es <c>byte[]</c>, use <see cref="C_Hlp_ByteEncoding"/>.</i></para>
    /// </summary>
    public static class C_Hlp_StringEncoding
    {
#region PUBLIC METHODS — UTF-8 / ASCII bytes

        /// <summary>
        /// Encodes text as UTF-8 bytes.
        /// <para><i>[ES] Codifica el texto en bytes UTF-8.</i></para>
        /// </summary>
        public static byte[] ToUtf8Bytes(string? text)
        {
            if (text is null)
                return Array.Empty<byte>();
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// Encodes text as ASCII bytes.
        /// <para><i>[ES] Codifica el texto en bytes ASCII.</i></para>
        /// </summary>
        public static byte[] ToAsciiBytes(string? text)
        {
            if (text is null)
                return Array.Empty<byte>();
            return Encoding.ASCII.GetBytes(text);
        }

#endregion

#region PUBLIC METHODS — Base64 (text)

        /// <summary>
        /// Encodes text to Base64 (UTF-8 payload).
        /// <para><i>[ES] Codifica texto a Base64 (carga UTF-8).</i></para>
        /// </summary>
        public static string EncodeBase64(string? text)
        {
            if (text is null)
                return string.Empty;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Decodes a Base64 string to text (UTF-8).
        /// <para><i>[ES] Decodifica una cadena Base64 a texto (UTF-8).</i></para>
        /// </summary>
        public static string DecodeBase64(string? base64)
        {
            if (string.IsNullOrEmpty(base64))
                return string.Empty;
            byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

#endregion
    }
}
