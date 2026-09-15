using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Long : C_Result
    {
        public new long Value
        {
            get
            {
                return (long)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.LONG;
            }
        }

        public C_Result_Long() : base(E_ResultType.LONG, 0) { }
        public C_Result_Long(E_ResultStatus status) : base(E_ResultType.LONG, 0, status) { }
        public C_Result_Long(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LONG, 0, status) { }
        public C_Result_Long(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LONG, 0, message, status) { }
        public C_Result_Long(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LONG, 0, message, details, status) { }
        public C_Result_Long(string title, long value, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LONG, value, status) { }
        public C_Result_Long(string title, long value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LONG, value, message, status) { }
        public C_Result_Long(string title, long value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LONG, value, message, details, status) { }
        public C_Result_Long(string title, C_Error error) : base(title, error) { }
        public C_Result_Long(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Long(string title, Exception exception) : base(title, exception) { }
        public C_Result_Long(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Long(long value, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LONG, value, status) { }
        public C_Result_Long(long value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LONG, value, message, status) { }
        public C_Result_Long(long value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.LONG, value, message, details, status) { }
        public C_Result_Long(C_Error error) : base(error) { }
        public C_Result_Long(C_Error error, string message) : base(error, message) { }
        public C_Result_Long(Exception exception) : base(exception) { }
        public C_Result_Long(Exception exception, string message) : base(exception, message) { }

        protected void Set(long value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.LONG, value, status);
        }
        protected void Set(long value, string message = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.LONG, value, message, status);
        }
        protected void Set(long value, string message, string details = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.LONG, value, message, details, status);
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
