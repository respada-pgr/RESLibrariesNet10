namespace _1_LibraryClasses.Report
{
    /// <summary>
    /// Contract for a process/operation report (title, details, creation time).
    /// <para>
    /// Conceptual role differs from <c>I_Document</c> (general text document with body <c>Text</c>).
    /// A report describes an outcome narrative; a document is free-form content.
    /// </para>
    /// <para><i>[ES] Contrato de informe de proceso/operación (título, detalles, fecha de creación).
    /// Distinto de <c>I_Document</c> (documento de texto con cuerpo <c>Text</c>).</i></para>
    /// </summary>
    public interface I_Report
    {
        /// <summary>Report title. / Título del informe.</summary>
        string Title { get; }

        /// <summary>Detail body. / Cuerpo de detalle.</summary>
        string Details { get; }

        /// <summary>Creation timestamp. / Fecha de creación.</summary>
        DateTime CreateDate { get; }
    }
}
