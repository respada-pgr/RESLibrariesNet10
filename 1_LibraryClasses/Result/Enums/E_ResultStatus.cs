namespace _1_LibraryClasses.Result
{
    /// <summary>
    /// Outcome status of an operation result.
    /// ND=-1, FAIL=0, OK=1 (binary outcome; not the severity scale of <see cref="E_ResultType"/>).
    /// <para><i>[ES] Estado de resultado. ND=-1, FAIL=0, OK=1 (binario; no es la escala de severidad de Type).</i></para>
    /// </summary>
    public enum E_ResultStatus
    {
        ND = -1,
        FAIL = 0,
        OK = 1,
    }
}
