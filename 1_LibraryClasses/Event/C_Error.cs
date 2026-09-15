using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Time;
using System;
using System.Text;

namespace _1_LibraryClassesNet10.Event
{
    public class C_Error
    {
        private C_DateTime _dateTime = new C_DateTime(System.DateTime.Now);
        private E_ErrorType _type = E_ErrorType.ND;
        private string _message = "";
        private string _details = "";


        public C_DateTime DateTime { get => _dateTime; }
        public E_ErrorType Type { get => this._type; }
        public string Message { get => _message; }
        public string Details { get => _details; }

        public C_Error(string errorMessage, string details = null)
        {
            this._type = E_ErrorType.ERROR;
            this._message = errorMessage;
            this._details = details;
        }

        public C_Error(C_Exception exception, string errorMessage = null)
        {
            this._type = E_ErrorType.EXCEPTION;
            this._message = errorMessage ?? exception.Message;
            this._details = exception.Details;
        }
        public C_Error(Exception exception, string errorMessage = null)
        {
            this._type = E_ErrorType.EXCEPTION;
            this._message = errorMessage ?? exception.Message;
            this._details = ((C_Exception)exception).Details;
        }

        public override string ToString()
        {
            string result = this.DateTime.ToString() + "> " + this.Type.ToString();
            if (!string.IsNullOrEmpty(this.Message))
                result += ": " + this.Message;
            result += ".";
            return result;
        }

        public string Info()
        {
            StringBuilder text = new StringBuilder();
            string dateTime = this.DateTime.ToString();
            text.AppendLine(dateTime + " > -- ERROR!! -----------------------------------------------------------------------");
            text.AppendLine(dateTime + " | Type: " + this.Type.ToString() + ".");
            text.AppendLine(dateTime + " | Message: " + (this.Message ?? "") + ".");
            text.AppendLine(dateTime + " | Details: " + (this.Details ?? "") + ".");
            text.AppendLine(dateTime + " ^-----------------------------------------------------------------------------------");
            return text.ToString();
        }
    }
}
