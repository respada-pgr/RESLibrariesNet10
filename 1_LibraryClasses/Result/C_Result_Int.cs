using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Int : C_Result
    {
        public new int Value
        {
            get
            {
                return (int)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.INT;
            }
        }

        public C_Result_Int() : base(E_ResultType.INT, 0, E_ResultStatus.ND) { }
        public C_Result_Int(E_ResultStatus status) : base(E_ResultType.INT, 0, status) { }
        public C_Result_Int(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.INT, 0, status) { }
        public C_Result_Int(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.INT, 0, message, status) { }
        public C_Result_Int(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.INT, 0, message, details, status) { }
        public C_Result_Int(string title, int value, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.INT, value, status) { }
        public C_Result_Int(string title, int value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.INT, value, message, status) { }
        public C_Result_Int(string title, int value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.INT, value, message, details, status) { }
        public C_Result_Int(string title, C_Error error) : base(title, error) { }
        public C_Result_Int(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Int(string title, Exception exception) : base(title, exception) { }
        public C_Result_Int(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Int(int value, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.INT, value, status) { }
        public C_Result_Int(int value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.INT, value, message, status) { }
        public C_Result_Int(int value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.INT, value, message, details, status) { }
        public C_Result_Int(C_Error error) : base(error) { }
        public C_Result_Int(C_Error error, string message) : base(error, message) { }
        public C_Result_Int(Exception exception) : base(exception) { }
        public C_Result_Int(Exception exception, string message) : base(exception, message) { }

        public void SetSuccess(int value)
        {
            base.SetSuccess(value);
        }
        public void SetSuccess(int value, string details)
        {
            base.SetSuccess(value, details);
        }
        public void Set(int value)
        {
            base.Set(E_ResultType.INT, value, value > 0 ? E_ResultStatus.SUCCESS : E_ResultStatus.FAIL);
        }
        public void Set(int value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.INT, value, status);
        }
        public void Set(int value, string message = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.INT, value, message, status);
        }
        public void Set(int value, string message, string details = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.INT, value, message, details, status);
        }
        public void Set(C_Result_Int result)
        {
            this.Set(result.Value, result.Message, result.Details, result.Status);
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
