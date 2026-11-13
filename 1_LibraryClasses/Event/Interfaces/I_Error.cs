using _1_LibraryClasses.Event;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Contract for an error notice (failed operation or invalid state).
    /// Specialization of <see cref="I_Event"/>; further refined by <see cref="I_Exception"/>.
    /// <para><i>[ES] Contrato de aviso de error. Especialización de <see cref="I_Event"/>;
    /// refinada por <see cref="I_Exception"/>.</i></para>
    /// </summary>
    public interface I_Error : I_Event
    {
        /// <summary>
        /// Coarse kind (error vs exception).
        /// <para><i>[ES] Tipo grueso (error vs excepción).</i></para>
        /// </summary>
        E_ErrorType Type { get; }

        /// <summary>
        /// Domain or technical code (validation, not-found, IO, …).
        /// <para><i>[ES] Código de dominio o técnico (validación, not-found, IO, …).</i></para>
        /// </summary>
        E_ErrorCode Code { get; }
    }
}
