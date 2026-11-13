namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Severity / kind of a runtime or domain notice.
    /// Shared scale with <c>E_LogLineType</c> / <c>E_ErrorType</c> / <c>E_ResultType</c> where names match:
    /// ND=-1, NONE/OK=0, WARNING=1, ERROR=2, EXCEPTION=3.
    /// <para><i>[ES] Severidad de un aviso. Escala compartida con otros enums cuando el nombre coincide.</i></para>
    /// </summary>
    public enum E_EventType
    {
        ND = -1,
        NONE = 0,
        WARNING = 1,
        ERROR = 2,
        EXCEPTION = 3,
    }
}
