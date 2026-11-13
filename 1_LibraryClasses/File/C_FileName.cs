using System.Text;
using _1_LibraryClasses.Random;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Structured file name only (no folder path): <see cref="Prefix"/>, <see cref="Name"/>,
    /// <see cref="Subfix"/>, <see cref="Extension"/>.
    /// <para>
    /// Combine with a folder via <see cref="C_File"/>. Helpers: <see cref="C_Hlp_Random"/>.
    /// </para>
    /// <para><i>[ES] Solo nombre de fichero estructurado (sin carpeta).
    /// Combinar con carpeta mediante <see cref="C_File"/>.</i></para>
    /// </summary>
    public class C_FileName : I_Infoable
    {
        #region PRIVATE FIELDS

        private string _prefix = string.Empty;
        private string _name = string.Empty;
        private string _subfix = string.Empty;
        private string _extension = string.Empty;

        private string _separatorPrefix = "_";
        private string _separatorSubfix = "_";
        private string _formatPrefix = "yyyyMMddHHmmssffff";
        private string _formatSubfix = "yyyyMMddHHmmssffff";
        private int _randomAlphaPrefixLength = 8;
        private int _randomAlphaSubfixLength = 8;

        #endregion

        #region PUBLIC PROPERTIES

        public string Prefix
        {
            get => _prefix;
            set => _prefix = value ?? string.Empty;
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? string.Empty;
        }

        public string Subfix
        {
            get => _subfix;
            set => _subfix = value ?? string.Empty;
        }

        /// <summary>Extension with leading dot when non-empty. / Extensión con punto si hay valor.</summary>
        public string Extension
        {
            get => _extension;
            set
            {
                if (string.IsNullOrEmpty(value))
                    _extension = string.Empty;
                else
                    _extension = value.StartsWith('.') ? value : "." + value;
            }
        }

        public string ExtensionWithoutDot
            => string.IsNullOrEmpty(_extension) ? string.Empty : _extension.TrimStart('.').ToLowerInvariant();

        public string ExtensionWithDot => _extension;

        public string FullNameWithoutExtension => Prefix + Name + Subfix;

        public string FullName => FullNameWithoutExtension + Extension;

        public string SeparatorPrefix
        {
            get => _separatorPrefix;
            set => _separatorPrefix = value ?? string.Empty;
        }

        public string SeparatorSubfix
        {
            get => _separatorSubfix;
            set => _separatorSubfix = value ?? string.Empty;
        }

        public string FormatPrefix
        {
            get => _formatPrefix;
            set => _formatPrefix = string.IsNullOrEmpty(value) ? "yyyyMMddHHmmssffff" : value;
        }

        public string FormatSubfix
        {
            get => _formatSubfix;
            set => _formatSubfix = string.IsNullOrEmpty(value) ? "yyyyMMddHHmmssffff" : value;
        }

        public int RandomAlphaPrefixLength
        {
            get => _randomAlphaPrefixLength;
            set => _randomAlphaPrefixLength = value < 0 ? 0 : value;
        }

        public int RandomAlphaSubfixLength
        {
            get => _randomAlphaSubfixLength;
            set => _randomAlphaSubfixLength = value < 0 ? 0 : value;
        }

        #endregion

        #region CONSTRUCTORS

        public C_FileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            string onlyName = Path.GetFileName(fileName);
            Name = Path.GetFileNameWithoutExtension(onlyName) ?? string.Empty;
            Extension = Path.HasExtension(onlyName) ? Path.GetExtension(onlyName) : string.Empty;
        }

        public C_FileName(string name, string extension)
        {
            Name = name;
            Extension = extension;
        }

        public C_FileName(string name, string extension, string subfix)
        {
            Name = name;
            Extension = extension;
            Subfix = subfix;
        }

        public C_FileName(string prefix, string name, string extension, string subfix)
        {
            Prefix = prefix;
            Name = name;
            Extension = extension;
            Subfix = subfix;
        }

        public C_FileName(Uri fileUri)
            : this(
                fileUri is null
                    ? string.Empty
                    : fileUri.IsAbsoluteUri && fileUri.IsFile
                        ? fileUri.LocalPath
                        : (string.IsNullOrEmpty(fileUri.LocalPath) ? fileUri.ToString() : fileUri.LocalPath))
        {
        }

        /// <summary>Copy constructor. / Constructor de copia.</summary>
        public C_FileName(C_FileName? source)
        {
            if (source is null)
                return;
            this._prefix = source._prefix;
            this._name = source._name;
            this._subfix = source._subfix;
            this._extension = source._extension;
            this._separatorPrefix = source._separatorPrefix;
            this._separatorSubfix = source._separatorSubfix;
            this._formatPrefix = source._formatPrefix;
            this._formatSubfix = source._formatSubfix;
            this._randomAlphaPrefixLength = source._randomAlphaPrefixLength;
            this._randomAlphaSubfixLength = source._randomAlphaSubfixLength;
        }

        #endregion

        #region PUBLIC METHODS
        public C_FileName Clone()
            => new C_FileName(this);

        public string Info()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

