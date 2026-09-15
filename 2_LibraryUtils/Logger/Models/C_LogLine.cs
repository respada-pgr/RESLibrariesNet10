using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using _1_LibraryClassesNet10.Result;
using System;
using System.Text;
using System.Xml;

namespace _2_LibraryUtils.Logger
{
    public class C_LogLine
    {
        #region PRIVATE VARIABLES ---------------------------------------------------------------
        private long _id = -1;
        private long _parentId = -1;
        private E_LogLineType _type = E_LogLineType.ND;
        private string _title = "";
        private string _message = "";
        private string _details = "";

        private C_StackTraceInfo _stackTrace = new C_StackTraceInfo();

        private DateTime _create_date;
        #endregion

        #region PUBLIC PROPERTIES ---------------------------------------------------------------
        public long Id { get => this._id; set => this._id = value; }
        public long ParentId { get => this._parentId; set => this._parentId = value; }
        public E_LogLineType Type { get => this._type; set => this._type = value; }
        public string Title { get => this._title; set => this._title = value; }
        public string Message { get => this._message; set => this._message = value; }
        public string Details { get => this._details; set => this._details = value; }
        public C_StackTraceInfo StackTrace { get => _stackTrace; set => _stackTrace = value; }
        public string Assembly { get => this._stackTrace.Assembly; set => this._stackTrace.Assembly = value; }
        public string Namespace { get => this._stackTrace.Namespace; set => this._stackTrace.Namespace = value; }
        public string Class { get => this._stackTrace.Class; set => this._stackTrace.Class = value; }
        public string Method { get => this._stackTrace.Method; set => this._stackTrace.Method = value; }
        public string CodeFilePath { get => this._stackTrace.CodeFilePath; set => this._stackTrace.CodeFilePath = value; }
        public long CodeLineNumber { get => this._stackTrace.CodeLineNumber; set => this._stackTrace.CodeLineNumber = value; }
        public DateTime CreateDate { get => this._create_date; set => this._create_date = value; }
        #endregion

        #region PUBLIC CONTRUCTORS ---------------------------------------------------------------
        public C_LogLine()
        {
            this._create_date = DateTime.Now;
        }

        public C_LogLine(C_LogLine line)
        {
            this._id = line.Id;
            this._parentId = line.ParentId;
            this._type = line.Type;
            this._title = line.Title;
            this._message = line.Message;
            this._details = line.Details;
            this._stackTrace = new C_StackTraceInfo(line.StackTrace);
            this._create_date = DateTime.Now;
        }

        public C_LogLine(C_StackTraceInfo stacktrace)
        {
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(string title, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._type = log_type;
            this._title = title;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(string title, string message, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._type = log_type;
            this._title = title;
            this._message = message;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(string title, string message, string details, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._type = log_type;
            this._title = title;
            this._message = message;
            this._details = details;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(long parentId, string title, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._parentId = parentId;
            this._type = log_type;
            this._title = title;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(long parentId, string title, string message, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._parentId = parentId;
            this._type = log_type;
            this._title = title;
            this._message = message;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(long parentId, string title, string message, string details, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._parentId = parentId;
            this._type = log_type;
            this._title = title;
            this._message = message;
            this._details = details;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(C_Result element, C_StackTraceInfo stacktrace, C_StackTraceInfo stackTrace)
        {
            this.Set(element);
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(C_Result element, C_StackTraceInfo stacktrace, E_LogLineType log_type = E_LogLineType.ND)
        {
            this.Set(element, log_type);
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(C_Result element, C_StackTraceInfo stacktrace, string details, E_LogLineType log_type = E_LogLineType.ND)
        {
            this.Set(element, log_type);
            this._details = details;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(C_Error error, C_StackTraceInfo stacktrace, string title = null, string message = null)
        {
            this.Set(error);
            this._title = title ?? this._title;
            this._message = message ?? this._message;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }

        public C_LogLine(Exception ex, C_StackTraceInfo stacktrace, string title = null, string message = null, E_LogLineType log_type = E_LogLineType.ND)
        {
            this.Set(ex, log_type);
            this._title = title ?? this._title;
            this._message = message ?? this._message;
            this._stackTrace = stacktrace;
            this._create_date = DateTime.Now;
        }
        #endregion

        #region PUBLIC METHODS ---------------------------------------------------------------

        public override string ToString()
        {
            string result = this.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " - ID:" + this.Id + ">"
                + (this.Type != E_LogLineType.ND ? "[" + this.Type.ToString() + "]" : "");
            if (!string.IsNullOrEmpty(this.Title))
            {
                result += ".[" + this.Title + "]";
                if (!string.IsNullOrEmpty(this.Message))
                    result += ": " + this.Message;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Message))
                    result += ": " + this.Message;
            }
            return result + ".";
        }

        public string Info()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("");
            result.AppendLine(this.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "> " + (this.Type != E_LogLineType.ND ? this.Type.ToString() : "") + ". ");
            result.AppendLine("Id: " + this.Id.ToString() + ". ");
            if (this.ParentId > 1)
                result.AppendLine("ParentId: " + this.ParentId.ToString() + ". ");
            result.AppendLine("Title: " + this.Title ?? "" + ". ");
            result.AppendLine("Message: " + this.Message ?? "" + ". ");
            result.AppendLine("Details: " + this.Details ?? "" + ". ");
            result.AppendLine("---------------------------------------------------------------------");
            result.Append(this._stackTrace.Info());
            result.AppendLine("---------------------------------------------------------------------");
            result.AppendLine("");
            return result.ToString();
        }

        public string InfoExt()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("");
            result.AppendLine(this.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "> " + (this.Type != E_LogLineType.ND ? this.Type.ToString() : "") + ". ");
            result.AppendLine("Id: " + this.Id.ToString() + ". ");
            if (this.ParentId > 1)
                result.AppendLine("ParentId: " + this.ParentId.ToString() + ". ");
            result.AppendLine("Title: " + this.Title ?? "" + ". ");
            result.AppendLine("Message: " + this.Message ?? "" + ". ");
            result.AppendLine("Details: " + this.Details ?? "" + ". ");
            result.AppendLine("---------------------------------------------------------------------");
            result.Append(this._stackTrace.ToString());
            result.AppendLine("---------------------------------------------------------------------");
            result.AppendLine("");
            return result.ToString();
        }
        #endregion

        #region INTERNAL METHODS ---------------------------------------------------------------

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

        public XmlElement ToXmlElement(XmlDocument document, bool addCreateDate = true)
        {
            XmlElement result = document.CreateElement(this.Type.ToString());
            if (this.Id > -1)
                result.SetAttribute("id", this.Id.ToString());
            if (this.ParentId > -1)
                result.SetAttribute("parent_id", this.ParentId.ToString());
            if (addCreateDate)
                result.SetAttribute("date", this.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (!string.IsNullOrWhiteSpace(this.Title))
                result.SetAttribute("title", this.Title);

            string innerhtml = "";
            try
            {
                if (!string.IsNullOrWhiteSpace(this.Message))
                {
                    XmlElement xmlDescription = document.CreateElement("message");
                    xmlDescription.InnerXml = this.Message;
                    innerhtml += xmlDescription.OuterXml;
                }

                innerhtml += this._stackTrace.ToXmlElement(document).OuterXml;

                XmlElement xmlDetails = document.CreateElement("details");
                try
                {
                    if (!string.IsNullOrWhiteSpace(this.Details))
                    {
                        xmlDetails.InnerXml = this.Details;
                    }
                }
                catch (Exception ex)
                {
                    C_DebugLogger.Add(ex, "C_LogLine.ToXmlElement: Error to generate innerhtml of details from element: " + this.ToString());
                    xmlDetails.InnerXml = "\r\n<!-- Exception!!! to generate innerhtml of details from element: " + this.ToString() + ". Exception: " + ex.Message + " -->\r\n";
                }
                innerhtml += xmlDetails.OuterXml;
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_LogLine.ToXmlElement: Error to generate innerhtml from element id: " + this.ToString());
                innerhtml += "\r\n<!-- Exception!!! to generate innerhtml from element id: " + this.ToString() + ". Exception: " + ex.Message + " -->\r\n";
            }
            result.InnerXml = innerhtml;
            return result;
        }

        //public void SetTrace(int methodsToSkip = 3)
        //{
        //    try
        //    {
        //        StackTrace trace = new StackTrace(StackTrace.METHODS_TO_SKIP + methodsToSkip, true);
        //        StackFrame frame = trace.GetFrame(0);
        //        if (frame != null)
        //        {
        //            MethodBase caller = frame.GetMethod();
        //            if (caller != null)
        //            {
        //                try { this._assembly = caller.ReflectedType.Assembly.ToString(); } catch { }
        //                try { this._namespace = caller.ReflectedType.Namespace; } catch { }
        //                try { this._class = caller.ReflectedType.Name; } catch { }
        //                try { this._method = caller.Name; } catch { }
        //            }

        //            try { this._codeFilePath = frame.GetFileName(); } catch { }
        //            try { this._codeLineNumber = frame.GetFileLineNumber(); } catch { }
        //        }
        //    }
        //    catch { }
        //}

        #endregion

        #region PRIVATE METHODS ---------------------------------------------------------------


        private void Set(C_Result element, E_LogLineType log_type = E_LogLineType.ND)
        {
            if (log_type == E_LogLineType.ND)
            {
                switch (element.Type)
                {
                    case E_ResultType.EXCEPTION:
                        this._type = E_LogLineType.EXCEPTION;
                        break;
                    case E_ResultType.ERROR:
                        this._type = E_LogLineType.ERROR;
                        break;
                    case E_ResultType.WARNING:
                        this._type = E_LogLineType.WARNING;
                        break;
                    default:
                        switch (element.Status)
                        {
                            case E_ResultStatus.SUCCESS:
                                this._type = E_LogLineType.INFORMATIVE;
                                break;
                            case E_ResultStatus.FAIL:
                                this._type = E_LogLineType.ERROR;
                                break;
                            default:
                                this._type = E_LogLineType.ND;
                                break;
                        }
                        break;
                }
            }
            else
                this._type = log_type;
            this._title = element.Title;
            this._message = element.Message;
            this._details = element.Details ?? "";

            if (element.Exception != null)
            {
                this._details += "\r\nException details: " + element.Exception.Details;
            }
        }

        private void Set(C_Error error, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._type = log_type == E_LogLineType.ND ? this._type = E_LogLineType.ERROR : log_type;
            this._message = error.Message;
            this._details = error.Details;
        }

        public void Set(C_Exception exception, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._type = log_type == E_LogLineType.ND ? E_LogLineType.EXCEPTION : log_type;
            this._message = exception.Message;
            this._details = exception.Details;
        }

        public void Set(Exception exception, E_LogLineType log_type = E_LogLineType.ND)
        {
            this._type = log_type == E_LogLineType.ND ? E_LogLineType.EXCEPTION : log_type;
            C_Exception ex = new C_Exception(exception);
            this._message = ex.Message;
            this._details = ex.Details;
        }
        #endregion
    }
}
