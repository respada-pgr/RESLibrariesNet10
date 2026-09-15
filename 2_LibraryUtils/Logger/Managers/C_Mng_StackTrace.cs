using System.Diagnostics;
using System.Reflection;

namespace _2_LibraryUtils.Logger
{
    public static class C_Mng_StackTrace
    {
        public static C_StackTraceInfo Get(int methodsToSkip = 2)
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
