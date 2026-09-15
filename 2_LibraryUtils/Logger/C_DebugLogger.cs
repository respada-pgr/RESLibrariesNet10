using _1_LibraryClassesNet10.Event;
using _1_LibraryClassesNet10.Result;
using System;
using System.Diagnostics;
using System.Reflection;

namespace _2_LibraryUtils.Logger
{
    public static class C_DebugLogger
    {
        private static long _indId = 1;

        public static void Ini()
        {
            _indId = 1;
        }

        public static void Add(string title, E_LogLineType logType = E_LogLineType.ND)
        {
            Add(new C_LogLine(title, GetStackTrace(), logType));
        }

        public static void Add(string title, string message, E_LogLineType logType = E_LogLineType.ND)
        {
            Add(new C_LogLine(title, message, GetStackTrace(), logType));
        }

        public static void Add(string title, string message, string details, E_LogLineType logType = E_LogLineType.ND)
        {
            Add(new C_LogLine(title, message, details, GetStackTrace(), logType));
        }

        public static void Add(C_Result element, string details = null)
        {
            Add(new C_LogLine(element, GetStackTrace(), details));
        }

        public static void Add(C_Error error, string title, string message = null)
        {
            Add(new C_LogLine(error, GetStackTrace(), message));
        }

        public static void Add(Exception ex, string title, string message = null)
        {
            Add(new C_LogLine(ex, GetStackTrace(), title, message));
        }

        public static void Add(C_LogLine loggerLine)
        {
            loggerLine.Id = _indId++;
            Debug.WriteLine(loggerLine.Info());
        }

        public static C_StackTraceInfo GetStackTrace(int methodsToSkip = 1)
        {
            C_StackTraceInfo result = new C_StackTraceInfo();
            try
            {
                StackTrace trace = new StackTrace(StackTrace.METHODS_TO_SKIP + methodsToSkip, true);
                StackFrame frame = trace.GetFrame(0);
                if (frame != null)
                {
                    MethodBase caller = frame.GetMethod();
                    if (caller != null)
                    {
                        try { result.Assembly = caller.ReflectedType.Assembly.ToString(); } catch { }
                        try { result.Namespace = caller.ReflectedType.Namespace; } catch { }
                        try { result.Class = caller.ReflectedType.Name; } catch { }
                        try { result.Method = caller.Name; } catch { }
                    }

                    try { result.CodeFilePath = frame.GetFileName(); } catch { }
                    try { result.CodeLineNumber = frame.GetFileLineNumber(); } catch { }
                }
            }
            catch { }
            return result;
        }
    }
}
