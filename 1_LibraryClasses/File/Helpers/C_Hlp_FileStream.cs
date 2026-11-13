namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Low-level file stream helpers (open, copy via streams).
    /// <para><i>[ES] Ayudas de bajo nivel sobre streams de fichero (abrir, copiar por streams).</i></para>
    /// </summary>
    public static class C_Hlp_FileStream
    {
        /// <summary>
        /// Opens an existing file for reading.
        /// <para><i>[ES] Abre un fichero existente para lectura.</i></para>
        /// </summary>
        /// <param name="filePath">Existing file path. / Ruta de un fichero existente.</param>
        /// <returns>A readable <see cref="FileStream"/>. Caller must dispose. / <see cref="FileStream"/> de lectura. El llamador debe hacer dispose.</returns>
        public static FileStream OpenRead(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// Creates or overwrites a file for writing. Creates the directory if needed.
        /// <para><i>[ES] Crea o sobrescribe un fichero para escritura. Crea el directorio si hace falta.</i></para>
        /// </summary>
        /// <param name="filePath">Target file path. / Ruta de destino.</param>
        /// <returns>A writable <see cref="FileStream"/>. Caller must dispose.</returns>
        public static FileStream OpenWrite(string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            return new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        }

        /// <summary>
        /// Opens a file for appending text/bytes at the end. Creates the file and directory if needed.
        /// <para><i>[ES] Abre un fichero para añadir al final. Crea fichero y directorio si hace falta.</i></para>
        /// </summary>
        /// <param name="filePath">Target file path. / Ruta de destino.</param>
        /// <returns>A writable <see cref="FileStream"/> positioned at end of file.</returns>
        public static FileStream OpenAppend(string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            return new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
        }

        /// <summary>
        /// Opens or creates a file for read/write (e.g. inspect end then append).
        /// <para><i>[ES] Abre o crea un fichero para lectura/escritura (p. ej. mirar el final y añadir).</i></para>
        /// </summary>
        /// <param name="filePath">Target file path. / Ruta de destino.</param>
        /// <returns>A read/write <see cref="FileStream"/>. Caller must dispose.</returns>
        public static FileStream OpenReadWrite(string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            return new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        }

        /// <summary>
        /// Copies an origin file to a destination using streams. Returns the number of bytes copied.
        /// <para><i>[ES] Copia un fichero origen a destino usando streams. Devuelve los bytes copiados.</i></para>
        /// </summary>
        /// <param name="originPath">Source file. / Fichero origen.</param>
        /// <param name="destinationPath">Destination file (created/overwritten). / Destino (creado/sobrescrito).</param>
        /// <returns>Bytes copied, or 0 if the origin does not exist. / Bytes copiados, o 0 si el origen no existe.</returns>
        public static long Copy(string originPath, string destinationPath)
        {
            long result = 0;
            if (!C_Hlp_File.Exists(originPath))
                return result;

            using (FileStream source = OpenRead(originPath))
            {
                result = source.Length;
                using (FileStream output = OpenWrite(destinationPath))
                {
                    source.CopyTo(output);
                }
            }
            return result;
        }

    }
}
