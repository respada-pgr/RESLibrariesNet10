using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_LibraryUtils.Logger
{
    internal class C_LoggerRepository
    {
        private readonly long _id;
        private E_LoggerAddLineMode _addLineMode = E_LoggerAddLineMode.AT_END;
        private int _numLinesMax = 200;
        private int _idIndex = 1;
        private List<C_LogLine> _lines = new List<C_LogLine>();
        private readonly object _lockObj = new object();

        public long Id => _id;
        public E_LoggerAddLineMode AddLineMode { get => _addLineMode; set => _addLineMode = value; }
        public int NumLinesMax { get => _numLinesMax; set => _numLinesMax = value; }
        public int IdIndex { get => _idIndex; set => _idIndex = value; }
        public List<C_LogLine> Log { get => _lines; set => _lines = value; }
        public int NumLines { get => _lines.Count; }

        public C_LoggerRepository(long id, int maxLinesNumber = 2000, E_LoggerAddLineMode addLineMode = E_LoggerAddLineMode.AT_END)
        {
            this._id = id;
            this._addLineMode = addLineMode;
            this._numLinesMax = maxLinesNumber;
            this._idIndex = 1;
            this._lines = new List<C_LogLine>();
        }

        public C_LogLine Get(long id)
        {
            lock (_lockObj)
            {
                return _lines.Where(p => p.Id == id).FirstOrDefault();
            }
        }

        public List<C_LogLine> GetChildren(long parentId)
        {
            lock (_lockObj)
            {
                return _lines.Where(p => p.ParentId == parentId).ToList();
            }
        }

        public C_LoggedLine Add(C_LogLine line)
        {
            C_LoggedLine result = null;
            try
            {
                lock (_lockObj)
                {
                    if (AddLine(line))
                        result = new C_LoggedLine(line.Id, line);
                }
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_LoggerRepository.Add(C_LogLine line)", line.Info());
                throw;
            }
            return result;
        }

        public C_LoggedLine AddChild(long parentId, C_LogLine child)
        {
            C_LoggedLine result = null;
            try
            {
                if (parentId > 0)
                {
                    lock (_lockObj)
                    {
                        if (child == null)
                            throw new Exception("The param child line is null");

                        C_LogLine parent = _lines.Where(p => p.Id == parentId).FirstOrDefault();
                        if (parent != null && parent.Id > 0)
                        {
                            child.ParentId = parent.Id;
                            child.Title = parent.Title + (!string.IsNullOrEmpty(child.Title) ? " - " + child.Title : "");
                        }
                        else
                        {
                            child.Title = "<Not parent log line found>" + (!string.IsNullOrEmpty(child.Title) ? " - " + child.Title : "");
                        }

                        if (AddLine(child))
                            result = new C_LoggedLine(child.Id, child);
                    }
                }
                else
                {
                    child.Title = "<Not parent log line found>" + (!string.IsNullOrEmpty(child.Title) ? " - " + child.Title : "");
                    AddLine(child);
                    //throw new Exception("Cannot insert the child log line because parent id is undefined.");
                }
            }
            catch (Exception ex)
            {
                C_DebugLogger.Add(ex, "C_LoggerRepository.AddChild(long parentId, C_LogLine child))", child.Info());
                throw;
            }
            return result;
        }

        public C_LoggedLine AddChild(C_LogLine parent, C_LogLine child)
        {
            return this.AddChild(parent.Id, child);
        }

        public C_Result_Int Clean()
        {
            C_Result_Int result = new C_Result_Int("Clean log");
            try
            {
                lock (_lockObj)
                {
                    result.Value = this._lines.Count;
                    this._idIndex = 1;
                    this._lines = new List<C_LogLine>();
                    result.Set(result.Value + " deleted successfully!", E_ResultStatus.SUCCESS);
                    result.Success = this.AddLine(new C_LogLine(result, C_Mng_StackTrace.Get(), "Log configuration: " + ConfigInfo(), E_LogLineType.WARNING));
                }
            }
            catch (Exception ex)
            {
                result.Set(ex);
                this.AddLine(new C_LogLine(result, C_Mng_StackTrace.Get()));
            }
            return result;
        }

        public string ConfigInfo()
        {
            StringBuilder lines = new StringBuilder();
            lines.Append("Id: " + this._id.ToString() + ",");
            lines.AppendLine("Num Lines Max: " + NumLinesMax + ",");
            lines.AppendLine("Add Line Mode: " + AddLineMode.ToString() + "',");
            lines.AppendLine("Num Lines: " + Log.Count + ",");
            return lines.ToString();
        }

        private bool AddLine(C_LogLine line)
        {
            bool result = false;
            if (line.Id > 0)
            {
                if (_lines.Where(p => p.Id == line.Id).FirstOrDefault() == null)
                {
                    if (line.Id > IdIndex)
                        this._idIndex = (int)line.Id;
                }
                else
                {
                    throw new Exception("Cannot insert log line because id is already exists.");
                }
            }
            else
            {
                line.Id = IdIndex;
            }

            if (AddLineMode == E_LoggerAddLineMode.AT_START)
            {
                if (NumLinesMax > 0 && Log.Count > NumLinesMax)
                {
                    _lines.RemoveRange(NumLinesMax, Log.Count - NumLinesMax);
                }
                _lines.Insert(0, line); //inserta al principio
                result = true;
            }
            else
            {
                if (NumLinesMax > 0 && Log.Count > NumLinesMax)
                {
                    _lines.RemoveRange(0, Log.Count - NumLinesMax);
                }
                _lines.Add(line);
                result = true;
            }

            this.IdIndex++;
            return result;
        }
    }
}

