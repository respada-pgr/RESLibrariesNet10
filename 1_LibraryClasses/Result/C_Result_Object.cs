using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Object : C_Result
    {
        public Object Object
        {
            get
            {
                return (Object)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.OBJECT;
            }
        }

        public C_Result_Object() : base() { }
        public C_Result_Object(E_ResultStatus status) : base(status) { }
        public C_Result_Object(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, status) { }
        public C_Result_Object(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, message, status) { }
        public C_Result_Object(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, message, details, status) { }
        public C_Result_Object(string title, Object value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.OBJECT, value, message, status) { }
        public C_Result_Object(string title, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.OBJECT, value, message, details, status) { }
        public C_Result_Object(string title, C_Error error) : base(title, error) { }
        public C_Result_Object(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Object(string title, Exception exception) : base(title, exception) { }
        public C_Result_Object(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Object(Object value, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.OBJECT, value, status) { }
        public C_Result_Object(Object value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.OBJECT, value, message, status) { }
        public C_Result_Object(Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(E_ResultType.OBJECT, value, message, details, status) { }
        public C_Result_Object(C_Result result, Object value, E_ResultStatus status = E_ResultStatus.ND) : base(result, value, status) { }
        public C_Result_Object(C_Result result, Object value, string message, E_ResultStatus status = E_ResultStatus.ND) : base(result, value, message, status) { }
        public C_Result_Object(C_Result result, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(result, value, message, details, status) { }
        public C_Result_Object(C_Error error) : base(error) { }
        public C_Result_Object(C_Error error, string message) : base(error, message) { }
        public C_Result_Object(Exception exception) : base(exception) { }
        public C_Result_Object(Exception exception, string message) : base(exception, message) { }

        public new void Set(C_Result result, Object value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            if (value != null)
            {
                base.Set(result, value, status);
            }
            else
            {
                throw new ArgumentNullException("value");
            }
        }

        public override string ToString()
        {
            string result = base.ToString();
            if (this.Object != null)
                result += ". " + this.Object.ToString();
            return result;
        }
        public new string Info()
        {
            string result = base.Info();
            if (this.Object != null)
                result += "\r\nObject: " + this.Object.ToString();
            return result;
        }
    }
}
