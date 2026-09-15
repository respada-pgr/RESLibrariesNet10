using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_String : C_Result
    {
        public new string Value
        {
            get
            {
                return (string)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.STRING;
            }
        }

        public C_Result_String() : base() { }
        public C_Result_String(E_ResultStatus status) : base(status) { }
        public C_Result_String(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, status) { }
        public C_Result_String(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, message, status) { }
        public C_Result_String(string title, C_Error error, string message = null) : base(title, error, message) { }
        public C_Result_String(string title, Exception exception, string message = null) : base(title, exception, message) { }
        public C_Result_String(C_Error error, string message = null) : base(error, message) { }
        public C_Result_String(Exception exception, string message = null) : base(exception, message) { }

        public new void Set(string value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.STRING, value, status);
        }
        public new void Set(string value, string message = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.STRING, value, message, status);
        }
        public void Set(string value, string message, string details = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.STRING, value, message, details, status);
        }
        public override string ToString()
        {
            return $"{base.ToString()}. Value: '{this.Value.ToString()}'.";
        }

        public new string Info()
        {
            return $"{base.Info()},\r\nValue: '{this.Value.ToString()}'.";
        }
    }
}
