using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Exception : C_Result
    {
        public C_Result_Exception() : base() { }
        public C_Result_Exception(string title) : base(title) { }
        public C_Result_Exception(string title, string message) : base(title, new C_Exception(message)) { }
        public C_Result_Exception(string title, string message, Exception exception) : base(title, new C_Exception(message, exception)) { }
        public C_Result_Exception(string title, C_Exception exception) : base(title, exception) { }
        public C_Result_Exception(string title, C_Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Exception(string title, Exception exception) : base(title, exception) { }
        public C_Result_Exception(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Exception(C_Exception exception) : base(exception) { }
        public C_Result_Exception(C_Exception exception, string message) : base(exception, message) { }
        public C_Result_Exception(Exception exception) : base(exception) { }
        public C_Result_Exception(Exception exception, string message) : base(exception, message) { }

        public override string ToString()
        {
            return $"{base.ToString()}, Exception: {this.Exception.Message}";
        }
        public new string Info()
        {
            return $"{base.ToString()}\r\nException: {this.Exception.Details}";
        }
    }
}
