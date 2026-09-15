using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using _1_LibraryClassesNet10.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace _2_LibraryUtils.Logger
{
    public static class C_Logger
    {
        internal const string CONST_DEFAULT_LOG_NAME = "logger.log";

        private static string _version = "v3";
        private static string _filePath = CONST_DEFAULT_LOG_NAME;
        private static E_LogLineType _logLevel = E_LogLineType.ND;
        private static C_LoggerRepository _repository = null;

        public static E_LogLineType LogLevel { get => _logLevel; set => _logLevel = value; }
        public static E_LoggerAddLineMode AddLineMode { get => _repository.AddLineMode; set => _repository.AddLineMode = value; }
        public static string FilePath { get => _filePath; set => _filePath = value; }
        public static int NumLinesMax { get => _repository.NumLinesMax; set => _repository.NumLinesMax = value; }
        public static List<C_LogLine> Log { get => _repository.Log; }


        public static C_LoggedLine Init(string filePath = CONST_DEFAULT_LOG_NAME
                                , E_LogLineType logLevel = E_LogLineType.ND
                                , E_LoggerAddLineMode addLineMode = E_LoggerAddLineMode.AT_END
                                , int maxLinesNumber = 500)
        {
            Thread.Sleep(100); // Para evitar que se creen logs en el mismo momento

            _filePath = filePath;
            _logLevel = logLevel;

            _repository = new C_LoggerRepository(DateTime.Now.Ticks, maxLinesNumber, addLineMode);

            return _repository.Add(new C_LogLine("Initialize logger", "Success!", Info(), C_Mng_StackTrace.Get(), E_LogLineType.INFORMATIVE));
        }

        public static void Set(string filePath = CONST_DEFAULT_LOG_NAME
                                , E_LogLineType logLevel = E_LogLineType.WARNING
                                , E_LoggerAddLineMode addLineMode = E_LoggerAddLineMode.AT_END
                                , int maxLinesNumber = 1000)
        {
            _filePath = filePath;
            _logLevel = logLevel;

            _repository.AddLineMode = addLineMode;
            _repository.AddLineMode = addLineMode;
            _repository.NumLinesMax = maxLinesNumber;
        }

        #region ADD METHODS
        public static C_Result_Logged Add(string title, E_LogLineType log_type = E_LogLineType.ND, E_ResultStatus status = E_ResultStatus.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, status);
            C_LoggedLine line = _repository.Add(new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(string title, string message, E_LogLineType log_type = E_LogLineType.ND, E_ResultStatus status = E_ResultStatus.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, message, status);
            C_LoggedLine line = _repository.Add(new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(string title, string message, string details, E_LogLineType log_type = E_LogLineType.ND, E_ResultStatus status = E_ResultStatus.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, message, details, status);
            C_LoggedLine line = _repository.Add(new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(C_Result element)
        {
            C_LoggedLine line = _repository.Add(new C_LogLine(element, C_Mng_StackTrace.Get()));
            C_Result_Logged result = new C_Result_Logged(element);
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(C_Result element, E_LogLineType log_type)
        {
            C_LoggedLine line = _repository.Add(new C_LogLine(element, C_Mng_StackTrace.Get(), log_type));
            C_Result_Logged result = new C_Result_Logged(element);
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(C_Result element, string details)
        {
            C_LoggedLine line = _repository.Add(new C_LogLine(element, C_Mng_StackTrace.Get(), details));
            C_Result_Logged result = new C_Result_Logged(element);
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(C_Result element, string details, E_LogLineType log_type)
        {
            C_LoggedLine line = _repository.Add(new C_LogLine(element, C_Mng_StackTrace.Get(), details, log_type));
            C_Result_Logged result = new C_Result_Logged(element);
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(C_Error error, string title, string message = null)
        {
            C_LoggedLine line = _repository.Add(new C_LogLine(error, C_Mng_StackTrace.Get(), message));
            C_Result_Logged result = new C_Result_Logged(title, error, message);
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged Add(Exception ex, string title, string message = null)
        {
            C_LoggedLine line = _repository.Add(new C_LogLine(ex, C_Mng_StackTrace.Get(), title, message));
            C_Result_Logged result = new C_Result_Logged(title, ex, message);
            result.LoggedLine = line;
            return result;
        }
        #endregion

        #region ADD CHILD METHODS
        public static C_Result_Logged AddChild(C_Result_Logged parent, string title, E_LogLineType log_type)
        {
            C_Result_Logged result = new C_Result_Logged(title);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, string title, E_ResultStatus status = E_ResultStatus.ND, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, status);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, string title, string message, E_LogLineType log_type)
        {
            C_Result_Logged result = new C_Result_Logged(title, message);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, string title, string message, E_ResultStatus status = E_ResultStatus.ND, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, message, status);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, string title, string message, string details, E_LogLineType log_type)
        {
            C_Result_Logged result = new C_Result_Logged(title, message, details);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, message, details, status);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Result child, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(child);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Result child, string message, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(child, message);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Result child, string message, string details, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(child, message, details);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Result_Logged child, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(child);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Result_Logged child, string message, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(child, message);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Result_Logged child, string message, string details, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(child, message, details);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, C_Error child, string title = null, string message = null, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, child, message);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        public static C_Result_Logged AddChild(C_Result_Logged parent, Exception child, string title = null, string message = null, E_LogLineType log_type = E_LogLineType.ND)
        {
            C_Result_Logged result = new C_Result_Logged(title, child, message);
            C_LoggedLine line = _repository.AddChild(parent.LoggedLine.Id, new C_LogLine(result, C_Mng_StackTrace.Get(), log_type));
            result.LoggedLine = line;
            return result;
        }
        #endregion        

        public static C_LogLine Get(long Id)
        {
            return _repository.Get(Id);
        }

        public static List<C_LogLine> GetChildren(long parentId)
        {
            return _repository.GetChildren(parentId);
        }

        public static C_Result_Int Clean()
        {
            return _repository.Clean();
        }

        public static C_Result_Logged CreateLogFile(E_LogLineType logLevel)
        {
            C_Result_Logged r_log = Add(new C_Result("Logger: Create file log", "Start"));
            SaveAllToLocalFile(logLevel);
            r_log.Success = true;
            Add(r_log);
            return r_log;
        }

        public static void SaveIfErrorToLocalFile(E_LogLineType log_typeLevel = E_LogLineType.ERROR, bool addDateTimeToLine = true, bool addDateTimeToFileName = false)
        {
            if (_logLevel >= E_LogLineType.ERROR)
            {
                SaveAllToLocalFile(_filePath, log_typeLevel, addDateTimeToLine, addDateTimeToFileName);
            }
        }

        public static void SaveIfErrorToLocalFile(string fileName, E_LogLineType log_typeLevel = E_LogLineType.ERROR, bool addDateTimeToLine = true, bool addDateTimeToFileName = false)
        {
            if (_logLevel >= E_LogLineType.ERROR)
            {
                SaveAllToLocalFile(fileName, log_typeLevel, addDateTimeToLine, addDateTimeToFileName);
            }
        }

        public static string SaveAllToLocalFile(E_LogLineType log_typeLevel = E_LogLineType.ERROR, bool addDateTimeToLine = true, bool addDateTimeToFileName = false)
        {
            return SaveAllToLocalFile(_filePath, log_typeLevel, addDateTimeToLine, addDateTimeToFileName);
        }

        public static string SaveAllToLocalFile(string pathLogFile, E_LogLineType log_typeLevel = E_LogLineType.ERROR, bool addDateTimeToLine = true, bool addDateTimeToFileName = false)
        {
            string resultPath = "";
            C_Result_Logged r_log = C_Logger.Add("Logger: Save log", "Start", "File name: " + pathLogFile);
            try
            {
                string fileName = Path.GetFileName(pathLogFile);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathLogFile);
                string fExtension = Path.GetExtension(pathLogFile);
                string route = pathLogFile.Replace(fileName, "");
                string dateTime = addDateTimeToFileName ? "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") : "";

                resultPath = route + fileNameWithoutExtension + dateTime + fExtension;

                XmlDocument xml = ToXmlDocument(log_typeLevel, addDateTimeToLine);
                xml.Save(resultPath);
                r_log.Success = true;
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_Logger.SaveAllToLocalFile");
                r_log.Set(ex, "C_Logger: Exception to save log");
            }
            Add(r_log);
            return resultPath;
        }

        public static XmlDocument ToXmlDocument(E_LogLineType log_typeLevel = E_LogLineType.ND, bool addCreateDate = true)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            XmlNode root = doc.CreateElement("root"); // This overload assumes the document already knows about the rdf schema as it is in the Schemas set
            doc.AppendChild(root);

            //doc.InsertBefore(xmlDeclaration, root);

            int idLine = 0;
            foreach (C_LogLine line in Log)
            {
                idLine++;
                if (line.Type >= log_typeLevel)
                {
                    XmlElement element = line.ToXmlElement(doc, addCreateDate);
                    root.AppendChild(element);
                }
            }
            return doc;
        }

        public static string Info()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("Logger version: '" + (_version ?? "") + "',");
            text.AppendLine("File path: '" + (FilePath ?? "") + "',");
            text.AppendLine("Log level: " + LogLevel.ToString() + "',");
            text.AppendLine("Add Line Mode: " + AddLineMode.ToString() + "',");
            text.AppendLine("Num Lines Max: " + NumLinesMax + ",");
            text.AppendLine("Num. Lines: " + _repository.Log.Count + ",");
            text.AppendLine("Index: " + _repository.IdIndex + ",");
            return text.ToString();
        }
    }
}
