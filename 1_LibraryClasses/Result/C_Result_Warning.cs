using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_Warning : C_Result
    {
        public C_Result_Warning() : base() { }
        public C_Result_Warning(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, status) { }
        public C_Result_Warning(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, new C_Warning(message), status) { }
        public C_Result_Warning(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(title, new C_Warning(message, details), status) { }
        public C_Result_Warning(string title, C_Warning warning) : base(title, warning) { }
        public C_Result_Warning(string title, C_Warning warning, string message) : base(title, warning, message) { }
        public C_Result_Warning(string title, C_Warning warning, string message, string details) : base(title, warning, message, details) { }
        public C_Result_Warning(string title, C_Error error) : base(title, error) { }
        public C_Result_Warning(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result_Warning(string title, C_Error error, string message, string details) : base(title, error, message, details) { }
        public C_Result_Warning(string title, C_Exception exception) : base(title, exception) { }
        public C_Result_Warning(string title, C_Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Warning(string title, C_Exception exception, string message, string details) : base(title, exception, message, details) { }
        public C_Result_Warning(string title, Exception exception) : base(title, exception) { }
        public C_Result_Warning(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result_Warning(string title, Exception exception, string message, string details) : base(title, exception, message, details) { }
        public C_Result_Warning(C_Warning warning, E_ResultStatus status = E_ResultStatus.ND) : base(warning, status) { }
        public C_Result_Warning(C_Warning warning, string message, E_ResultStatus status = E_ResultStatus.ND) : base(warning, message, status) { }
        public C_Result_Warning(C_Warning warning, string message, string details, E_ResultStatus status = E_ResultStatus.ND) : base(warning, message, details, status) { }
        public C_Result_Warning(C_Error error) : base(error) { }
        public C_Result_Warning(C_Error error, string message) : base(error, message) { }
        public C_Result_Warning(C_Error error, string message, string details) : base(error, message, details) { }
        public C_Result_Warning(C_Exception exception) : base(exception) { }
        public C_Result_Warning(C_Exception exception, string message) : base(exception, message) { }
        public C_Result_Warning(C_Exception exception, string message, string details) : base(exception, message, details) { }
        public C_Result_Warning(Exception exception) : base(exception) { }
        public C_Result_Warning(Exception exception, string message) : base(exception, message) { }
        public C_Result_Warning(Exception exception, string message, string details) : base(exception, message, details) { }
    }
}
