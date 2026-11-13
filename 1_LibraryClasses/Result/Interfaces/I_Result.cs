namespace _1_LibraryClasses.Result
{
    /// <summary>
    /// Minimal contract for an operation result.
    /// <para><i>[ES] Contrato mínimo de resultado de operación.</i></para>
    /// </summary>
    public interface I_Result
    {
        E_ResultStatus Status { get; }
        E_ResultType Type { get; }
        bool IsOK { get; }
        bool IsFail { get; }
        bool IsWarning { get; }
        bool IsError { get; }
        bool HasException { get; }
        string Message { get; }
        string Details { get; }
    }

    /// <summary>
    /// Operation result with a typed payload.
    /// <para><i>[ES] Resultado de operación con valor tipado.</i></para>
    /// </summary>
    public interface I_Result<TValue> : I_Result
    {
        TValue Value { get; }
    }
}
