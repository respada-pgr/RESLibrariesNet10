namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Helpers for <see cref="System.Uri"/> related to file paths.
    /// <para>
    /// String path operations: <see cref="C_Hlp_Path"/>.
    /// </para>
    /// <para><i>[ES] Ayudas de <see cref="System.Uri"/> relacionadas con rutas de fichero.
    /// Operaciones de ruta en string: <see cref="C_Hlp_Path"/>.</i></para>
    /// </summary>
    public static class C_Hlp_Uri
    {
#region PUBLIC METHODS       

        /// <summary>
        /// Local path from a <see cref="Uri"/>.
        /// <para><i>[ES] Ruta local a partir de un <see cref="Uri"/>.</i></para>
        /// </summary>
        public static string GetLocalPath(Uri uri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            return uri.IsAbsoluteUri ? uri.LocalPath : uri.ToString();
        }

        /// <summary>
        /// <see langword="true"/> when the URI is not absolute.
        /// <para><i>[ES] <see langword="true"/> si el URI no es absoluto.</i></para>
        /// </summary>
        public static bool IsRelative(Uri uri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            return !uri.IsAbsoluteUri;
        }

        /// <summary>
        /// Tries to build a <see cref="Uri"/> from an absolute path string without resolving relative paths.
        /// <para><i>[ES] Intenta construir un <see cref="Uri"/> desde una cadena absoluta sin resolver relativas.</i></para>
        /// </summary>
        public static bool TryCreateAbsolute(string path, out Uri? uri)
            => Uri.TryCreate(path, UriKind.Absolute, out uri);

        /// <summary>
        /// Returns <paramref name="target"/> relative to <paramref name="baseUri"/> using <see cref="Uri.MakeRelativeUri"/>.
        /// <para><i>[ES] Devuelve <paramref name="target"/> relativo a <paramref name="baseUri"/>.</i></para>
        /// </summary>
        /// <summary>
        /// Returns <paramref name="target"/> relative to <paramref name="baseUri"/>, or <see langword="null"/> if not possible.
        /// <para><i>[ES] Devuelve <paramref name="target"/> relativo a <paramref name="baseUri"/>, o <see langword="null"/> si no es posible.</i></para>
        /// </summary>
        public static string? MakeRelative(Uri baseUri, Uri target)
        {
            if (baseUri is null || target is null)
                return null;

            if (!baseUri.IsAbsoluteUri || !target.IsAbsoluteUri)
                return null;

            try
            {
                Uri relativeUri = baseUri.MakeRelativeUri(target);
                // If still absolute, bases were not comparable (different scheme/root).
                if (relativeUri.IsAbsoluteUri)
                    return null;

                string relative = Uri.UnescapeDataString(relativeUri.ToString());
                return string.IsNullOrEmpty(relative) ? null : relative;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Builds a relative path from two absolute path strings via URI comparison.
        /// Returns <see langword="null"/> when a relative path cannot be obtained.
        /// <para><i>[ES] Ruta relativa entre dos absolutas. <see langword="null"/> si no se puede obtener.</i></para>
        /// </summary>
        public static string? GetRelativePath(string absolutePath, string absoluteBasePath)
        {
            if (string.IsNullOrEmpty(absolutePath) || string.IsNullOrEmpty(absoluteBasePath))
                return null;

            try
            {
                Uri targetUri = ToUri(absolutePath);
                if (!targetUri.IsAbsoluteUri)
                    return null;

                Uri baseUri = ToUri(absoluteBasePath);
                return MakeRelative(baseUri, targetUri);
            }
            catch
            {
                return null;
            }
        }

#endregion
    }
}
