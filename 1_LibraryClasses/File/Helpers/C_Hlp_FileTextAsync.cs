using System.Text;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Asynchronous text file helpers (read/write/append UTF-8 by default).
    /// <para><i>[ES] Ayudas asíncronas de ficheros de texto (lectura/escritura/anexado; UTF-8 por defecto).</i></para>
    /// </summary>
    public static class C_Hlp_FileTextAsync
    {
#region PUBLIC METHODS — read

        /// <summary>
        /// Reads the entire file as text, or <see langword="null"/> if it does not exist.
        /// <para><i>[ES] Lee todo el fichero como texto, o <see langword="null"/> si no existe.</i></para>
        /// </summary>
        public static async Task<string?> ReadAsync(string filePath, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            if (!C_Hlp_File.Exists(filePath))
                return null;
            return await File.ReadAllTextAsync(filePath, encoding ?? Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads all lines from the file, or an empty array if it does not exist.
        /// <para><i>[ES] Lee todas las líneas del fichero, o un array vacío si no existe.</i></para>
        /// </summary>
        public static async Task<string[]> ReadLinesAsync(string filePath, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            if (!C_Hlp_File.Exists(filePath))
                return Array.Empty<string>();
            return await File.ReadAllLinesAsync(filePath, encoding ?? Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

#endregion

#region PUBLIC METHODS — write

        /// <summary>
        /// Writes or overwrites the file with text (creates parent folder if needed).
        /// <para><i>[ES] Escribe o sobrescribe el fichero con texto (crea la carpeta padre si hace falta).</i></para>
        /// </summary>
        public static async Task WriteAsync(string text, string filePath, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            C_Hlp_File.EnsureFolder(filePath);
            await File.WriteAllTextAsync(filePath, text ?? string.Empty, encoding ?? Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes all lines (creates parent folder if needed; overwrites).
        /// <para><i>[ES] Escribe todas las líneas (crea la carpeta padre si hace falta; sobrescribe).</i></para>
        /// </summary>
        public static async Task WriteAsync(IEnumerable<string> lines, string filePath, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            C_Hlp_File.EnsureFolder(filePath);
            await File.WriteAllLinesAsync(filePath, lines ?? Array.Empty<string>(), encoding ?? Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

#endregion

#region PUBLIC METHODS — append

        /// <summary>
        /// Appends a line of text to the file (creates parent folder and file if needed).
        /// <para><i>[ES] Añade una línea de texto al fichero (crea carpeta y fichero si hace falta).</i></para>
        /// </summary>
        public static async Task AddLineAsync(string text, string filePath, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            C_Hlp_File.EnsureFolder(filePath);
            await File.AppendAllTextAsync(filePath, (text ?? string.Empty) + Environment.NewLine, encoding ?? Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

#endregion
    }
}
