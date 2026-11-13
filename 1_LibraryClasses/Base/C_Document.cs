using _1_LibraryClasses;
using System.Text;

namespace _1_LibraryClasses.Document
{
    /// <summary>
    /// Dated text document with a title and body.
    /// <para><i>[ES] Documento de texto fechado con título y cuerpo.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var doc = new C_Document("Import summary", "120 rows processed.");
    /// Console.WriteLine(doc.Info());
    /// </code>
    /// </example>
    public class C_Document : I_Document
    {
        /// <summary>
        /// Gets or sets the document title.
        /// <para><i>[ES] Obtiene o establece el título del documento.</i></para>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the main body text.
        /// <para><i>[ES] Obtiene o establece el texto principal (cuerpo).</i></para>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets the date and time when the document was created.
        /// <para><i>[ES] Obtiene la fecha y hora de creación del documento.</i></para>
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        /// Initializes a new empty document with <see cref="CreateDate"/> = now.
        /// <para><i>[ES] Inicializa un documento vacío con <see cref="CreateDate"/> = ahora.</i></para>
        /// </summary>
        public C_Document()
        {
            Title = "";
            Text = "";
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new document with title and optional body.
        /// <para><i>[ES] Inicializa un documento con título y cuerpo opcional.</i></para>
        /// </summary>
        /// <param name="title">Document title. / Título del documento.</param>
        /// <param name="text">Body text. / Texto del cuerpo.</param>
        public C_Document(string title, string text = null)
        {
            Title = title ?? "";
            Text = text ?? "";
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new document copying title, text and create date from another document.
        /// <para><i>[ES] Inicializa un documento copiando título, texto y fecha de creación de otro.</i></para>
        /// </summary>
        public C_Document(I_Document source)
        {
            if (source is null)
            {
                Title = "";
                Text = "";
                CreateDate = DateTime.Now;
            }
            else
            {
                Title = source.Title ?? "";
                Text = source.Text ?? "";
                CreateDate = source.CreateDate;
            }
        }

        /// <summary>Copy constructor from the same type. / Constructor de copia del mismo tipo.</summary>
        public C_Document(C_Document? source) : this((I_Document?)source)
        {
        }

        public C_Document Clone()
            => new C_Document(this);

        /// <summary>
        /// Returns a multi-line summary of the document.
        /// <para><i>[ES] Devuelve un resumen multilínea del documento.</i></para>
        /// </summary>
        public virtual string Info()
        {
            StringBuilder text = new StringBuilder();
            string when = CreateDate.ToString();
            text.AppendLine(when + " > -- DOCUMENT --------------------------------------------------------------------");
            text.AppendLine(when + " | Title: " + (Title ?? ""));
            text.AppendLine(when + " | Text:");
            if (!string.IsNullOrEmpty(Text))
            {
                foreach (string line in Text.Replace("\r\n", "\n").Split('\n'))
                    text.AppendLine(when + " |   " + line);
            }
            text.AppendLine(when + " ^-----------------------------------------------------------------------------------");
            return text.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string result = CreateDate.ToString() + "> " + (Title ?? "");
            if (!string.IsNullOrEmpty(Text))
                result += ": " + (Text.Length > 80 ? Text.Substring(0, 80) + "…" : Text);
            return result;
        }
    }
}
