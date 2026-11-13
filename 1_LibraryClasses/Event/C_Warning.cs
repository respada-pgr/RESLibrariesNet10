using _1_LibraryClasses.Event;
using System.Text;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Warning notice (non-fatal issue).
    /// <para><i>[ES] Aviso de advertencia (problema no fatal).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var w = new C_Warning("Quota high", "90% used", E_ErrorType.ND);
    /// var fromError = new C_Warning(existingError, "Treated as warning");
    /// </code>
    /// </example>
    public class C_Warning : C_Event, I_Warning
    {
        /// <summary>
        /// Gets the related error classification (if any).
        /// <para><i>[ES] Obtiene la clasificación de error relacionada (si aplica).</i></para>
        /// </summary>
        public E_ErrorType Type { get; protected set; } = E_ErrorType.ND;

        /// <summary>Obsolete alias of <see cref="Type"/>.</summary>
        [Obsolete("Use Type instead of ErrorType.")]
        public E_ErrorType ErrorType => Type;

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new warning with a message.
        /// <para><i>[ES] Inicializa una nueva advertencia con un mensaje.</i></para>
        /// </summary>
        public C_Warning(string message, E_ErrorType errorType = E_ErrorType.ND)
            : base(E_EventType.WARNING, message, null)
        {
            Type = errorType;
        }

        /// <summary>
        /// Initializes a new warning with message and details.
        /// <para><i>[ES] Inicializa una nueva advertencia con mensaje y detalles.</i></para>
        /// </summary>
        public C_Warning(string message, string details, E_ErrorType errorType = E_ErrorType.ND)
            : base(E_EventType.WARNING, message, details)
        {
            Type = errorType;
        }

        /// <summary>
        /// Initializes a warning from an existing <see cref="C_Error"/>.
        /// <para><i>[ES] Inicializa una advertencia a partir de un <see cref="C_Error"/> existente.</i></para>
        /// </summary>
        public C_Warning(C_Error error, string message = null)
            : base(E_EventType.WARNING, message ?? error?.Message ?? "", error?.Details)
        {
            Type = error?.Type ?? E_ErrorType.ERROR;
        }

        /// <summary>
        /// Initializes a warning from a <see cref="C_Exception"/>.
        /// <para><i>[ES] Inicializa una advertencia a partir de un <see cref="C_Exception"/>.</i></para>
        /// </summary>
        public C_Warning(C_Exception exception, string errorMessage = null)
            : base(E_EventType.WARNING, errorMessage ?? exception?.Message ?? "", exception?.Details)
        {
            Type = E_ErrorType.EXCEPTION;
        }

        /// <summary>
        /// Initializes a warning from a <see cref="Exception"/>.
        /// <para><i>[ES] Inicializa una advertencia a partir de un <see cref="Exception"/>.</i></para>
        /// </summary>
        public C_Warning(Exception exception, string errorMessage = null)
            : base(
                E_EventType.WARNING,
                errorMessage ?? exception?.Message ?? "",
                exception is C_Exception cex ? cex.Details : exception?.ToString())
        {
            Type = E_ErrorType.EXCEPTION;
        }

        public C_Warning(C_Warning? source)
            : base(source)
        {
            Type = source?.Type ?? E_ErrorType.ND;
        }

        #endregion

        #region PUBLIC METHODS

        public C_Warning Clone()
            => new C_Warning(this);

        #endregion
    }
}
