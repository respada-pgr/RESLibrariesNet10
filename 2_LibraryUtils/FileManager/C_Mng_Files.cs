using _2_LibraryUtils.Logger;
using System.Text;
using System.Text.RegularExpressions;


namespace _2_LibraryUtils.FileManager
{
    public static class C_Mng_Files
    {
        public static string NormalizeName(string name, string replaceCharBy = " ")
        {
            // Reemplazar caracteres no permitidos por replaceCharBy
            //string nameWithoutInvalidCharacters = Regex.Replace(name, @"[\\/:*?""<>|]", replaceCharBy);
            string pattern = $"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]";
            string nameWithoutInvalidCharacters = Regex.Replace(name, pattern, replaceCharBy);


            // Recortar longitud a 255 caracteres
            if (nameWithoutInvalidCharacters.Length > 255)
            {
                nameWithoutInvalidCharacters = nameWithoutInvalidCharacters.Substring(0, 255);
            }

            // Remover espacios y puntos al final
            nameWithoutInvalidCharacters = nameWithoutInvalidCharacters.TrimEnd(' ', '.');

            // Eliminar caracteres de control invisibles
            nameWithoutInvalidCharacters = new string(nameWithoutInvalidCharacters.Where(c => !char.IsControl(c)).ToArray());

            return nameWithoutInvalidCharacters;
        }

        public static bool Exists(string filePath)
        {
            if (filePath == null) return false;
            return File.Exists(filePath);
        }

        public static string GetFullPath(string filePath)
        {
            if (filePath == null) return null;
            return Path.GetFullPath(filePath);
        }

        public static string GetFolderPath(string filePath)
        {
            if (filePath == null) return null;
            return Path.GetDirectoryName(filePath);
        }

        public static string GetFileName(string filePath)
        {
            if (filePath == null) return null;
            return Path.GetFileName(filePath);
        }

        public static string GetFileNameWithoutExtension(string filePath)
        {
            if (filePath == null) return null;
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static string GetExtension(string filePath)
        {
            if (filePath == null) return null;
            if (!Path.HasExtension(filePath)) return string.Empty;
            return Path.GetExtension(filePath).ToLower().Trim();
        }

        public static string GetExtensionWithoutDot(string filePath)
        {
            if (filePath == null) return null;
            if (Path.HasExtension(filePath)) return string.Empty;
            return Path.GetExtension(filePath).Replace(".", "").ToLower().Trim();
        }

        public static string GetFileNameDated(string filePath, string formatDateTime = "yyyyMMddHHmmssffff")
        {
            if (filePath == null) return null;
            string result = GetFileNameDatedWithoutExtension(filePath, formatDateTime);
            result += GetExtension(filePath);
            return result;
        }

        public static string GetFileNameDatedWithoutExtension(string filePath, string formatDateTime = "yyyyMMddHHmmssffff")
        {
            if (filePath == null) return null;
            string result = C_Mng_Files.GetFileNameWithoutExtension(filePath);
            result += "_" + DateTime.Now.ToString(formatDateTime);
            return result;
        }

        /// <summary>
        /// Obtiene el tamaño en bytes del archivo
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Tamaño del archivo en bytes, si no exite devuelve -1</returns>
        public static long GetSize(string filePath)
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
        /// 
        /// </summary>
        /// <param name="filePath">DB_path o Nombre del fichero. Por ejemplo: "MyTest.txt" o @"c:\temp\MyTest.txt"</param>
        /// <returns></returns>
        public static string Read(string filePath)
        {
            if (!Exists(filePath)) return null;
            string readText = File.ReadAllText(filePath);
            return readText;
        }

        public static FileStream ReadFileStream(string filePath)
        {
            FileStream source = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return source;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">Texto a insertar como línea al final del fichero</param>
        /// <param name="filePath">DB_path o Nombre del fichero. Por ejemplo: "MyTest.txt" o @"c:\temp\MyTest.txt"</param>
        public static void AddLine(string text, string filePath)
        {
            //if (path.EndsWith(".txt"))
            //{
            StringBuilder textFile = new StringBuilder();
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            textFile.AppendLine(line);
                        }
                    }
                }

                textFile.AppendLine(text);

                using (StreamWriter sw = new StreamWriter(
                        new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite)
                        , System.Text.Encoding.UTF8)) //Encoding.GetEncoding(1252)
                {
                    sw.WriteLine(textFile.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    throw new Exception("Extensión file is not .txt");
            //}
        }

        public static void Save(byte[] bytes, string filePath)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        public static void Save(string text, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(
                    new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite), System.Text.Encoding.UTF8)) //Encoding.GetEncoding(1252)
            {
                sw.Write(text);
            }
        }

        public static void Save_ListText(List<string> l_text, string filePath)
        {
            StringBuilder textFile = new StringBuilder();
            try
            {
                using (StreamWriter sw = new StreamWriter(
                        new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite)
                        , System.Text.Encoding.UTF8)) //Encoding.GetEncoding(1252)
                {
                    foreach (string r in l_text)
                    {
                        sw.WriteLine(r);
                    }
                }
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_Mng_Files.Save_ListText(List<string> l_text, string filePath)");
                throw;
            }
        }

        public static string ChangeExtension(string filePath, string newExtension, bool deleteFileIfExist = false)
        {
            string result = "";

            if (File.Exists(filePath))
            {
                string newPathFile = Path.ChangeExtension(filePath, newExtension);

                if (deleteFileIfExist && File.Exists(newPathFile))
                {
                    File.Delete(newPathFile);
                }

                File.Move(filePath, newPathFile);

                result = newPathFile;
            }

            return result;
        }

        public static string Rename(string filePath, string newFileName, bool deleteFileIfExist = false)
        {
            string result = "";

            if (File.Exists(filePath))
            {
                string fullPath = Path.GetFullPath(filePath);
                string directory = Path.GetDirectoryName(fullPath);
                string newPathFile = Path.Combine(directory, newFileName);

                if (File.Exists(newPathFile))
                {
                    if (deleteFileIfExist)
                    {
                        File.Delete(newPathFile);
                    }
                    else
                    {
                        string oldRenameFilePath = newPathFile;
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newPathFile);
                        string extension = Path.GetExtension(newPathFile);

                        int counter = 1;
                        while (File.Exists(oldRenameFilePath))
                        {
                            oldRenameFilePath = Path.Combine(directory, $"{fileNameWithoutExtension} ({counter}){extension}");
                            counter++;
                        }

                        File.Move(newPathFile, oldRenameFilePath);
                    }
                }

                File.Move(fullPath, newPathFile);
                result = newPathFile;
            }

            return result;
        }

        public static void Copy(string sourceFilePath, string destinationFilePath, bool overwrite = false)
        {
            string fullSourceFilePath = Path.GetFullPath(sourceFilePath);
            string fullDestinationFilePath = Path.GetFullPath(destinationFilePath);

            if (!overwrite && File.Exists(fullDestinationFilePath))
            {
                string directory = Path.GetDirectoryName(fullDestinationFilePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullDestinationFilePath);
                string extension = Path.GetExtension(fullDestinationFilePath);

                string newFullDestinationFilePath = Path.GetFullPath(fullDestinationFilePath);
                int counter = 1;
                while (File.Exists(newFullDestinationFilePath))
                {
                    newFullDestinationFilePath = Path.Combine(directory, $"{fileNameWithoutExtension} ({counter}){extension}");
                    counter++;
                }

                File.Move(fullDestinationFilePath, newFullDestinationFilePath);
            }

            File.Copy(fullSourceFilePath, fullDestinationFilePath, overwrite);
        }

        public static void Move(string sourceFilePath, string destinationFilePath, bool deleteDestinationFileIfExist = false)
        {
            string sourcefullPath = Path.GetFullPath(sourceFilePath);
            string destinationfullPath = Path.GetFullPath(destinationFilePath);

            if (File.Exists(destinationfullPath))
            {
                if (deleteDestinationFileIfExist)
                {
                    File.Delete(destinationfullPath);
                }
                else
                {
                    string directory = Path.GetDirectoryName(destinationfullPath);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(destinationfullPath);
                    string extension = Path.GetExtension(destinationfullPath);

                    string newDetinationFilePath = destinationFilePath;
                    int counter = 1;
                    while (File.Exists(newDetinationFilePath))
                    {
                        newDetinationFilePath = Path.Combine(directory, $"{fileNameWithoutExtension} ({counter}){extension}");
                        counter++;
                    }

                    File.Move(destinationfullPath, newDetinationFilePath);
                }
            }

            File.Move(sourcefullPath, destinationfullPath);
        }

        public static void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static long ExportToFile(string originPath, string destinationPath)
        {
            long result = 0;
            try
            {
                if (File.Exists(originPath))
                {
                    using (FileStream source = File.Open(originPath, FileMode.Open))
                    {
                        result = source.Length;
                        using (FileStream outputFileStream = new FileStream(destinationPath, FileMode.Create))
                        {
                            source.CopyTo(outputFileStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static List<string> GetFileNames(string folderPath, bool includeHiddenFiles = false)
        {
            List<string> fileNames = new List<string>();

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    // Check if the file is hidden
                    FileInfo fileInfo = new FileInfo(file);
                    if (!includeHiddenFiles && (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue; // Skip this file if hidden files are not included
                    }
                    fileNames.Add(Path.GetFileName(file));
                }
            }
            else
            {
                Console.WriteLine("The specified folder does not exist.");
            }

            return fileNames;
        }

        public static List<FileInfo> GetFiles(string folderPath, bool includeHiddenFiles)
        {
            List<FileInfo> fileInfos = new List<FileInfo>();

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    // Check if the file is hidden
                    if (!includeHiddenFiles && (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue; // Skip this file if hidden files are not included
                    }

                    fileInfos.Add(fileInfo);
                }
            }
            else
            {
                Console.WriteLine("The specified folder does not exist.");
            }

            return fileInfos;
        }

        public static bool DirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist
        /// 
        /// Crea todos los directorios y subdirectorios en la ruta especificada a menos que ya existan.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public static bool DeleteDirectory(string path, bool forced = false)
        {
            bool result = false;
            if (Directory.Exists(path))
            {
                Directory.Delete(path, forced);
                result = true;
            }
            return result;
        }
    }
}
