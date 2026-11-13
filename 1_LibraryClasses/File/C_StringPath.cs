using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _1_LibraryClasses.File
{
    /// <summary>
    /// OS-agnostic path as text. Stores the path string; <see cref="Segments"/> is derived via the separator.
    /// <para>
    /// Does not use the host OS defaults. If the separator cannot be determined, constructors throw.
    /// </para>
    /// <para><i>[ES] Ruta como texto independiente del SO. Guarda la cadena; <see cref="Segments"/> se obtiene con el separador.
    /// No usa valores por defecto del SO anfitrión. Si no se puede determinar el separador, los constructores lanzan.</i></para>
    /// </summary>
    public class C_StringPath : I_Infoable, IReadOnlyList<string>
    {
        #region PRIVATE FIELDS

        private string _path;
        private char _separator;
        private E_PathStyle _pathStyle;
        private E_PathType _pathType;

        #endregion

        #region PUBLIC PROPERTIES

        public string Path
        {
            get => _path;
            private set => _path = value ?? string.Empty;
        }

        public char Separator
        {
            get => _separator;
            set
            {
                if (value == default(char))
                    throw new ArgumentException("Separator is required.", nameof(value));
                _separator = value;
            }
        }

        public E_PathStyle PathStyle
        {
            get => _pathStyle;
            private set => _pathStyle = value;
        }

        public E_PathType PathType
        {
            get => _pathType;
            private set => _pathType = value;
        }

        public bool IsAbsolute => _pathType == E_PathType.ABSOLUTE;

        public bool IsRelative => _pathType == E_PathType.RELATIVE;

        public IReadOnlyList<string> Segments
            => C_Hlp_StringPath.SplitSegments(Path);

        public string? LastElement
        {
            get
            {
                IReadOnlyList<string> segs = Segments;
                return segs.Count == 0 ? null : segs[^1];
            }
        }

        public int Count => Segments.Count;

        public string this[int index] => Segments[index];

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Detects style, type and separator from the path text. Throws if separator cannot be determined
        /// (e.g. empty path or a single segment with no <c>/</c> or <c>\</c>).
        /// <para><i>[ES] Detecta estilo, tipo y separador del texto. Lanza si no se puede determinar el separador.</i></para>
        /// </summary>
        public C_StringPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is required.", nameof(path));

            this._path = path;
            this._pathStyle = C_Hlp_StringPath.GetPathStyle(path);
            this._pathType = C_Hlp_StringPath.GetPathType(path);
            this._separator = C_Hlp_StringPath.GetSeparator(path);
        }

        /// <summary>
        /// Explicit separator (must not be null or empty). Validates that the path does not use a conflicting separator.
        /// <para><i>[ES] Separador explícito. Valida que la ruta no use un separador contradictorio.</i></para>
        /// </summary>
        public C_StringPath(string path, char separator)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is required.", nameof(path));
            if (separator == default(char))
                throw new ArgumentException("Separator is required.", nameof(separator));

            char detectedSeparator = C_Hlp_StringPath.GetSeparator(path);
            if(detectedSeparator != default(char) && detectedSeparator != separator)
                throw new ArgumentException($"Path contains a conflicting separator '{detectedSeparator}'.", nameof(path));)
            
            this._path = path;
            this._pathStyle = C_Hlp_StringPath.GetPathStyle(path);
            this._pathType = C_Hlp_StringPath.GetPathType(path);
            this._separator = detectedSeparator;
        }

        /// <summary>
        /// Explicit <see cref="E_PathStyle"/>; separator from style. Validates style against the path text.
        /// <para><i>[ES] Estilo explícito; separador del estilo. Valida el estilo respecto al texto.</i></para>
        /// </summary>
        public C_StringPath(string path, E_PathStyle pathStyle)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is required.", nameof(path));

            E_PathStyle detectedStyle = C_Hlp_StringPath.GetPathStyle(path);

            if (pathStyle != E_PathStyle.UNKNOWN && pathStyle != E_PathStyle.UNDEFINED)
            {
                if (detectedStyle != E_PathStyle.UNKNOWN && detectedStyle != pathStyle)
                    throw new ArgumentException($"Path contains a conflicting style '{detectedStyle}'.", nameof(path));
            }            

            this._path = path;
            this._pathStyle = detectedStyle;
            this._pathType = C_Hlp_StringPath.GetPathType(path);
            this._separator = C_Hlp_StringPath.GetSeparator(path);
        }

        /// <summary>
        /// Explicit style and path type; separator from style. Validates both against the path text.
        /// <para><i>[ES] Estilo y tipo explícitos; separador del estilo. Valida ambos respecto al texto.</i></para>
        /// </summary>
        public C_StringPath(string path, E_PathStyle pathStyle, E_PathType pathType)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is required.", nameof(path));

            E_PathStyle detectedStyle = C_Hlp_StringPath.GetPathStyle(path);

            if (pathStyle != E_PathStyle.UNKNOWN && pathStyle != E_PathStyle.UNDEFINED)
            {
                if (detectedStyle != E_PathStyle.UNKNOWN && detectedStyle != pathStyle)
                    throw new ArgumentException($"Path contains a conflicting style '{detectedStyle}'.", nameof(path));
            }

            E_PathType detectedType = C_Hlp_StringPath.GetPathType(path);

            if(pathType != E_PathType.UNKNOWN && pathType != E_PathType.UNDEFINED)
            {
                if (detectedType != E_PathType.UNKNOWN && detectedType != pathType)
                    throw new ArgumentException($"Path contains a conflicting type '{detectedType}'.", nameof(path));
            }

            this._path = path;
            this._pathStyle = detectedStyle;
            this._pathType = detectedType;
            this._separator = C_Hlp_StringPath.GetSeparator(path);
        }

        public C_StringPath(C_StringPath source)
            : this(source.Path, source.PathStyle, source.PathType)
        {
        }

        #endregion

        #region PUBLIC METHODS

        public C_StringPath Clone()
            => new C_StringPath(this);

        public string Info()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

