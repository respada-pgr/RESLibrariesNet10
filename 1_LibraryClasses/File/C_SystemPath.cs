using System.Text;
using _1_LibraryClasses.Result;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Host file-system path. Internally stores a <see cref="C_StringPath"/>; <see cref="Path"/> remains a <see cref="string"/>.
    /// <para>
    /// Specialized by <see cref="C_File"/> (file) and <see cref="C_Folder"/> (directory).
    /// Helper: <see cref="C_Hlp_SystemPath"/> (uses <see cref="C_Hlp_Path"/>, <see cref="C_Hlp_Uri"/>).
    /// </para>
    /// <para><i>[ES] Ruta del sistema de ficheros del host. Por dentro guarda un <see cref="C_StringPath"/>; <see cref="Path"/> sigue siendo <see cref="string"/>.
    /// Especializada por <see cref="C_File"/> y <see cref="C_Folder"/>.
    /// Helper: <see cref="C_Hlp_SystemPath"/>.</i></para>
    /// </summary>
    public class C_SystemPath : I_Infoable
    {
        #region PRIVATE FIELDS

        private C_StringPath _path;

        #endregion

        #region PUBLIC PROPERTIES     

        public string Path => _path.Path;

        public E_PathStyle PathStyle => _path.PathStyle;
        public E_PathType PathType => _path.PathType;

        public string? RelativePath => C_Hlp_SystemPath.GetRelativePath(this._path.Path);

        public string? PathRoot => C_Hlp_SystemPath.GetPathRoot(this._path.Path);

        public string? FolderPath => C_Hlp_SystemPath.GetFolderPath(this._path.Path);

        public string? FileName => C_Hlp_SystemPath.GetFileName(this._path.Path);

        public string? FileNameWithoutExtension => C_Hlp_SystemPath.GetFileNameWithoutExtension(this._path.Path);

        public string? ExtensionWithDot => C_Hlp_SystemPath.GetExtensionWithDot(this._path.Path);

        public string? Extension => C_Hlp_SystemPath.GetExtension(this._path.Path);

        public C_Folder? Folder => this.FolderPath != null ? new C_Folder(this.FolderPath) : null;

        public C_File? File => this.FileName != null ? new C_File(this.Path) : null;

        public bool Exists => C_Hlp_SystemPath.Exists(this._path.Path);

        #endregion

        #region CONSTRUCTORS

        public C_SystemPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path is required.", nameof(path));

            string normalizedPath = C_Hlp_SystemPath.Normalize(path);
            _path = new C_StringPath(normalizedPath);
        }

        public C_SystemPath(C_StringPath stringPath)
            : this(stringPath?.Path ?? throw new ArgumentNullException(nameof(stringPath)))
        {
        }

        /// <summary>
        /// Creates a path from a <see cref="Uri"/> (uses local path).
        /// <para><i>[ES] Crea una ruta desde un <see cref="Uri"/> (usa la ruta local).</i></para>
        /// </summary>
        public C_SystemPath(Uri uri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            string localPath = C_Hlp_Uri.GetLocalPath(uri);
            string normalizedPath = C_Hlp_SystemPath.Normalize(localPath);
            _path = new C_StringPath(normalizedPath);
        }

        public C_SystemPath(C_SystemPath? source)
            : this(source?.Path ?? throw new ArgumentNullException(nameof(source)))
        {
        }

        #endregion

        #region PUBLIC METHODS
        
        public string? GetRelativePathTo(C_SystemPath target)
            => C_Hlp_SystemPath.GetRelativePath(this._path.Path, target?.Path ?? throw new ArgumentNullException(nameof(target)));

        public C_SystemPath Clone()
            => new C_SystemPath(this);                

        public Uri ToUri()
            => C_Hlp_SystemPath.ToUri(this._path.Path);
        
        public override string ToString()
            => this.Path;

        public string Info()
            => C_Hlp_SystemPath.GetInfo(this._path.Path);

        #endregion
    }
}

