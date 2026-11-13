using _1_LibraryClasses.Event;
using _1_LibraryClasses;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Contract for a dated domain/runtime notice (message, optional details).
    /// <para><i>[ES] Contrato de un aviso de dominio/ejecución con fecha (mensaje y detalles opcionales).</i></para>
    /// </summary>
    /// <remarks>
    /// Not the same as <see cref="System.EventArgs"/> / <see cref="C_EventArgs"/> (UI or .NET event payload).
    /// <para><i>[ES] No es lo mismo que <see cref="System.EventArgs"/> / <see cref="C_EventArgs"/> (carga de evento .NET o UI).</i></para>
    /// </remarks>
    /// <example>
    /// <code>
    /// I_Event ev = new C_Error("Validation failed", "Field X is required");
    /// Console.WriteLine(ev.EventType);
    /// Console.WriteLine(ev.Info());
    /// </code>
    /// </example>
    public interface I_Event : I_Infoable
    {
        /// <summary>
        /// Gets the date and time when the notice was created.
        /// <para><i>[ES] Obtiene la fecha y hora de creación del aviso.</i></para>
        /// </summary>
        DateTime CreateDate { get; }

        /// <summary>
        /// Gets the notice kind (warning, error, exception, …).
        /// <para><i>[ES] Obtiene el tipo de aviso (advertencia, error, excepción, …).</i></para>
        /// </summary>
        E_EventType EventType { get; }

        /// <summary>
        /// Gets the short message.
        /// <para><i>[ES] Obtiene el mensaje corto.</i></para>
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets optional detail text.
        /// <para><i>[ES] Obtiene el texto de detalle opcional.</i></para>
        /// </summary>
        string Details { get; }
    }
}
