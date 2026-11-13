namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Domain or technical error code (detail beyond <see cref="E_ErrorType"/>).
    /// <para><i>[ES] Código de error de dominio o técnico (detalle más allá de <see cref="E_ErrorType"/>).</i></para>
    /// </summary>
    public enum E_ErrorCode
    {
        /// <summary>Not determined. / No determinado.</summary>
        ND = -1,

        /// <summary>No specific code. / Sin código específico.</summary>
        NONE = 0,

        // Domain / business (100+)
        VALIDATION = 100,
        NOT_FOUND = 101,
        DUPLICATE = 102,
        CONFLICT = 103,
        UNAUTHORIZED = 104,

        // Technical / system (500+)
        GENERIC = 500,
        DATABASE = 502,
        IO = 503,
        NETWORK = 504,
    }
}
