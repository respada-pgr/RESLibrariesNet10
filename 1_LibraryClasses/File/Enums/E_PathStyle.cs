namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Path string syntax style (separator / root convention), not the host OS and not absolute vs relative.
    /// <para>
    /// Use <see cref="E_PathType"/> for absolute vs relative.
    /// <see cref="UNC"/> is a path form; SMB is a network protocol.
    /// </para>
    /// <para><i>[ES] Estilo de sintaxis de la cadena de ruta (separador / convención de raíz), no el SO anfitrión ni absoluta vs relativa.
    /// Use <see cref="E_PathType"/> para absoluta vs relativa.
    /// <see cref="UNC"/> es una forma de ruta; SMB es un protocolo de red.</i></para>
    /// </summary>
    public enum E_PathStyle
    {
        /// <summary>Empty or not classifiable. / Vacía o no clasificable.</summary>
        UNKNOWN = 0,

        /// <summary>
        /// Ambiguous or non-canonical (e.g. <c>C:/temp</c>, <c>//server/share</c>, mixed separators).
        /// <para><i>[ES] Ambiguo o no canónico.</i></para>
        /// </summary>
        UNDEFINED = 1,

        /// <summary>
        /// Windows DOS style with backslash (e.g. <c>C:\temp</c>, <c>data\logs</c>).
        /// <para><i>[ES] Estilo DOS Windows con barra invertida.</i></para>
        /// </summary>
        WINDOWS = 2,

        /// <summary>
        /// Microsoft UNC (<c>\\server\share</c>). Not the SMB protocol.
        /// <para><i>[ES] UNC de Microsoft. No es el protocolo SMB.</i></para>
        /// </summary>
        UNC = 3,

        /// <summary>
        /// POSIX style (Linux, macOS, BSD…): <c>/var/log</c>, <c>data/logs</c>.
        /// <para><i>[ES] Estilo POSIX.</i></para>
        /// </summary>
        POSIX = 4,

        /// <summary>
        /// URI with explicit scheme (<c>file:</c>, <c>http:</c>, …).
        /// <para><i>[ES] URI con esquema explícito.</i></para>
        /// </summary>
        URI = 5,
    }
}
