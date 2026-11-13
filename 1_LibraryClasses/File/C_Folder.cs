using System.Text;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Path specialized as a <em>directory</em>.
    /// <para>
    /// Inherits <see cref="C_SystemPath"/>. Static helpers: <see cref="C_Hlp_Folder"/>.
    /// </para>
    /// <para><i>[ES] Ruta especializada como <em>directorio</em>.
    /// Hereda <see cref="C_SystemPath"/>. Ayudas estáticas: <see cref="C_Hlp_Folder"/>.</i></para>
    /// </summary>
    public class C_Folder : C_SystemPath
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// Directory name only (last segment).
        /// <para><i>[ES] Solo el nombre del directorio (último segmento).</i></para>
        /// </summary>
        public string Name
        {
            get
            {
                string trimmed = LocalPath.TrimEnd(
                    System.IO.Path.DirectorySeparatorChar,
                    System.IO.Path.AltDirectorySeparatorChar);
                return C_Hlp_Path.GetFileName(trimmed) ?? string.Empty;
            }
        }

        /// <summary>
        /// Parent directory path (same as <see cref="C_SystemPath.FolderPath"/>).
        /// <para><i>[ES] Ruta del directorio padre (igual que <see cref="C_SystemPath.FolderPath"/>).</i></para>
        /// </summary>
        public string ParentPath => FolderPath;

        /// <summary>
        /// Whether the directory exists on disk.
        /// <para><i>[ES] Indica si el directorio existe en disco.</i></para>
        /// </summary>
        public bool Exists => C_Hlp_Folder.Exists(LocalPath);

        #endregion

        #region CONSTRUCTORS

        public C_Folder(string path)
            : base(path)
        {
        }

        public C_Folder(Uri uri)
            : base(uri)
        {
        }

        public C_Folder(C_Folder? source)
            : base(source is null ? string.Empty : source.LocalPath)
        {
        }

        #endregion

        #region PUBLIC METHODS

        public C_Folder Clone()
            => new C_Folder(this);

        #endregion
    }
}
