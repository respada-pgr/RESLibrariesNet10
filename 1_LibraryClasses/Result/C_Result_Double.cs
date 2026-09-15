using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Double : C_Result
    {
        public new double Value
        {
            get
            {
                return (double)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.DOUBLE;
            }
        }

        public C_Result_Double() : base(E_ResultType.DOUBLE, 0.0d) { }
        public C_Result_Double(E_ResultStatus status) : base(E_ResultType.DOUBLE, 0.0d, status) { }
        public C_Result_Double(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.DOUBLE, 0.0d, status) { }
        public C_Result_Double(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.DOUBLE, 0.0d, message, status) { }
        public C_Result_Double(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.DOUBLE, 0.0d, message, details, status) { }
        public C_Result_Double(string title, double value, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.DOUBLE, value, status) { }
        public C_Result_Double(string title, double value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.DOUBLE, value, message, status) { }
        public C_Result_Double(string title, double value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.DOUBLE, value, message, details, status) { }
        public C_Result_Double(string title, C_Error error) : base(title, error) { }
        public C_Result_Double(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Double(string title, Exception exception) : base(title, exception) { }
        public C_Result_Double(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Double(double value, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.DOUBLE, value, status) { }
        public C_Result_Double(double value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.DOUBLE, value, message, status) { }
        public C_Result_Double(double value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.DOUBLE, value, message, details, status) { }
        public C_Result_Double(C_Error error) : base(error) { }
        public C_Result_Double(C_Error error, string message) : base(error, message) { }
        public C_Result_Double(Exception exception) : base(exception) { }
        public C_Result_Double(Exception exception, string message) : base(exception, message) { }

        protected void Set(double value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.DOUBLE, value, status);
        }
        protected void Set(double value, string message = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            base.Set(E_ResultType.DOUBLE, value, message, status);
        }
        protected void Set(double value, string message, string details = null, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.DOUBLE, value, message, details, status);
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
