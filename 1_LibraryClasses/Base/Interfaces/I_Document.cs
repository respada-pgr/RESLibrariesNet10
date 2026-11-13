namespace _1_LibraryClasses
{
    /// <summary>
    /// Defines a contract for a dated text document with a title and body.
    /// <para>
    /// Intended for human-readable content (notes, memos, generated text). Distinct from
    /// operation results (<c>C_Result</c>), which use message/details and a status.
    /// </para>
    /// <para><i>[ES] Define un contrato para un documento de texto fechado con título y cuerpo.
    /// Pensado para contenido legible (notas, memorandos, texto generado). Distinto de los
    /// resultados de operación (<c>C_Result</c>), que usan mensaje/detalles y un estado.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Memo : I_Document
    /// {
    ///     public string Title { get; set; } = "";
    ///     public string Text { get; set; } = "";
    ///     public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    ///
    ///     public string Info() => Title + Environment.NewLine + Text;
    /// }
    ///
    /// I_Document doc = new C_Memo { Title = "Summary", Text = "All rows imported." };
    /// Console.WriteLine(doc.Info());
    /// </code>
    /// </example>
    public interface I_Document : I_Infoable
    {
        /// <summary>
        /// Gets the date and time when the document was created.
        /// <para><i>[ES] Obtiene la fecha y hora de creación del documento.</i></para>
        /// </summary>
        DateTime CreateDate { get; }

        /// <summary>
        /// Gets the document title.
        /// <para><i>[ES] Obtiene el título del documento.</i></para>
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the main body text of the document.
        /// <para><i>[ES] Obtiene el texto principal (cuerpo) del documento.</i></para>
        /// </summary>
        string Text { get; }
    }
}
