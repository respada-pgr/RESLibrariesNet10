using System.Text;

namespace _1_LibraryClassesNet10.Files
{
    public class C_FilePath
    {
        private Uri _fileUri;

        public Uri Uri { get => _fileUri; }
        public string LocalPath { get => Uri.LocalPath; }
        public string FullPath { get => Path.GetFullPath(LocalPath); }
        public string FolderPath { get => Path.GetDirectoryName(LocalPath); }
        public string FileName { get => Path.GetFileName(LocalPath); }
        public string FileNameWithoutExtension { get => Path.GetFileNameWithoutExtension(LocalPath); }
        public string Extension { get => Path.GetExtension(LocalPath); }
        public string ExtensionWithoutDot { get => Path.GetExtension(LocalPath).Replace(".", ""); }

        public C_FilePath(string filePath)
        {
            _fileUri = new Uri(filePath);
        }

        public C_FilePath(Uri uri)
        {
            _fileUri = uri;
        }

        public string GetFileNameDated(string formatDateTime = "yyyyMMddHHmmssffff")
        {
            string result = GetFileNameDatedWithoutExtension(formatDateTime);
            result += this.Extension;
            return result;
        }

        public string GetFileNameDatedWithoutExtension(string formatDateTime = "yyyyMMddHHmmssffff")
        {
            string result = FileNameWithoutExtension;
            result += "_" + DateTime.Now.ToString(formatDateTime);
            return result;
        }

        public string GetRelativePath(string basePath)
        {
            Uri baseUri = new Uri(basePath);
            Uri rutaRelativaUri = baseUri.MakeRelativeUri(Uri);
            string result = Uri.UnescapeDataString(rutaRelativaUri.ToString());
            return result;
        }

        public string Info()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Uri: {this.Uri.ToString()}");
            result.AppendLine($"Local Path: {this.LocalPath}");
            result.AppendLine($"Full Path: {this.FullPath}");
            result.AppendLine($"Relative Path: {this.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory)}");
            result.AppendLine($"Folder Path: {this.FolderPath}");
            result.AppendLine($"File Name: {this.FileName}");
            result.AppendLine($"File Name Without Extension: {this.FileNameWithoutExtension}");
            result.AppendLine($"Extension: {this.Extension}");
            result.AppendLine($"Extension Without Dot: {this.ExtensionWithoutDot}");
            result.AppendLine($"File Name Dated: {this.GetFileNameDated()}");
            result.AppendLine($"Get File Name Dated Without Extension: {this.GetFileNameDatedWithoutExtension()}");
            return result.ToString();
        }
    }
}
