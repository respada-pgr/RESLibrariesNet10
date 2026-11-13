using _1_LibraryClasses.Result;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Helpers for host file-system paths (<see cref="C_SystemPath"/>).
    /// <para>
    /// Logical/OS-agnostic text: <see cref="C_Hlp_StringPath"/>.
    /// Low-level string / BCL: <see cref="C_Hlp_Path"/>.
    /// </para>
    /// <para><i>[ES] Ayudas para rutas del sistema de ficheros del host (<see cref="C_SystemPath"/>).
    /// Texto lógico / independiente del SO: <see cref="C_Hlp_StringPath"/>.
    /// String / BCL de bajo nivel: <see cref="C_Hlp_Path"/>.</i></para>
    /// </summary>
    public static class C_Hlp_SystemPath
    {
        public static bool IsValid(string path) => C_Hlp_Path.IsValid(path);

        public static bool IsInvalidFormat(string path) => C_Hlp_Path.IsInvalidFormat(path);

        public static bool IsPathRooted(string path) => C_Hlp_Path.IsPathRooted(path);

        public static bool IsRelative(string path) => C_Hlp_Path.IsRelative(path);

        public static bool IsAbsolute(string path) => C_Hlp_Path.IsAbsolute(path);

        public static bool HasExtension(string path) => C_Hlp_Path.HasExtension(path);

        public static bool Exists(string path) {
            if(IsInvalidFormat(path))
                return false;
            string? folderPath = GetFolderPath(path);
            if (folderPath != null && !C_Hlp_Folder.Exists(folderPath))
                return false;
            string? filePath = GetFullPath(path);
            return C_Hlp_File.Exists(filePath);
        }

        public static string Normalize(string path)
            => C_Hlp_Path.Normalize(path);

        public static string GetFullPath(string path) => C_Hlp_Path.GetFullPath(path);

        public static string GetFullPath(Uri uri)
        {
            string localPath = C_Hlp_Uri.GetLocalPath(uri);
            return GetFullPath(localPath);
        }

        public static bool TryGetFullPath(string path, out string fullPath) => C_Hlp_Path.TryGetFullPath(path, out fullPath);

        public static string? GetRelativePath(string path)
            => C_Hlp_Path.GetRelativePath(Directory.GetCurrentDirectory(), path);

        public static string? GetRelativePath(string path, string basePath)
            => C_Hlp_Path.GetRelativePath(basePath, path);

        public static bool TryGetRelativePath(string path, out string relativePath)
            => TryGetRelativePath(path, Directory.GetCurrentDirectory(), out relativePath);

        public static bool TryGetRelativePath(string relativeTo, string path, out string relativePath)
            => C_Hlp_Path.TryGetRelativePath(relativeTo, path, out relativePath);

        public static string? GetPathRoot(string path)
            => C_Hlp_Path.GetPathRoot(path);

        public static string? GetFolderPath(string path)
            => C_Hlp_Path.GetFolderPath(path);

        public static string? GetFolderName(string path)
            => C_Hlp_Path.GetFolderName(path);

        public static string? GetFileName(string path)
            => C_Hlp_Path.GetFileName(path);

        public static string? GetFileNameWithoutExtension(string path)
            => C_Hlp_Path.GetFileNameWithoutExtension(path);

        public static string? GetExtensionWithDot(string path)
            => C_Hlp_Path.GetExtensionWithDot(path);

        public static string? GetExtension(string path)
            => C_Hlp_Path.GetExtension(path);        

        public static string GetInfo(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Path: {path}");
            sb.AppendLine($"IsValid: {IsValid(path)}");
            sb.AppendLine($"IsInvalidFormat: {IsInvalidFormat(path)}");
            sb.AppendLine($"IsPathRooted: {IsPathRooted(path)}");
            sb.AppendLine($"IsRelative: {IsRelative(path)}");
            sb.AppendLine($"IsAbsolute: {IsAbsolute(path)}");
            sb.AppendLine($"HasExtension: {HasExtension(path)}");
            sb.AppendLine($"FolderPath: {GetFolderPath(path) ?? "N/A"}");
            sb.AppendLine($"FolderName: {GetFolderName(path) ?? "N/A"}");
            sb.AppendLine($"FileName: {GetFileName(path) ?? "N/A"}");
            sb.AppendLine($"FileNameWithoutExtension: {GetFileNameWithoutExtension(path) ?? "N/A"}");
            sb.AppendLine($"ExtensionWithDot: {GetExtensionWithDot(path) ?? "N/A"}");
            sb.AppendLine($"Extension: {GetExtension(path) ?? "N/A"}");
            return sb.ToString();
        }

        public static Uri ToUri(string path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));
            return C_Hlp_Path.ToUri(path);
        }
    }
}
