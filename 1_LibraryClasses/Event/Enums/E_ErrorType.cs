namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Coarse error kind. Numbers aligned with <see cref="E_EventType"/> for ERROR / EXCEPTION.
    /// <para><i>[ES] Tipo grueso de error. Números alineados con <see cref="E_EventType"/>.</i></para>
    /// </summary>
    public enum E_ErrorType
    {
        ND = -1,
        NONE = 0,
        /// <summary>Generic error (same value as <see cref="E_EventType.ERROR"/>).</summary>
        ERROR = 2,
        /// <summary>Exception-backed (same value as <see cref="E_EventType.EXCEPTION"/>).</summary>
        EXCEPTION = 3,
    }
}
