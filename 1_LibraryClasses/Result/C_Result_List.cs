using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result_List : C_Result
    {
        public new List<C_Result> Value
        {
            get
            {
                return (List<C_Result>)base.Value;
            }
            set
            {
                base.Value = value;
                base.Type = E_ResultType.LIST;
            }
        }

        public C_Result_List() : base(E_ResultType.LIST)
        {
            this.Value = new List<C_Result>();
        }
        public C_Result_List(E_ResultStatus status) : base(E_ResultType.LIST, status)
        {
            this.Value = new List<C_Result>();
        }
        public C_Result_List(string title, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LIST, status)
        {
            this.Value = new List<C_Result>();
        }
        public C_Result_List(string title, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, E_ResultType.LIST, message, status)
        {
            this.Value = new List<C_Result>();
        }
        public C_Result_List(string title, C_Result result) : base(title, E_ResultType.LIST, result.Status)
        {
            this.Value = new List<C_Result>();
            Value.Add(result);
        }
        public C_Result_List(string title, C_Result result, string message) : base(title, E_ResultType.LIST, message)
        {
            this.Value = new List<C_Result>();
            Value.Add(result);
        }
        public C_Result_List(string title, C_Error error) : base(title, error)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Error(error));
        }
        public C_Result_List(string title, C_Error error, string message) : base(title, error, message)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Error(error));
        }
        public C_Result_List(string title, Exception exception) : base(title, exception)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Exception(exception));
        }
        public C_Result_List(string title, Exception exception, string message = null) : base(title, exception, message)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Exception(exception));
        }
        public C_Result_List(C_Error error) : base(error)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Error(error));
        }
        public C_Result_List(C_Error error, string message) : base(error, message)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Error(error));
        }
        public C_Result_List(Exception exception) : base(exception)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Exception(exception));
        }
        public C_Result_List(Exception exception, string message) : base(exception, message)
        {
            this.Value = new List<C_Result>();
            Value.Add(new C_Result_Exception(exception));
        }

        public new void Add(C_Result result)
        {
            base.Add(result.Status);
            this.Message = !string.IsNullOrEmpty(result.Message) ? result.Message : this.Message;
            Value.Add(result);
        }
        public void Add(C_Result result, string message)
        {
            base.Add(result.Status);
            this.Message = message;
            Value.Add(result);
        }
        public new void Add(C_Error error)
        {
            this.Add(new C_Result_Error(error));
        }
        public void Add(C_Error error, string message)
        {
            this.Add(new C_Result_Error(error), message);
        }
        public new void Add(Exception exception)
        {
            this.Add(new C_Result_Exception(exception));
        }
        public void Add(Exception exception, string message)
        {
            this.Add(new C_Result_Exception(exception), message);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Count: {this.Value.Count.ToString()}.";
        }
        public new string Info()
        {
            StringBuilder result = new StringBuilder();

            string line = base.ToString();
            result.AppendLine(line);

            if (this.Value != null && this.Value.Count > 0)
            {
                int i = 1;
                result.AppendLine("Values:");
                foreach (C_Result item in this.Value)
                {
                    switch (item.Type)
                    {
                        case E_ResultType.ND:
                            result.AppendLine(i + ": " + item.ToString());
                            break;
                        case E_ResultType.BOOLEAN:
                            result.AppendLine(i + ": " + ((C_Result_Boolean)item).ToString());
                            break;
                        case E_ResultType.INT:
                            result.AppendLine(i + ": " + ((C_Result_Int)item).Info());
                            break;
                        case E_ResultType.LONG:
                            result.AppendLine(i + ": " + ((C_Result_Long)item).ToString());
                            break;
                        case E_ResultType.FLOAT:
                            result.AppendLine(i + ": " + ((C_Result_Float)item).Info());
                            break;
                        case E_ResultType.DOUBLE:
                            result.AppendLine(i + ": " + ((C_Result_Double)item).ToString());
                            break;
                        case E_ResultType.STRING:
                            result.AppendLine(i + ": " + ((C_Result_String)item).ToString());
                            break;
                        case E_ResultType.LIST:
                            result.AppendLine(i + ": " + ((C_Result_List)item).ToString());
                            break;
                        case E_ResultType.OBJECT:
                            result.AppendLine(i + ": " + ((C_Result_Object)item).ToString());
                            break;
                        default:
                            result.AppendLine(i + ": " + item.ToString());
                            break;
                    }
                }
            }
            return result.ToString();
        }
    }
}
