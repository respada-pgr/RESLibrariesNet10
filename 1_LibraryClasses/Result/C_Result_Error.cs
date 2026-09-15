using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Error : C_Result
    {
        public C_Result_Error() : base() { }
        public C_Result_Error(string title) : base(title) { }
        public C_Result_Error(string title, string message) : base(title, new C_Error(message)) { }
        public C_Result_Error(string title, string message, string details) : base(title, new C_Error(message, details)) { }
        public C_Result_Error(string title, C_Error error) : base(title, error) { }
        public C_Result_Error(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Error(string title, C_Error error, string message, string details) : base(title, error, message, details) { }
        public C_Result_Error(string title, C_Exception exception) : base(title, exception) { }
        public C_Result_Error(string title, C_Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Error(string title, C_Exception exception, string message, string details) : base(title, exception, message, details) { }
        public C_Result_Error(string title, Exception exception) : base(title, exception) { }
        public C_Result_Error(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Error(string title, Exception exception, string message, string details) : base(title, exception, message, details) { }
        public C_Result_Error(C_Error error) : base(error) { }
        public C_Result_Error(C_Error error, string message) : base(error, message) { }
        public C_Result_Error(C_Error error, string message, string details) : base(error, message, details) { }
        public C_Result_Error(C_Exception exception) : base(exception) { }
        public C_Result_Error(C_Exception exception, string message) : base(exception, message) { }
        public C_Result_Error(C_Exception exception, string message, string details) : base(exception, message, details) { }
        public C_Result_Error(Exception exception) : base(exception) { }
        public C_Result_Error(Exception exception, string message) : base(exception, message) { }
        public C_Result_Error(Exception exception, string message, string details) : base(exception, message, details) { }
    }
}
