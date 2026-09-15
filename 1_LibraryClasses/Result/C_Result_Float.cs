using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Float : C_Result
    {
        public new float Value
        {
            get
            {
                return (float)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.FLOAT;
            }
        }

        public C_Result_Float() : base(E_ResultType.FLOAT, 0.0f) { }
        public C_Result_Float(E_ResultStatus status) : base(E_ResultType.FLOAT, 0.0f, status) { }
        public C_Result_Float(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.FLOAT, 0.0f, status) { }
        public C_Result_Float(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.FLOAT, 0.0f, message, status) { }
        public C_Result_Float(string title, float value, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.FLOAT, value, status) { }
        public C_Result_Float(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.FLOAT, 0.0f, message, details, status) { }
        public C_Result_Float(string title, float value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.FLOAT, value, message, status) { }
        public C_Result_Float(string title, float value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.FLOAT, value, message, details, status) { }
        public C_Result_Float(string title, C_Error error) : base(title, error) { }
        public C_Result_Float(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Float(string title, Exception exception) : base(title, exception) { }
        public C_Result_Float(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Float(float value, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.FLOAT, value, status) { }
        public C_Result_Float(float value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.FLOAT, value, message, status) { }
        public C_Result_Float(float value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.FLOAT, value, message, details, status) { }
        public C_Result_Float(C_Error error) : base(error) { }
        public C_Result_Float(C_Error error, string message) : base(error, message) { }
        public C_Result_Float(Exception exception) : base(exception) { }
        public C_Result_Float(Exception exception, string message) : base(exception, message) { }

        protected void Set(float value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.FLOAT, value, status);
        }
        protected void Set(float value, string message = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.FLOAT, value, message, status);
        }
        protected void Set(float value, string message, string details = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.FLOAT, value, message, details, status);
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
