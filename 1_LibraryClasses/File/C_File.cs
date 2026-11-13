using System.Text;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// File location on disk (<see cref="C_SystemPath"/>) with convenient name accessors.
    /// <para>
    /// Structured name tokens live in <see cref="C_FileName"/>. Compose with <see cref="C_Folder"/> + <see cref="C_FileName"/>.
    /// </para>
    /// <para><i>[ES] Ubicación de fichero en disco (<see cref="C_SystemPath"/>) con accesores de nombre.
    /// El nombre estructurado está en <see cref="C_FileName"/>.</i></para>
    /// </summary>
    public class C_File : C_SystemPath
    {
        #region PUBLIC PROPERTIES

        public string FileName => C_Hlp_Path.GetFileName(LocalPath) ?? string.Empty;

        public string FileNameWithoutExtension => C_Hlp_Path.GetFileNameWithoutExtension(LocalPath) ?? string.Empty;

        /// <summary>Extension without dot. / Extensión sin punto.</summary>
        public string Extension => C_Hlp_Path.GetExtension(LocalPath) ?? string.Empty;

        public string ExtensionWithDot => C_Hlp_Path.GetExtensionWithDot(LocalPath) ?? string.Empty;

        /// <summary>
        /// Structured name parsed from the current path (Prefix/Subfix empty).
        /// <para><i>[ES] Nombre estructurado de la ruta actual (Prefix/Subfix vacíos).</i></para>
        /// </summary>
        public C_FileName StructuredFileName => new C_FileName(FileName);

        #endregion

        #region CONSTRUCTORS

        public C_File(string filePath)
            : base(filePath)
        {
        }

        public C_File(Uri uri)
            : base(uri)
        {
        }

        public C_File(C_Folder folder, C_FileName fileName)
            : base(C_Hlp_Path.Combine(
                folder?.LocalPath ?? throw new ArgumentNullException(nameof(folder)),
                fileName is null ? string.Empty : fileName.FullName))
        {
        }

        public C_File(C_Folder folder, string fileName)
            : base(C_Hlp_Path.Combine(
                folder?.LocalPath ?? throw new ArgumentNullException(nameof(folder)),
                fileName ?? string.Empty))
        {
        }

        public C_File(string folderPath, C_FileName fileName)
            : base(C_Hlp_Path.Combine(folderPath ?? string.Empty, fileName is null ? string.Empty : fileName.FullName))
        {
        }

        public C_File(string folderPath, string fileName)
            : base(C_Hlp_Path.Combine(folderPath ?? string.Empty, fileName ?? string.Empty))
        {
        }

        public C_File(C_File? source)
            : this(source is null ? string.Empty : source.LocalPath)
        {
        }

        #endregion

        #region PUBLIC METHODS

        public C_File Clone()
            => new C_File(this);

        #endregion
    }
}

