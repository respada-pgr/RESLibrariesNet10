using _1_LibraryClasses.Event;
using System.Text;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Error notice (failed operation or invalid state).
    /// <para><i>[ES] Aviso de error (operación fallida o estado inválido).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var err = new C_Error("Not found", "Customer 12");
    /// var fromEx = new C_Error(ex, "Save failed");
    /// </code>
    /// </example>
    public class C_Error : C_Event, I_Error
    {
        /// <summary>
        /// Gets the error classification (domain/technical).
        /// <para><i>[ES] Obtiene la clasificación del error (dominio/técnica).</i></para>
        /// </summary>
        public E_ErrorType Type { get; protected set; } = E_ErrorType.ND;

        /// <summary>
        /// Domain or technical code.
        /// <para><i>[ES] Código de dominio o técnico.</i></para>
        /// </summary>
        public E_ErrorCode Code { get; protected set; } = E_ErrorCode.NONE;

        /// <summary>
        /// Initializes a new error with a message.
        /// <para><i>[ES] Inicializa un nuevo error con un mensaje.</i></para>
        /// </summary>
        public C_Error(string errorMessage, string details = null, E_ErrorCode code = E_ErrorCode.NONE)
            : base(E_EventType.ERROR, errorMessage, details)
        {
            Type = E_ErrorType.ERROR;
            Code = code;
        }

        /// <summary>
        /// Initializes a new error from a <see cref="C_Exception"/>.
        /// <para><i>[ES] Inicializa un nuevo error a partir de un <see cref="C_Exception"/>.</i></para>
        /// </summary>
        public C_Error(C_Exception exception, string errorMessage = null, E_ErrorCode code = E_ErrorCode.GENERIC)
            : base(E_EventType.EXCEPTION, errorMessage ?? exception?.Message ?? "", exception?.Details)
        {
            Type = E_ErrorType.EXCEPTION;
            Code = code;
        }

        /// <summary>
        /// Initializes a new error from a <see cref="Exception"/>.
        /// <para><i>[ES] Inicializa un nuevo error a partir de un <see cref="Exception"/>.</i></para>
        /// </summary>
        public C_Error(Exception exception, string errorMessage = null, E_ErrorCode code = E_ErrorCode.GENERIC)
            : base(
                E_EventType.EXCEPTION,
                errorMessage ?? exception?.Message ?? "",
                exception is C_Exception cex ? cex.Details : exception?.ToString())
        {
            Type = E_ErrorType.EXCEPTION;
            Code = code;
        }

        /// <summary>
        /// Copy constructor. Preserves type, code, message, details and CreateDate.
        /// <para><i>[ES] Constructor de copia. Conserva tipo, código, mensaje, detalles y CreateDate.</i></para>
        /// </summary>
        public C_Error(C_Error? source)
            : base(source)
        {
            if (source is null)
            {
                Type = E_ErrorType.ERROR;
                Code = E_ErrorCode.NONE;
            }
            else
            {
                Type = source.Type;
                Code = source.Code;
            }
        }

        public C_Error Clone()
            => new C_Error(this);
    }
}

