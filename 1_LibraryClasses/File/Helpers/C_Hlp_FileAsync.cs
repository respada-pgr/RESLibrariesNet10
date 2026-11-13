namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Asynchronous file helpers (bytes read/write, copy).
    /// <para>
    /// Prefer <see cref="C_Hlp_File"/> for synchronous existence/size checks.
    /// Text: <see cref="C_Hlp_FileTextAsync"/>. Streams: <see cref="C_Hlp_FileStreamAsync"/>.
    /// </para>
    /// <para><i>[ES] Ayudas asíncronas de fichero (lectura/escritura de bytes, copia).
    /// Existencia/tamaño síncronos: <see cref="C_Hlp_File"/>.
    /// Texto: <see cref="C_Hlp_FileTextAsync"/>. Streams: <see cref="C_Hlp_FileStreamAsync"/>.</i></para>
    /// </summary>
    public static class C_Hlp_FileAsync
    {
#region PUBLIC METHODS — read / write bytes

        /// <summary>
        /// Reads the entire file as a byte array, or <see langword="null"/> if it does not exist.
        /// <para><i>[ES] Lee todo el fichero como array de bytes, o <see langword="null"/> si no existe.</i></para>
        /// </summary>
        public static async Task<byte[]?> ReadBytesAsync(string filePath, CancellationToken cancellationToken = default)
        {
            if (!C_Hlp_File.Exists(filePath))
                return null;
            return await File.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes raw bytes (creates parent folder if needed; overwrites if the file exists).
        /// <para><i>[ES] Escribe bytes (crea la carpeta padre si hace falta; sobrescribe si existe).</i></para>
        /// </summary>
        public static async Task WriteBytesAsync(byte[] bytes, string filePath, CancellationToken cancellationToken = default)
        {
            C_Hlp_File.EnsureFolder(filePath);
            await File.WriteAllBytesAsync(filePath, bytes ?? Array.Empty<byte>(), cancellationToken).ConfigureAwait(false);
        }

#endregion

#region PUBLIC METHODS — copy

        /// <summary>
        /// Copies a file asynchronously. Returns the number of bytes written, or -1 if the source is missing.
        /// <para><i>[ES] Copia un fichero de forma asíncrona. Devuelve los bytes escritos, o -1 si falta el origen.</i></para>
        /// </summary>
        public static Task<long> CopyAsync(string originPath, string destinationPath, bool overwrite = true, CancellationToken cancellationToken = default)
            => C_Hlp_FileStreamAsync.CopyAsync(originPath, destinationPath, overwrite, cancellationToken);

#endregion
    }
}
