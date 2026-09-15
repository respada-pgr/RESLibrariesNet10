using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml;

namespace _1_LibraryClassesNet10.Event
{
    public class C_Exception : Exception
    {
        private DateTime _dateTime = DateTime.Now;
        private DateTime _utcDateTime = DateTime.UtcNow;

        public DateTime DateTime => this._dateTime;
        public DateTime UtcDateTime => this._utcDateTime;

        public Exception ExcepcionOriginal { get { return this.InnerException; } }
        public string Details { get => GetDetailsException().OuterXml; }

        public C_Exception(string message) : base(message) { }
        public C_Exception(string message, Exception innerException) : base(message, innerException) { }
        public C_Exception(Exception innerException) : base(innerException.Message, innerException) { }

        public override string ToString()
        {
            return this._dateTime.ToString() + "> Exception!! " + this.Message + ".";
        }

        public string Info()
        {
            StringBuilder text = new StringBuilder();
            string dateTime = this.DateTime.ToString();
            text.AppendLine(dateTime + " > -- EXCEPTION!!! ---------------------------------------------------------------------");
            text.AppendLine(dateTime + " | UTC Datetime: " + this._utcDateTime.ToString() + ".");
            text.AppendLine(dateTime + " | Message: " + (base.Message ?? "") + ".");
            text.AppendLine(dateTime + " | Details: " + (this.Details ?? "") + ".");
            text.AppendLine(dateTime + " ^--------------------------------------------------------------------------------------");
            return text.ToString();
        }

        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            XmlNode root = doc.CreateElement("root"); // This overload assumes the document already knows about the rdf schema as it is in the Schemas set
            doc.AppendChild(root);

            XmlElement element = GetDetailsException(doc);
            root.AppendChild(element);
            return doc;
        }

        private XmlElement GetDetailsException(XmlDocument document = null)
        {
            if (document == null)
                document = new XmlDocument();
            XmlElement result = document.CreateElement("Exception");
            string innerhtml = "";
            try
            {
                XmlElement xml_Source = document.CreateElement("source");
                try { xml_Source.InnerText = base.InnerException.Source; } catch { }
                innerhtml += xml_Source.OuterXml;

                XmlElement xml_Type = document.CreateElement("type");
                //xml_Type.SetAttribute("Name", ex.GetType().Name);
                //xml_Type.SetAttribute("FullName", ex.GetType().FullName);
                try { xml_Type.SetAttribute("assembly", base.InnerException.GetType().AssemblyQualifiedName); } catch { }
                try { xml_Type.InnerText = base.InnerException.GetType().ToString(); } catch { }
                innerhtml += xml_Type.OuterXml;

                XmlElement xml_Message = document.CreateElement("message");
                try { xml_Message.InnerText = base.InnerException.Message; } catch { }
                innerhtml += xml_Message.OuterXml;

                XmlElement xml_InnerException = document.CreateElement("inner_exception");
                if (base.InnerException != null)
                {
                    try { xml_InnerException.InnerText = base.InnerException.ToString(); } catch { }
                    innerhtml += xml_InnerException.OuterXml;
                }

                innerhtml += GetStringStackTrace(document);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("C_Exception.GetDetailsException(): " + ex.Message);
                innerhtml += $"\r\n<!-- ---------------- ERROR! Getting exception details ---------------- -->";
                innerhtml += $"\r\n<!-- Message: " + ex.Message + ". -->";
                innerhtml += $"\r\n<!-- ------------------------------------------------------------------ -->";
            }
            result.InnerXml = innerhtml;
            return result;
        }

        private XmlElement GetStringStackTrace(XmlDocument document)
        {
            XmlElement result = document.CreateElement("stack_trace");
            if (base.InnerException != null)
            {
                try
                {
                    StackTrace st = new StackTrace(base.InnerException, true);
                    StackFrame[] sf = st.GetFrames();

                    if (sf != null)
                    {
                        foreach (StackFrame frame in sf)
                        {
                            XmlElement xml_line = document.CreateElement("line");
                            try { xml_line.SetAttribute("file", frame.GetFileName()); } catch { }
                            try { xml_line.SetAttribute("number", frame.GetFileLineNumber().ToString()); } catch { }

                            MethodBase method = frame.GetMethod();
                            if (method != null)
                            {
                                try
                                {
                                    XmlElement xml_Module = document.CreateElement("module");
                                    xml_Module.InnerXml = method.Module.ToString();
                                    xml_line.AppendChild(xml_Module);
                                }
                                catch { }
                                try
                                {
                                    XmlElement xml_DeclaringType = document.CreateElement("declaring_type");
                                    xml_DeclaringType.InnerXml = method.DeclaringType.ToString();
                                    xml_line.AppendChild(xml_DeclaringType);
                                }
                                catch { }
                                try
                                {
                                    XmlElement xml_Method = document.CreateElement("assembly");
                                    xml_Method.InnerXml = method.ReflectedType.Assembly.ToString();
                                    xml_line.AppendChild(xml_Method);
                                }
                                catch { }
                                try
                                {
                                    XmlElement xml_Method = document.CreateElement("namespace");
                                    xml_Method.InnerXml = method.ReflectedType.Namespace.ToString();
                                    xml_line.AppendChild(xml_Method);
                                }
                                catch { }
                                try
                                {
                                    XmlElement xml_Method = document.CreateElement("class");
                                    xml_Method.InnerXml = method.ReflectedType.Name.ToString();
                                    xml_line.AppendChild(xml_Method);
                                }
                                catch { }
                                try
                                {
                                    XmlElement xml_Method = document.CreateElement("method");
                                    xml_Method.InnerXml = method.ToString();
                                    xml_line.AppendChild(xml_Method);
                                }
                                catch { }
                            }
                            result.AppendChild(xml_line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("C_Exception.GetStringStackTrace(Exception ex): " + ex.Message);
                    XmlElement xml_exception = document.CreateElement("EXCEPTION!");
                    xml_exception.InnerXml = "Se ha producido una excepción al obtener los DETALLES DE LA EXCEPCIÓN: " + ex.Message;
                    result.AppendChild(xml_exception);

                }
            }
            return result;
        }
    }
}
