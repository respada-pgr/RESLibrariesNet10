using System;

namespace _2_LibraryUtils.Logger
{
    [Serializable]
    public class C_LoggerException : Exception
    {
        public string Trace { get; }

        public C_LoggerException() { }

        public C_LoggerException(string message)
            : base(message) { }

        public C_LoggerException(string message, Exception ex)
            : base(message, ex)
        {
            if (ex != null)
                Trace = ex.Message ?? "";
        }

        public C_LoggerException(string message, string trace)
            : this(message)
        {
            Trace = trace;
        }
    }
}
