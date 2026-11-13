namespace _1_LibraryClasses.File
{
    /// <summary>
    /// Whether a path string is absolute or relative (independent of Windows / POSIX style).
    /// <para><i>[ES] Si una cadena de ruta es absoluta o relativa (independiente del estilo Windows / POSIX).</i></para>
    /// </summary>
    public enum E_PathType
    {
        ///<summary>Undefined. / Sin definir.</summary>
        UNDEFINED = -1,

        /// <summary>Empty or not classifiable. / Vacía o no clasificable.</summary>
        UNKNOWN = 0,

        /// <summary>Absolute path or absolute URI. / Ruta absoluta o URI absoluta.</summary>
        ABSOLUTE = 1,

        /// <summary>Relative to a base location. / Relativa a una ubicación base.</summary>
        RELATIVE = 2,
    }
}
