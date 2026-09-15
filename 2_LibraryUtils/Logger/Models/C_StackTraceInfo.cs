using System.Text;
using System.Xml;

namespace _2_LibraryUtils.Logger
{
    public class C_StackTraceInfo
    {
        private string _assembly = "";
        private string _namespace = "";
        private string _class = "";
        private string _method = "";
        private string _codeFilePath = "";
        private long _codeLineNumber = -1;

        public string Assembly { get => this._assembly; set => this._assembly = value; }
        public string Namespace { get => this._namespace; set => this._namespace = value; }
        public string Class { get => this._class; set => this._class = value; }
        public string Method { get => this._method; set => this._method = value; }
        public string CodeFilePath { get => this._codeFilePath; set => this._codeFilePath = value; }
        public long CodeLineNumber { get => this._codeLineNumber; set => this._codeLineNumber = value; }

        public string CodePath => "[" + (this._namespace ?? " ") + "]." + (this._class ?? "") + "." + (this._method ?? "");
        public string CodeLocation => CodePath + " " + this._codeLineNumber.ToString();

        public C_StackTraceInfo()
        {
        }

        public C_StackTraceInfo(C_StackTraceInfo element)
        {
            this._assembly = element.Assembly;
            this._namespace = element.Namespace;
            this._class = element.Class;
            this._method = element.Method;
            this._codeFilePath = element.CodeFilePath;
            this._codeLineNumber = element.CodeLineNumber;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Assembly: " + this.Assembly ?? "" + ". ");
            result.AppendLine("Namespace: " + this.Namespace ?? "" + ". ");
            result.AppendLine("Class: " + this.Class ?? "" + ". ");
            result.AppendLine("Method: " + this.Method ?? "" + ". ");
            result.AppendLine("Code line: " + this.CodeLineNumber.ToString() + ". ");
            return result.ToString();
        }

        public string Info()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Assembly: " + this.Assembly ?? "" + ". ");
            result.AppendLine("Code path: " + this.CodePath ?? "" + ". ");
            result.AppendLine("Code line: " + this.CodeLineNumber.ToString() + ". ");
            return result.ToString();
        }

        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            XmlNode root = doc.CreateElement("root"); // This overload assumes the document already knows about the rdf schema as it is in the Schemas set
            doc.AppendChild(root);

            XmlElement element = ToXmlElement(doc);
            root.AppendChild(element);
            return doc;
        }

        internal XmlElement ToXmlElement(XmlDocument document)
        {
            XmlElement result = document.CreateElement("stack_trace_info");
            string innerhtml = "";
            try
            {
                if (!string.IsNullOrWhiteSpace(this.Assembly))
                {
                    XmlElement xmlAssembly = document.CreateElement("assembly");
                    xmlAssembly.InnerXml = this.Assembly;
                    innerhtml += xmlAssembly.OuterXml;
                }
                if (!string.IsNullOrWhiteSpace(this.Namespace))
                {
                    XmlElement xmlNamespace = document.CreateElement("namespace");
                    xmlNamespace.InnerXml = this.Namespace;
                    innerhtml += xmlNamespace.OuterXml;
                }
                if (!string.IsNullOrWhiteSpace(this.Class))
                {
                    XmlElement xmlClass = document.CreateElement("class");
                    xmlClass.InnerXml = this.Class;
                    innerhtml += xmlClass.OuterXml;
                }
                if (!string.IsNullOrWhiteSpace(this.Method))
                {
                    XmlElement xmlMethod = document.CreateElement("method");
                    xmlMethod.InnerXml = this.Method;
                    innerhtml += xmlMethod.OuterXml;
                }
                if (!string.IsNullOrWhiteSpace(this.CodeFilePath))
                {
                    XmlElement xmlMethod = document.CreateElement("code_file_path");
                    xmlMethod.InnerXml = this.CodeFilePath;
                    innerhtml += xmlMethod.OuterXml;
                }
                if (this.CodeLineNumber > -1)
                {
                    XmlElement xmlMethod = document.CreateElement("code_line_number");
                    xmlMethod.InnerXml = this.CodeLineNumber.ToString();
                    innerhtml += xmlMethod.OuterXml;
                }
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_StackTraceInfo.ToXmlElement", "Error to generate innerhtml");
                innerhtml += "\r\n<!-- Exception generating stack trace xml!!! Exception message: " + ex.Message + "\r\nStack trace info:\r\n" + this.ToString() + "-->\r\n";
            }
            result.InnerXml = innerhtml;
            return result;
        }
    }
}
