using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Time;
using System;
using System.Text;

namespace _1_LibraryClassesNet10.Event
{
    public class C_Warning
    {
        private C_DateTime _dateTime = new C_DateTime(System.DateTime.Now);
        private E_ErrorType _type = E_ErrorType.ND;
        private string _message = "";
        private string _details = "";

        public C_DateTime DateTime { get => _dateTime; }
        public E_ErrorType ErrorType { get => this._type; }
        public string Message { get => _message; }
        public string Details { get => _details; }

        public C_Warning(string message, E_ErrorType errorType = E_ErrorType.ND)
        {
            this._type = errorType;
            this._message = message;
        }

        public C_Warning(string message, string details, E_ErrorType errorType = E_ErrorType.ND)
        {
            this._type = errorType;
            this._message = message;
            this._details = details;
        }

        public C_Warning(C_Error error, string message = null)
        {
            this._type = E_ErrorType.ERROR;
            this._message = message ?? error.Message;
            this._details = error.Details;
        }

        public C_Warning(C_Exception exception, string errorMessage = null)
        {
            this._type = E_ErrorType.EXCEPTION;
            this._message = errorMessage ?? exception.Message;
            this._details = exception.Details;
        }
        public C_Warning(Exception exception, string errorMessage = null)
        {
            this._type = E_ErrorType.EXCEPTION;
            this._message = errorMessage ?? exception.Message;
            this._details = ((C_Exception)exception).Details;
        }

        public override string ToString()
        {
            string result = this.DateTime.ToString() + "> WARNING!: [Error type: " + this.ErrorType.ToString() + "];";
            if (!string.IsNullOrEmpty(this.Message))
                result += ": " + this.Message;
            result += ".";
            return result;
        }

        public string Info()
        {
            StringBuilder text = new StringBuilder();
            string dateTime = this.DateTime.ToString();
            text.AppendLine(dateTime + " > -- WARNING! ----------------------------------------------------------------------");
            text.AppendLine(dateTime + " | Error Type: " + this.ErrorType.ToString() + ".");
            text.AppendLine(dateTime + " | Message: " + (this.Message ?? "") + ".");
            text.AppendLine(dateTime + " | Details: " + (this.Details ?? "") + ".");
            text.AppendLine(dateTime + " ^-----------------------------------------------------------------------------------");
            return text.ToString();
        }
    }
}
