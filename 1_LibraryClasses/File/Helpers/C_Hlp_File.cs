namespace _1_LibraryClasses.File
{
    /// <summary>
    /// File I/O helpers (exists, read, write bytes, copy, move, delete).
    /// <para><i>[ES] Ayudas de I/O de fichero (existe, leer, escribir bytes, copiar, mover, borrar).</i></para>
    /// </summary>
    /// <remarks>
    /// Text: <see cref="C_Hlp_FileText"/> / <see cref="C_Hlp_FileTextAsync"/>.
    /// Streams: <see cref="C_Hlp_FileStream"/> / <see cref="C_Hlp_FileStreamAsync"/>.
    /// Async bytes/copy: <see cref="C_Hlp_FileAsync"/>.
    /// <para><i>[ES] Texto: <see cref="C_Hlp_FileText"/> / <see cref="C_Hlp_FileTextAsync"/>.
    /// Streams: <see cref="C_Hlp_FileStream"/> / <see cref="C_Hlp_FileStreamAsync"/>.
    /// Bytes/copia async: <see cref="C_Hlp_FileAsync"/>.</i></para>
    /// </remarks>
    public static class C_Hlp_File
    {
        /// <summary>
        /// Returns whether the file exists.
        /// <para><i>[ES] Indica si el fichero existe.</i></para>
        /// </summary>
        public static bool Exists(string filePath)
        {
            if (filePath == null) return false;
            return File.Exists(filePath);
        }

        /// <summary>
        /// Ensures the parent folder of the file path exists.
        /// <para><i>[ES] Asegura que existe la carpeta padre de la ruta de fichero.</i></para>
        /// </summary>
        /// <param name="filePath">File path (directory part is ensured). / Ruta de fichero (se asegura el directorio).</param>
        public static void EnsureFolder(string filePath)
        {
            C_Hlp_Folder.Ensure(C_Hlp_Path.GetFolderPath(filePath));
        }

        /// <summary>
        /// Returns the file size in bytes, or -1 if missing or invalid.
        /// <para><i>[ES] Devuelve el tamaño en bytes, o -1 si no existe o la ruta no es válida.</i></para>
        /// </summary>
        public static long Size(string filePath)
        {
            long result = -1;
            if (filePath != null && Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                result = fileInfo.Length;
            }
            return result;
        }

        /// <summary>
        /// Reads the entire file as text, or <see langword="null"/> if it does not exist.
        /// <para><i>[ES] Lee todo el fichero como texto, o <see langword="null"/> si no existe.</i></para>
        /// </summary>
        /// <param name="filePath">File path. Example: "MyTest.txt" or @"c:\temp\MyTest.txt"</param>
        public static string Read(string filePath)
        {
            if (!Exists(filePath)) return null;
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Writes raw bytes (creates parent folder if needed; overwrites if the file exists).
        /// <para><i>[ES] Escribe bytes (crea la carpeta padre si hace falta; sobrescribe si existe).</i></para>
        /// </summary>
        public static void Write(byte[] bytes, string filePath)
        {
            C_Hlp_File.EnsureFolder(filePath);
            File.WriteAllBytes(filePath, bytes);
        }

        /// <summary>
        /// Changes the extension and renames on disk when the source file exists.
        /// <para><i>[ES] Cambia la extensión y renombra en disco si el origen existe.</i></para>
        /// </summary>
        /// <param name="newExtension">New extension (with or without leading dot). / Nueva extensión (con o sin punto).</param>
        /// <param name="deleteFileIfExist">Delete target if it already exists. / Borrar el destino si ya existe.</param>
        /// <returns>The new path (even if the source file did not exist).</returns>
        public static string ChangeExtension(string filePath, string newExtension, bool deleteFileIfExist = false)
        {
            if (string.IsNullOrEmpty(filePath)) return filePath;
            if (!newExtension.StartsWith(".")) newExtension = "." + newExtension;
            string newPath = Path.ChangeExtension(filePath, newExtension);
            if (Exists(filePath))
            {
                if (Exists(newPath) && deleteFileIfExist)
                    Delete(newPath);
                File.Move(filePath, newPath);
            }
            return newPath;
        }

        /// <summary>
        /// Renames a file within the same folder (generates a numbered name if the target exists and is not deleted).
        /// <para><i>[ES] Renombra un fichero en la misma carpeta (nombre numerado si el destino existe y no se borra).</i></para>
        /// </summary>
        /// <returns>The resulting path after rename.</returns>
        public static string Rename(string filePath, string newFileName, bool deleteFileIfExist = false)
        {
            string? directory = C_Hlp_Path.GetFolderPath(filePath);
            string newPath = string.IsNullOrEmpty(directory)
                ? newFileName
                : Path.Combine(directory, newFileName);

            if (Exists(filePath))
            {
                if (Exists(newPath))
                {
                    if (deleteFileIfExist)
                        Delete(newPath);
                    else
                    {
                        string name = C_Hlp_Path.GetFileNameWithoutExtension(newPath) ?? "file";
                        string ext = C_Hlp_Path.GetExtensionWithDot(newPath) ?? "";
                        int counter = 1;
                        string candidate = newPath;
                        while (Exists(candidate))
                        {
                            string numbered = $"{name} ({counter}){ext}";
                            candidate = string.IsNullOrEmpty(directory)
                                ? numbered
                                : Path.Combine(directory, numbered);
                            counter++;
                        }
                        newPath = candidate;
                    }
                }
                File.Move(filePath, newPath);
            }
            return newPath;
        }

        /// <summary>
        /// Copies a file. If the destination exists and <paramref name="overwrite"/> is false, uses a numbered name.
        /// <para><i>[ES] Copia un fichero. Si el destino existe y <paramref name="overwrite"/> es false, usa un nombre numerado.</i></para>
        /// </summary>
        public static void Copy(string sourceFilePath, string destinationFilePath, bool overwrite = false)
        {
            string fullSource = Path.GetFullPath(sourceFilePath);
            string fullDest = Path.GetFullPath(destinationFilePath);

            if (Exists(fullDest) && !overwrite)
            {
                string? directory = Path.GetDirectoryName(fullDest);
                string name = Path.GetFileNameWithoutExtension(fullDest);
                string ext = Path.GetExtension(fullDest);
                int counter = 1;
                string candidate = fullDest;
                while (Exists(candidate))
                {
                    candidate = Path.Combine(directory ?? "", $"{name} ({counter}){ext}");
                    counter++;
                }
                fullDest = candidate;
            }

            C_Hlp_File.EnsureFolder(fullDest);
            File.Copy(fullSource, fullDest, overwrite);
        }

        /// <summary>
        /// Moves a file to a new path. Optionally deletes or renumbers an existing destination.
        /// <para><i>[ES] Mueve un fichero a una nueva ruta. Opcionalmente borra o numera un destino existente.</i></para>
        /// </summary>
        public static void Move(string sourceFilePath, string destinationFilePath, bool deleteDestinationFileIfExist = false)
        {
            string sourceFull = Path.GetFullPath(sourceFilePath);
            string destFull = Path.GetFullPath(destinationFilePath);

            if (Exists(destFull))
            {
                if (deleteDestinationFileIfExist)
                    Delete(destFull);
                else
                {
                    string? directory = Path.GetDirectoryName(destFull);
                    string name = Path.GetFileNameWithoutExtension(destFull);
                    string ext = Path.GetExtension(destFull);
                    int counter = 1;
                    string candidate = destFull;
                    while (Exists(candidate))
                    {
                        candidate = Path.Combine(directory ?? "", $"{name} ({counter}){ext}");
                        counter++;
                    }
                    File.Move(destFull, candidate);
                }
            }

            C_Hlp_File.EnsureFolder(destFull);
            File.Move(sourceFull, destFull);
        }

        /// <summary>
        /// Deletes the file if it exists.
        /// <para><i>[ES] Borra el fichero si existe.</i></para>
        /// </summary>
        public static void Delete(string filePath)
        {
            if (Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// Copies a file via streams. Prefer <see cref="C_Hlp_FileStream.Copy"/> for stream-level API.
        /// <para><i>[ES] Copia un fichero mediante streams. Preferir <see cref="C_Hlp_FileStream.Copy"/> para la API de streams.</i></para>
        /// </summary>
        /// <returns>Bytes copied, or 0 if the origin does not exist.</returns>
        public static long ExportToFile(string originPath, string destinationPath)
        {
            return C_Hlp_FileStream.Copy(originPath, destinationPath);
        }
    }
}
