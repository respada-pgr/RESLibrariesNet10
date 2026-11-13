using _1_LibraryClasses.Event;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Contract for a warning notice (non-fatal issue).
    /// <para><i>[ES] Contrato de un aviso de advertencia (problema no fatal).</i></para>
    /// </summary>
    public interface I_Warning : I_Event
    {
        /// <summary>
        /// Gets the related error classification (if any).
        /// <para><i>[ES] Obtiene la clasificación de error relacionada (si aplica).</i></para>
        /// </summary>
        E_ErrorType Type { get; }
    }
}
