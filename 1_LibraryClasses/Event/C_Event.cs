using _1_LibraryClasses.Event;
using System.Text;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Base class for dated notices (errors, warnings) with message and optional details.
    /// <para><i>[ES] Clase base para avisos fechados (errores, advertencias) con mensaje y detalles opcionales.</i></para>
    /// </summary>
    /// <remarks>
    /// Distinct from <see cref="C_EventArgs"/>, which extends <see cref="System.EventArgs"/> for .NET events.
    /// <para><i>[ES] Distinta de <see cref="C_EventArgs"/>, que extiende <see cref="System.EventArgs"/> para eventos .NET.</i></para>
    /// </remarks>
    /// <example>
    /// <code>
    /// C_Event notice = new C_Warning("Disk almost full", "Only 5% free");
    /// string text = notice.Info();
    /// </code>
    /// </example>
    public abstract class C_Event : I_Event
    {
        /// <summary>
        /// Gets the date and time when the notice was created.
        /// <para><i>[ES] Obtiene la fecha y hora de creación del aviso.</i></para>
        /// </summary>
        public DateTime CreateDate { get; protected set; } = DateTime.Now;

        /// <summary>
        /// Notice kind for subclasses and <see cref="I_Event"/> (not part of the public API of <see cref="C_Error"/> / <see cref="C_Warning"/>).
        /// <para><i>[ES] Tipo de aviso para subclases e <see cref="I_Event"/> (no forma parte de la API pública de <see cref="C_Error"/> / <see cref="C_Warning"/>).</i></para>
        /// </summary>
        protected E_EventType EventType { get; set; } = E_EventType.ND;

        /// <inheritdoc />
        E_EventType I_Event.EventType => EventType;

        /// <summary>
        /// Gets the short message.
        /// <para><i>[ES] Obtiene el mensaje corto.</i></para>
        /// </summary>
        public string Message { get; protected set; } = "";

        /// <summary>
        /// Gets optional detail text.
        /// <para><i>[ES] Obtiene el texto de detalle opcional.</i></para>
        /// </summary>
        public string Details { get; protected set; } = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="C_Event"/> class.
        /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Event"/>.</i></para>
        /// </summary>
        /// <param name="eventType">Notice kind. / Tipo de aviso.</param>
        /// <param name="message">Short message. / Mensaje corto.</param>
        /// <param name="details">Optional details. / Detalles opcionales.</param>
        protected C_Event(E_EventType eventType, string message, string details = null)
        {
            EventType = eventType;
            Message = message ?? "";
            Details = details ?? "";
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Copy constructor. Preserves message, details, type and <see cref="CreateDate"/>.
        /// <para><i>[ES] Constructor de copia. Conserva mensaje, detalles, tipo y CreateDate.</i></para>
        /// </summary>
        protected C_Event(C_Event? source)
        {
            if (source is null)
            {
                EventType = E_EventType.ND;
                Message = "";
                Details = null;
                CreateDate = DateTime.Now;
            }
            else
            {
                EventType = source.EventType;
                Message = source.Message;
                Details = source.Details;
                CreateDate = source.CreateDate;
            }
        }

        public string Info()
        {
            throw new NotImplementedException();
        }
    }
}

