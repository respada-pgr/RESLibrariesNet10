namespace _1_LibraryClasses.Result
{
    /// <summary>
    /// Semantic severity of a result. Higher value = worse outcome.
    /// Shared idea with events/log: OK=0, WARNING=1; then FAIL=2, ERROR=3, EXCEPTION=4.
    /// <para><i>[ES] Severidad semántica del resultado. Mayor valor = peor.
    /// FAIL = mensaje; ERROR = mensaje+detalles; EXCEPTION = +excepción.</i></para>
    /// </summary>
    public enum E_ResultType
    {
        ND = -1,
        OK = 0,
        WARNING = 1,
        /// <summary>Failed operation (message). / Fallo (mensaje).</summary>
        FAIL = 2,
        /// <summary>Error (message + details). / Error (mensaje + detalles).</summary>
        ERROR = 3,
        /// <summary>Exception-backed failure. / Fallo con excepción.</summary>
        EXCEPTION = 4,
    }
}
