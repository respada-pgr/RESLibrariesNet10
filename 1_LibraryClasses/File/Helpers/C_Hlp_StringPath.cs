namespace _1_LibraryClasses.File
{
    /// <summary>
    /// OS-agnostic path string helpers (segments, separators, style). Does not touch the disk.
    /// <para>
    /// Host-system paths: <see cref="C_Hlp_Path"/> / <see cref="C_SystemPath"/>.
    /// </para>
    /// <para><i>[ES] Ayudas de ruta como texto, independientes del SO (segmentos, separadores, estilo). No tocan el disco.
    /// Rutas del host: <see cref="C_Hlp_Path"/> / <see cref="C_SystemPath"/>.</i></para>
    /// </summary>
    public static class C_Hlp_StringPath
    {
        #region PUBLIC METHODS — style / type

        public static E_PathStyle GetPathStyle(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return E_PathStyle.UNKNOWN;

            string p = path.Trim();

            if (p.StartsWith(@"\\", StringComparison.Ordinal))
                return E_PathStyle.UNC;

            if (p.StartsWith("//", StringComparison.Ordinal))
                return E_PathStyle.UNDEFINED;

            int schemeSep = p.IndexOf(':');
            if (schemeSep > 1)
            {
                string scheme = p.Substring(0, schemeSep);
                if (scheme.All(c => char.IsLetterOrDigit(c) || c == '+' || c == '-' || c == '.'))
                {
                    if (scheme.Equals("file", StringComparison.OrdinalIgnoreCase)
                        || scheme.Equals("http", StringComparison.OrdinalIgnoreCase)
                        || scheme.Equals("https", StringComparison.OrdinalIgnoreCase)
                        || scheme.Equals("ftp", StringComparison.OrdinalIgnoreCase)
                        || p.Contains("://", StringComparison.Ordinal))
                        return E_PathStyle.URI;
                }
            }

            if (p.Length >= 3 && char.IsLetter(p[0]) && p[1] == ':' && p[2] == '\\')
                return E_PathStyle.WINDOWS;

            if (p.Length >= 2 && char.IsLetter(p[0]) && p[1] == ':')
                return E_PathStyle.UNDEFINED;

            if (p.StartsWith('/'))
                return E_PathStyle.POSIX;

            bool hasBack = p.Contains('\\');
            bool hasFwd = p.Contains('/');

            if (hasBack && hasFwd)
                return E_PathStyle.UNDEFINED;

            if (hasBack)
                return E_PathStyle.WINDOWS;

            if (hasFwd)
                return E_PathStyle.POSIX;

            return E_PathStyle.UNKNOWN;
        }

        public static E_PathType GetPathType(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return E_PathType.UNKNOWN;

            string p = path.Trim();

            int schemeSep = p.IndexOf(':');
            if (schemeSep > 1 && p.Contains("://", StringComparison.Ordinal))
                return E_PathType.ABSOLUTE;

            if (schemeSep > 1)
            {
                string scheme = p.Substring(0, schemeSep);
                if (scheme.Equals("file", StringComparison.OrdinalIgnoreCase)
                    || scheme.Equals("http", StringComparison.OrdinalIgnoreCase)
                    || scheme.Equals("https", StringComparison.OrdinalIgnoreCase)
                    || scheme.Equals("ftp", StringComparison.OrdinalIgnoreCase))
                    return E_PathType.ABSOLUTE;
            }

            if (p.Length >= 2 && char.IsLetter(p[0]) && p[1] == ':')
                return E_PathType.ABSOLUTE;

            if (p.StartsWith(@"\\", StringComparison.Ordinal) || p.StartsWith("//", StringComparison.Ordinal))
                return E_PathType.ABSOLUTE;

            if (p.StartsWith('/'))
                return E_PathType.ABSOLUTE;

            return E_PathType.RELATIVE;
        }

        /// <summary>
        /// Preferred separator for a style (<c>\</c> for Windows/UNC, <c>/</c> for POSIX/URI default).
        /// <para><i>[ES] Separador preferido de un estilo.</i></para>
        /// </summary>
        public static char GetSeparator(E_PathStyle style)
            => style is E_PathStyle.WINDOWS or E_PathStyle.UNC ? '\\' : '/';

        /// <summary>
        /// Detects separator from the string (first <c>\</c> or <c>/</c>); fallback from style.
        /// <para><i>[ES] Detecta el separador en la cadena; si no hay, usa el del estilo.</i></para>
        /// </summary>
        public static char GetSeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
                return '/';

            int back = path.IndexOf('\\');
            int fwd = path.IndexOf('/');
            if (back >= 0 && (fwd < 0 || back < fwd))
                return '\\';
            if (fwd >= 0)
                return '/';

            return GetSeparator(GetPathStyle(path));
        }

        #endregion

        #region PUBLIC METHODS — segments

        /// <summary>
        /// Splits a path into segments (empty segments removed). UNC keeps server/share as first segments after empty root marker.
        /// <para><i>[ES] Parte la ruta en segmentos (sin vacíos).</i></para>
        /// </summary>
        public static List<string> SplitSegments(string? path)
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(path))
                return list;

            string p = path.Trim();
            // Normalize separators to \ for split, remember UNC
            bool unc = p.StartsWith(@"\\", StringComparison.Ordinal) || p.StartsWith("//", StringComparison.Ordinal);
            string normalized = p.Replace('/', '\\');
            if (unc && normalized.StartsWith(@"\\", StringComparison.Ordinal))
            {
                // \\server\share\rest
                string rest = normalized.TrimStart('\\');
                string[] parts = rest.Split('\\', StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(parts);
                return list;
            }

            // Drive: C:\...
            if (normalized.Length >= 2 && char.IsLetter(normalized[0]) && normalized[1] == ':')
            {
                list.Add(normalized.Substring(0, 2)); // "C:"
                string rest = normalized.Length > 2 ? normalized.Substring(2).TrimStart('\\') : string.Empty;
                if (!string.IsNullOrEmpty(rest))
                    list.AddRange(rest.Split('\\', StringSplitOptions.RemoveEmptyEntries));
                return list;
            }

            foreach (string part in normalized.Split('\\', StringSplitOptions.RemoveEmptyEntries))
                list.Add(part);

            return list;
        }

        /// <summary>
        /// Joins segments with the given separator. Handles Windows drive and UNC prefixes.
        /// <para><i>[ES] Une segmentos con el separador dado.</i></para>
        /// </summary>
        public static string JoinSegments(IReadOnlyList<string> segments, char separator, E_PathStyle style = E_PathStyle.UNKNOWN)
        {
            if (segments is null || segments.Count == 0)
                return string.Empty;

            // UNC: \\server\share\...
            if (style == E_PathStyle.UNC || (segments.Count >= 2 && style == E_PathStyle.UNKNOWN && separator == '\\'))
            {
                // Heuristic: if first two look like server+share and style UNC
            }

            if (style == E_PathStyle.UNC && segments.Count >= 1)
            {
                return @"\\" + string.Join(separator.ToString(), segments);
            }

            // Windows drive "C:" as first segment
            if (segments.Count >= 1
                && segments[0].Length == 2
                && char.IsLetter(segments[0][0])
                && segments[0][1] == ':')
            {
                if (segments.Count == 1)
                    return segments[0] + separator;
                return segments[0] + separator + string.Join(separator.ToString(), segments.Skip(1));
            }

            // Absolute POSIX: leading separator if style POSIX and was absolute
            string body = string.Join(separator.ToString(), segments);
            if (style == E_PathStyle.POSIX && separator == '/')
                return separator + body.TrimStart(separator);

            return body;
        }

        public static string? GetLastElement(string? path)
        {
            List<string> segs = SplitSegments(path);
            return segs.Count == 0 ? null : segs[^1];
        }

        public static string GetParentPath(string? path, char? separator = null)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            char sep = separator ?? GetSeparator(path);
            E_PathStyle style = GetPathStyle(path);
            List<string> segs = SplitSegments(path);
            if (segs.Count <= 1)
            {
                if (style == E_PathStyle.POSIX && path.TrimStart().StartsWith('/'))
                    return "/";
                if (segs.Count == 1 && segs[0].Length == 2 && segs[0][1] == ':')
                    return segs[0] + sep;
                return string.Empty;
            }

            segs.RemoveAt(segs.Count - 1);
            return JoinSegments(segs, sep, style);
        }

        #endregion

        #region PUBLIC METHODS — combine / convert

        public static string Combine(string path, string segment, char? separator = null)
        {
            if (string.IsNullOrEmpty(segment))
                return path ?? string.Empty;

            char sep = separator ?? GetSeparator(path);
            E_PathStyle style = GetPathStyle(path);
            List<string> segs = SplitSegments(path);
            foreach (string part in SplitSegments(segment))
                segs.Add(part);
            string joined = JoinSegments(segs, sep, style);

            // Preserve absolute POSIX root
            if (style == E_PathStyle.POSIX && (path?.TrimStart().StartsWith('/') ?? false) && !joined.StartsWith('/'))
                joined = "/" + joined.TrimStart('/');

            return joined;
        }

        public static string ToWindowsString(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            E_PathStyle style = GetPathStyle(path);
            List<string> segs = SplitSegments(path);
            if (style == E_PathStyle.UNC || path.StartsWith(@"\\") || path.StartsWith("//"))
                return JoinSegments(segs, '\\', E_PathStyle.UNC);

            return JoinSegments(segs, '\\', style == E_PathStyle.POSIX ? E_PathStyle.WINDOWS : style);
        }

        public static string ToPosixString(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            List<string> segs = SplitSegments(path);
            E_PathStyle style = GetPathStyle(path);
            string body = string.Join("/", segs);

            if (style is E_PathStyle.POSIX or E_PathStyle.UNKNOWN or E_PathStyle.UNDEFINED
                || path.TrimStart().StartsWith('/'))
            {
                if (style == E_PathStyle.WINDOWS && segs.Count > 0 && segs[0].Length == 2 && segs[0][1] == ':')
                {
                    // C:\a\b → /c/a/b (common portability mapping)
                    segs[0] = segs[0].Substring(0, 1).ToLowerInvariant();
                    return "/" + string.Join("/", segs);
                }
                if (path.TrimStart().StartsWith('/') || style == E_PathStyle.POSIX)
                    return "/" + body.TrimStart('/');
            }

            return body;
        }

        public static string ToSystemPathString(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            char sys = System.IO.Path.DirectorySeparatorChar;
            return sys == '\\' ? ToWindowsString(path) : ToPosixString(path);
        }

        #endregion
    }
}
