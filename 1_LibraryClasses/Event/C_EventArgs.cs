using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Time;
using System;

namespace _1_LibraryClassesNet10.Event
{
    public class C_EventArgs : EventArgs
    {
        private E_ResultStatus _status = E_ResultStatus.ND;
        private C_DateTime _dateTime = new C_DateTime(System.DateTime.Now);
        private string _message = "";

        public C_DateTime DateTime { get => _dateTime; }

        public string Message { get => this._message; set => this._message = value; }

        public E_ResultStatus Status
        {
            get => this._status;
            set
            {
                this._status = value;
                this._dateTime.DateTime = System.DateTime.Now;
            }
        }

        public bool Success
        {
            get => this._status == E_ResultStatus.SUCCESS;
            set
            {
                this._status = value ? E_ResultStatus.SUCCESS : E_ResultStatus.ND;
                this._dateTime.DateTime = System.DateTime.Now;
            }
        }

        public bool Fail
        {
            get => this._status == E_ResultStatus.FAIL;
            set
            {
                this._status = value ? E_ResultStatus.FAIL : E_ResultStatus.ND;
                this._dateTime.DateTime = System.DateTime.Now;
            }
        }

        public C_EventArgs()
        {
        }

        public C_EventArgs(string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._status = status;
            this._message = message ?? "";
        }


        public C_EventArgs(C_Error error, string message = null)
        {
            this._status = E_ResultStatus.FAIL;
            this._message = message ?? error.Message;
        }

        public C_EventArgs(Exception exception, string message = null)
        {
            this._status = E_ResultStatus.FAIL;
            this._message = message ?? exception.Message;
        }

        public string Info()
        {
            string result = this.DateTime.ToString() + "> " + this.Status.ToString();
            if (!string.IsNullOrEmpty(this._message))
                result += ". Message: " + this._message;
            result += ".";

            return result.ToString();
        }
    }
}
