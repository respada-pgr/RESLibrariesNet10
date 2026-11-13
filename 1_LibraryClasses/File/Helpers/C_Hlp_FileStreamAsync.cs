namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Asynchronous stream helpers for files (open with async I/O, copy).
    /// <para><i>[ES] Ayudas asíncronas de streams de fichero (apertura con I/O async, copia).</i></para>
    /// </summary>
    public static class C_Hlp_FileStreamAsync
    {
#region PUBLIC METHODS — open

        /// <summary>
        /// Opens an existing file for asynchronous reading.
        /// <para><i>[ES] Abre un fichero existente para lectura asíncrona.</i></para>
        /// </summary>
        public static FileStream OpenRead(string filePath)
        {
            return new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);
        }

        /// <summary>
        /// Opens or creates a file for asynchronous writing (truncates if exists).
        /// <para><i>[ES] Abre o crea un fichero para escritura asíncrona (trunca si existe).</i></para>
        /// </summary>
        public static FileStream OpenWrite(string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            return new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);
        }

        /// <summary>
        /// Opens or creates a file for asynchronous append.
        /// <para><i>[ES] Abre o crea un fichero para anexado asíncrono.</i></para>
        /// </summary>
        public static FileStream OpenAppend(string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            return new FileStream(
                filePath,
                FileMode.Append,
                FileAccess.Write,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);
        }

#endregion

#region PUBLIC METHODS — copy

        /// <summary>
        /// Copies a file using asynchronous streams. Returns bytes written, or -1 if the source is missing.
        /// <para><i>[ES] Copia un fichero con streams asíncronos. Devuelve bytes escritos, o -1 si falta el origen.</i></para>
        /// </summary>
        public static async Task<long> CopyAsync(
            string originPath,
            string destinationPath,
            bool overwrite = true,
            CancellationToken cancellationToken = default)
        {
            if (!C_Hlp_File.Exists(originPath))
                return -1;

            if (!overwrite && C_Hlp_File.Exists(destinationPath))
                return -1;

            C_Hlp_File.EnsureFolder(destinationPath);

            await using FileStream source = OpenRead(originPath);
            await using FileStream output = OpenWrite(destinationPath);
            await source.CopyToAsync(output, cancellationToken).ConfigureAwait(false);
            return output.Length;
        }

#endregion
    }
}
