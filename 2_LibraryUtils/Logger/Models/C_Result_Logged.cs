using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using _1_LibraryClassesNet10.Result;
using System;
using System.Text;

namespace _2_LibraryUtils.Logger
{
    public class C_Result_Logged : C_Result
    {
        //private C_LoggedLine _loggedLine = null;

        public C_LoggedLine LoggedLine
        {
            get => (C_LoggedLine)base.LogInfo ?? new C_LoggedLine();
            set
            {
                base.LogInfo = value;
                base.Type = E_ResultType.LOG;
            }
        }

        #region CONSTRUCTORS 
        public C_Result_Logged() : base() { }
        public C_Result_Logged(E_ResultStatus status) : base(status) { }
        public C_Result_Logged(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, status) { }
        public C_Result_Logged(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, message, status) { }
        public C_Result_Logged(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, message, details, status) { }
        public C_Result_Logged(string title, C_LoggedLine loggedLine, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LOG, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(string title, C_LoggedLine loggedLine, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LOG, message, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(string title, C_LoggedLine loggedLine, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LOG, message, details, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(string title, C_Result result, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, status) { }
        public C_Result_Logged(string title, C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, message, status) { }
        public C_Result_Logged(string title, C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, message, details, status) { }
        public C_Result_Logged(string title, C_Result result, C_LoggedLine loggedLine, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LOG, result, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(string title, C_Result_Logged result, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LOG, result, status)
        {
            this.LoggedLine = result.LoggedLine;
        }
        public C_Result_Logged(string title, C_Result_Logged result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, message, status)
        {
            this.LoggedLine = result.LoggedLine;
        }
        public C_Result_Logged(string title, C_Result_Logged result, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, message, details, status)
        {
            this.LoggedLine = result.LoggedLine;
        }
        public C_Result_Logged(string title, C_Error error) : base(title, error) { }
        public C_Result_Logged(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Logged(string title, Exception exception) : base(title, exception) { }
        public C_Result_Logged(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Logged(C_LoggedLine loggedLine, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(C_LoggedLine loggedLine, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, message, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(C_LoggedLine loggedLine, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, message, details, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(C_Result result, E_ResultStatus status = E_ResultStatus.ND) : base(result, status) { }
        public C_Result_Logged(C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(result, message, status) { }
        public C_Result_Logged(C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(result, message, details, status) { }
        public C_Result_Logged(C_Result result, C_LoggedLine loggedLine, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, result, status)
        {
            this.LoggedLine = loggedLine;
        }
        public C_Result_Logged(C_Result_Logged result, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, result, status)
        {
            this.LoggedLine = result.LoggedLine;
        }
        public C_Result_Logged(C_Result_Logged result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, result, message, status)
        {
            this.LoggedLine = result.LoggedLine;
        }
        public C_Result_Logged(C_Result_Logged result, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LOG, result, message, details, status)
        {
            this.LoggedLine = result.LoggedLine;
        }
        public C_Result_Logged(C_Error error) : base(error) { }
        public C_Result_Logged(C_Error error, string message) : base(error, message) { }
        public C_Result_Logged(Exception exception) : base(exception) { }
        public C_Result_Logged(Exception exception, string message) : base(exception, message) { }
        #endregion        

        public override string ToString()
        {
            string result = base.ToString();
            if (LoggedLine != null)
                result += " [Log line: " + LoggedLine.ToString() + "]";
            return result;
        }

        public new string Info()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine(this.DateTime.ToString() + "> " + base.ToString() + ": ");
            text.AppendLine("Type: '" + this.Type.ToString() + ",");
            text.AppendLine("Title: '" + (this.Title ?? "") + ",");
            text.AppendLine("Message: " + (this.Message ?? "") + ",");
            if (LoggedLine != null)
            {
                text.AppendLine("Log id: " + this.LoggedLine.LogId.ToString() + "',");
                text.AppendLine("Log line id: " + this.LoggedLine.Id.ToString());
            }
            if (this.LoggedLine != null)
                text.AppendLine("Log line: " + this.LoggedLine.Info());
            return text.ToString();
        }
    }
}
