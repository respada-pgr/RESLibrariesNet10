namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Directory helpers (exists, create, delete, list files).
    /// <para><i>[ES] Ayudas de directorio (existe, crear, borrar, listar ficheros).</i></para>
    /// </summary>
    public static class C_Hlp_Folder
    {
        /// <summary>
        /// Returns whether the directory exists.
        /// <para><i>[ES] Indica si el directorio existe.</i></para>
        /// </summary>
        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Creates the directory if it does not exist (no-op if null/empty or already exists).
        /// <para><i>[ES] Crea el directorio si no existe (no-op si es null/vacío o ya existe).</i></para>
        /// </summary>
        /// <param name="directoryPath">Directory path. / Ruta de directorio.</param>
        public static void Ensure(string? directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return;
            if (!Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// <para><i>[ES] Crea todos los directorios y subdirectorios de la ruta salvo que ya existan.</i></para>
        /// </summary>
        /// <returns>A <see cref="DirectoryInfo"/> for the path.</returns>
        public static DirectoryInfo Create(string path)
        {
            return Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Deletes the directory if it exists.
        /// <para><i>[ES] Borra el directorio si existe.</i></para>
        /// </summary>
        /// <param name="path">Directory path. / Ruta del directorio.</param>
        /// <param name="forced">If <see langword="true"/>, deletes contents recursively. / Si es <see langword="true"/>, borra el contenido de forma recursiva.</param>
        /// <returns><see langword="true"/> if a directory was deleted; otherwise <see langword="false"/>.</returns>
        public static bool Delete(string path, bool forced = false)
        {
            bool result = false;
            if (Exists(path))
            {
                Directory.Delete(path, forced);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Lists file names in a folder (not full paths).
        /// <para><i>[ES] Lista nombres de fichero de una carpeta (no rutas completas).</i></para>
        /// </summary>
        /// <param name="folderPath">Folder path. / Ruta de la carpeta.</param>
        /// <param name="includeHiddenFiles">Include hidden files. / Incluir ocultos.</param>
        /// <returns>File names; empty if the folder does not exist. / Nombres; vacío si la carpeta no existe.</returns>
        public static List<string> GetFileNames(string folderPath, bool includeHiddenFiles = false)
        {
            List<string> fileNames = new List<string>();

            if (Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (!includeHiddenFiles && (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue;
                    fileNames.Add(Path.GetFileName(file));
                }
            }
            else
            {
                Console.WriteLine("The specified folder does not exist.");
            }

            return fileNames;
        }

        /// <summary>
        /// Lists <see cref="FileInfo"/> entries in a folder.
        /// <para><i>[ES] Lista entradas <see cref="FileInfo"/> de una carpeta.</i></para>
        /// </summary>
        /// <param name="folderPath">Folder path. / Ruta de la carpeta.</param>
        /// <param name="includeHiddenFiles">Include hidden files. / Incluir ocultos.</param>
        public static List<FileInfo> GetFiles(string folderPath, bool includeHiddenFiles)
        {
            List<FileInfo> fileInfos = new List<FileInfo>();

            if (Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (!includeHiddenFiles && (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue;
                    fileInfos.Add(fileInfo);
                }
            }
            else
            {
                Console.WriteLine("The specified folder does not exist.");
            }

            return fileInfos;
        }
    }
}
