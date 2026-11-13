using System.Text;

namespace _1_LibraryClasses.Report
{
    /// <summary>
    /// Process/operation report with title, details and creation date.
    /// <para><i>[ES] Informe de proceso/operación con título, detalles y fecha de creación.</i></para>
    /// </summary>
    public class C_Report : I_Report
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreateDate { get; protected set; }

        public C_Report()
        {
            Title = "";
            Details = "";
            CreateDate = DateTime.Now;
        }

        public C_Report(string title, string details = null)
        {
            Title = title ?? "";
            Details = details ?? "";
            CreateDate = DateTime.Now;
        }

        public C_Report(I_Report source)
        {
            if (source is null)
            {
                Title = "";
                Details = "";
                CreateDate = DateTime.Now;
            }
            else
            {
                Title = source.Title ?? "";
                Details = source.Details ?? "";
                CreateDate = source.CreateDate;
            }
        }

        public C_Report(C_Report? source) : this((I_Report?)source)
        {
        }

        public C_Report Clone()
            => new C_Report(this);

        public virtual string Info()
        {
            StringBuilder text = new StringBuilder();
            string when = CreateDate.ToString();
            text.AppendLine(when + " > -- REPORT -----------------------------------------------------------------------");
            text.AppendLine(when + " | Title: " + (Title ?? ""));
            text.AppendLine(when + " | Details:");
            if (!string.IsNullOrEmpty(Details))
            {
                foreach (string line in Details.Replace("\r\n", "\n").Split('\n'))
                    text.AppendLine(when + " |   " + line);
            }
            text.AppendLine(when + " ^-----------------------------------------------------------------------------------");
            return text.ToString();
        }

        public override string ToString()
        {
            string result = CreateDate.ToString() + "> " + (Title ?? "");
            if (!string.IsNullOrEmpty(Details))
                result += ": " + (Details.Length > 80 ? Details.Substring(0, 80) + "…" : Details);
            return result;
        }
    }
}
