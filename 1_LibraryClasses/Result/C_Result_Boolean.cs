using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Boolean : C_Result
    {
        public new bool Value
        {
            get
            {
                return (bool)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.BOOLEAN;
            }
        }

        public C_Result_Boolean() : base(E_ResultType.BOOLEAN, false) { }
        public C_Result_Boolean(E_ResultStatus status) : base(E_ResultType.BOOLEAN, false, status) { }
        public C_Result_Boolean(string title, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.BOOLEAN, false, title, status) { }
        public C_Result_Boolean(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.BOOLEAN, false, message, status) { }
        public C_Result_Boolean(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.BOOLEAN, false, message, details, status) { }
        public C_Result_Boolean(string title, bool value, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.BOOLEAN, value, status) { }
        public C_Result_Boolean(string title, bool value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.BOOLEAN, value, message, status) { }
        public C_Result_Boolean(string title, bool value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.BOOLEAN, value, message, details, status) { }
        public C_Result_Boolean(string title, C_Error error) : base(title, error) { }
        public C_Result_Boolean(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Boolean(string title, Exception exception) : base(title, exception) { }
        public C_Result_Boolean(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Boolean(bool value, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.BOOLEAN, value, status) { }
        public C_Result_Boolean(bool value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.BOOLEAN, value, message, status) { }
        public C_Result_Boolean(bool value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.BOOLEAN, value, message, details, status) { }
        public C_Result_Boolean(C_Error error) : base(error) { }
        public C_Result_Boolean(C_Error error, string message) : base(error, message) { }
        public C_Result_Boolean(Exception exception) : base(exception) { }
        public C_Result_Boolean(Exception exception, string message) : base(exception, message) { }

        protected void Set(bool value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.BOOLEAN, value, status);
        }
        protected void Set(bool value, string message, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.BOOLEAN, value, message, status);
        }
        protected void Set(bool value, string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.BOOLEAN, value, message, details, status);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Value: {this.Value.ToString()}.";
        }
        public new string Info()
        {
            return $"{base.Info()},\r\nValue: {this.Value.ToString()}.";
        }
    }
}
