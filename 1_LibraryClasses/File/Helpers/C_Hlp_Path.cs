using System.Linq;
using System.Text;

namespace _1_LibraryClasses.File
{
    public static class C_Hlp_Path
    {
        public static bool ContainsInvalidChars(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            char[] invalidChars = GetInvalidPathChars();
            return path.ContainsAny(invalidChars);
        }

        public static bool IsInvalidFormat(string path)
        {
            if (string.IsNullOrEmpty(path))
                return true;

            try
            {
                char[] invalidChars = GetInvalidPathChars();
                return path.ContainsAny(invalidChars);
            }
            catch
            {
                return true;
            }
        }

        public static bool IsValid(string path)
        {
            if (IsInvalidFormat(path))
                return false;

            try
            {
                string? fullPath = GetFullPath(path);
                return !string.IsNullOrEmpty(fullPath);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPathRooted(string path)
        {
            if (IsInvalidFormat(path))
                return false;
            return Path.IsPathRooted(path);
        }

        public static bool IsRelative(string path)
        {
            return !IsPathRooted(path);
        }

        public static bool IsAbsolute(string path)
        {
            if (IsInvalidFormat(path))
                return false;
            return IsPathRooted(path);
        }

        public static bool HasExtension(string path)
            => Path.HasExtension(path);        

        public static string Normalize(string path)
        {
            if (IsInvalidFormat(path))
                throw new ArgumentException("Path is invalid.", nameof(path));
            return GetFullPath(path);
        }

        public static bool TryNormalize(string path, out string normalizedPath)
        {
            normalizedPath = string.Empty;
            if (IsInvalidFormat(path))
                return false;
            try
            {
                normalizedPath = GetFullPath(path);
                return !string.IsNullOrEmpty(normalizedPath);
            }
            catch
            {
                return false;
            }
        }

        public static string? GetPathRoot(string path)
            => Path.GetPathRoot(path);

        public static char GetDirectorySeparatorChar()
            => Path.DirectorySeparatorChar;

        public static char GetAltDirectorySeparatorChar()
            => Path.AltDirectorySeparatorChar;

        public static char GetPathSeparator()
            => Path.PathSeparator;

        public static char[] GetInvalidPathChars()
            => Path.GetInvalidPathChars();

        public static string GetTempPath()
            => Path.GetTempPath();

        public static string GetTempFileName()
            => Path.GetTempFileName();

        public static string GetFullPath(string path)
             => Path.GetFullPath(path);

        public static bool TryGetFullPath(string path, out string fullPath)
        {
            fullPath = string.Empty;
            if (IsInvalidFormat(path)) return false;
            try
            {
                fullPath = GetFullPath(path);
                return !string.IsNullOrEmpty(fullPath);
            }
            catch
            {
                return false;
            }
        }

        public static string GetRelativePath(string relativeTo, string path)
            => Path.GetRelativePath(relativeTo, path);

        public static bool TryGetRelativePath(string relativeTo, string path, out string relativePath)
        {
            relativePath = string.Empty;
            if (string.IsNullOrEmpty(relativeTo) || string.IsNullOrEmpty(path))
                return false;
            try
            {
                relativePath = GetRelativePath(relativeTo, path);
                return !string.IsNullOrEmpty(relativePath);
            }
            catch
            {
                return false;
            }
        }

        public static string? GetFolderPath(string path)
            => Path.GetDirectoryName(path);

        public static string? GetFolderName(string path)
        {
            if (IsInvalidFormat(path))
                return null;

            try
            {
                string? fullPath = Path.GetFullPath(path);

                if (Path.HasExtension(fullPath))
                {
                    fullPath = Path.GetDirectoryName(fullPath);
                    if (string.IsNullOrEmpty(fullPath))
                    {
                        return null;
                    }
                }

                return Path.GetFileName(fullPath);
            }
            catch
            {
                return null;
            }
        }

        public static string GetRandomFolderName()
            => Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

        public static string? GetFileName(string path)
            => Path.GetFileName(path);

        public static string GetRandomFileName(string? extension = null)
        {
            string randomName = Path.GetRandomFileName();
            if (!string.IsNullOrEmpty(extension))
            {
                if (!extension.StartsWith("."))
                {
                    extension = "." + extension;
                }
                randomName = Path.ChangeExtension(randomName, extension);
            }
            return randomName;
        }

        public static string GetFileNameWithoutExtension(string path)
            => Path.GetFileNameWithoutExtension(path);

        public static string GetRandomFileNameWithoutExtension()
            => GetFileNameWithoutExtension(Path.GetRandomFileName());

        public static string? GetExtensionWithDot(string? path)
        {
            if (path is null) return null;
            if (!Path.HasExtension(path)) return string.Empty;
            return Path.GetExtension(path).ToLower().Trim();
        }

        public static string? GetExtension(string? path)
        {
            if (path is null) return null;
            if (!Path.HasExtension(path)) return string.Empty;
            return Path.GetExtension(path).Replace(".", "").ToLower().Trim();
        }        

        public static string ChangeExtension(string path, string? extension)
            => Path.ChangeExtension(path, extension);

        public static string ChangeFileName(string path, string newFileName)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path is required.", nameof(path));
            if (string.IsNullOrEmpty(newFileName))
                throw new ArgumentException("New file name is required.", nameof(newFileName));
            string? directory = Path.GetDirectoryName(path);
            return string.IsNullOrEmpty(directory)
                ? newFileName
                : Path.Combine(directory, newFileName);
        }

        public static string Combine(string path1, string path2)
            => Path.Combine(path1, path2);

        public static string Combine(params string[] paths)
            => Path.Combine(paths);

        public static string Combine(IEnumerable<string> paths)
            => Path.Combine(paths.ToArray());

        public static string EnsureTrailingDirectorySeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path ?? string.Empty;

            if (!path.EndsWith(System.IO.Path.DirectorySeparatorChar) &&
                !path.EndsWith(System.IO.Path.AltDirectorySeparatorChar))
                return path + System.IO.Path.DirectorySeparatorChar;

            return path;
        }

        public static Uri ToUri(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path is required.", nameof(path));

            if (Uri.TryCreate(path, UriKind.Absolute, out Uri? absolute) && absolute is not null)
                return absolute;

            string full = C_Hlp_Path.GetFullPath(path)
                ?? throw new ArgumentException("Path could not be resolved.", nameof(path));
            return new Uri(full);
        }
    }
}
