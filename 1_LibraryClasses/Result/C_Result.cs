using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Event;
using _1_LibraryClassesNet10.Time;
using System;
using System.Text;

namespace _1_LibraryClassesNet10.Result
{
    public class C_Result<T> : C_Result
    {
        public new T Value
        {
            get => (T)base.Value;
            set
            {
                base.Value = value;
                base.Type = E_ResultType.OBJECT;
            }
        }

        #region CONSTRUCTORS
        public C_Result() { }
        public C_Result(E_ResultStatus status) : base(status) { }
        public C_Result(string title, string message = null, E_ResultStatus status = E_ResultStatus.ND) : base(title, message, status) { }
        public C_Result(string title, T value, string message = null, E_ResultStatus status = E_ResultStatus.SUCCESS) : base(title, value, message, status) { }
        public C_Result(string title, E_ResultType type, T value, string message, E_ResultStatus status = E_ResultStatus.SUCCESS) : base(title, type, value, message, status) { }
        public C_Result(string title, E_ResultType type, T value, string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS) : base(title, type, value, message, details, status) { }
        public C_Result(string title, C_Result result, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, status) { }
        public C_Result(string title, C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, message, status) { }
        public C_Result(string title, C_Result<T> result, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, status)
        {
            this.Value = result.Value;
        }
        public C_Result(string title, C_Result<T> result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(title, result, message, status)
        {
            this.Value = result.Value;
        }
        public C_Result(string title, C_Error error) : base(title, error) { }
        public C_Result(string title, C_Error error, string message) : base(title, error, message) { }
        public C_Result(string title, C_Exception exception) : base(title, exception) { }
        public C_Result(string title, C_Exception exception, string message) : base(title, exception, message) { }
        public C_Result(string title, Exception exception) : base(title, exception) { }
        public C_Result(string title, Exception exception, string message) : base(title, exception, message) { }
        public C_Result(T value, E_ResultStatus status = E_ResultStatus.SUCCESS) : base(value, status) { }
        public C_Result(E_ResultType type, T value, E_ResultStatus status = E_ResultStatus.SUCCESS) : base(type, value, status) { }
        public C_Result(E_ResultType type, T value, string message, E_ResultStatus status = E_ResultStatus.SUCCESS) : base(type, value, message, status) { }
        public C_Result(C_Result result, E_ResultStatus status = E_ResultStatus.ND) : base(result, status) { }
        public C_Result(C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(result, message, status) { }
        public C_Result(C_Result<T> result, E_ResultStatus status = E_ResultStatus.ND) : base(result, status)
        {
            this.Value = result.Value;
        }
        public C_Result(C_Result<T> result, string message, E_ResultStatus status = E_ResultStatus.ND) : base(result, message, status)
        {
            this.Value = result.Value;
        }
        public C_Result(C_Error error) : base(error) { }
        public C_Result(C_Error error, string message) : base(error, message) { }
        public C_Result(C_Exception exception) : base(exception) { }
        public C_Result(C_Exception exception, string message) : base(exception, message) { }
        public C_Result(Exception exception) : base(exception) { }
        public C_Result(Exception exception, string message) : base(exception, message) { }
        #endregion
    }

    public class C_Result
    {
        private E_ResultType _type = E_ResultType.ND;
        private E_ResultStatus _status = E_ResultStatus.ND;
        private C_DateTime _dateTime = new C_DateTime(System.DateTime.Now);
        private string _title = "";
        private string _message = null;
        private string _details = null;
        private Object _value = null;
        private Object _log_info = null;
        private C_Exception _exception = null;

        public C_DateTime DateTime { get => _dateTime; }
        public E_ResultType Type { get => this._type; set => this._type = value; }
        public E_ResultStatus Status
        {
            get => this._status;
            set
            {
                this._status = value;
                this._dateTime.DateTime = System.DateTime.Now;

                if (this.Status == E_ResultStatus.SUCCESS)
                {
                    if (this.Type == E_ResultType.WARNING)
                        this._message = "Warning! Success";
                    else
                    {
                        this._message = "Success!";
                    }
                }
                else if (this.Status == E_ResultStatus.FAIL)
                {
                    if (this.Type == E_ResultType.EXCEPTION)
                        this._message = "Exception!!";
                    else if (this.Type == E_ResultType.ERROR)
                        this._message = "Error!!";
                    else if (this.Type == E_ResultType.WARNING)
                        this._message = "Warning! Fail";
                    else
                        this._message = "Fail!";
                }
                else
                {
                    this.Type = E_ResultType.ND;
                    this._message = "";
                }
            }
        }
        public string Title { get => this._title; set => this._title = value; }
        public string Message { get => this._message ?? _status.ToString(); set => this._message = value; }
        public string Details { get => this._details; set => this._details = value; }

        public C_Exception Exception { get => this._exception; }
        public object Value
        {
            get => this._value;
            set
            {
                this._value = value;
            }
        }
        public object LogInfo
        {
            get => this._log_info;
            set
            {
                this._log_info = value;
            }
        }
        public bool Logged { get => this._log_info != null; }
        public bool Success
        {
            get => this._status == E_ResultStatus.SUCCESS;
            set => this.Status = value ? E_ResultStatus.SUCCESS : E_ResultStatus.ND;
        }
        public bool Fail
        {
            get => this._status == E_ResultStatus.FAIL;
            set => this.Status = value ? E_ResultStatus.FAIL : E_ResultStatus.ND;
        }

        #region PUBLIC CONSTRUCTORS
        public C_Result()
        {
        }
        public C_Result(E_ResultStatus status)
        {
            this.Status = status;
        }
        public C_Result(string title, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Status = status;
        }
        public C_Result(string title, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(message, status);
        }
        public C_Result(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(message, details, status);
        }
        public C_Result(string title, C_Result result, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? result.Title;
            this.Set(result, status);
        }
        public C_Result(string title, C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? result.Title;
            this.Set(result, message, status);
        }
        public C_Result(string title, C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? result.Title;
            this.Set(result, message, details, status);
        }
        public C_Result(string title, C_Warning warning, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "Warning!";
            this.Set(warning);
        }
        public C_Result(string title, C_Warning warning, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "Warning!";
            this.Set(warning, message);
        }
        public C_Result(string title, C_Warning warning, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "Warning!";
            this.Set(warning, message, details);
        }
        public C_Result(string title, C_Error error)
        {
            this._title = title ?? "Error!";
            this.Set(error);
        }
        public C_Result(string title, C_Error error, string message)
        {
            this._title = title ?? "Error!";
            this.Set(error, message);
        }
        public C_Result(string title, C_Error error, string message, string details)
        {
            this._title = title ?? "Error!";
            this.Set(error, message, details);
        }
        public C_Result(string title, C_Exception exception)
        {
            this._title = title ?? "Exception";
            this.Set(exception);
        }
        public C_Result(string title, C_Exception exception, string message)
        {
            this._title = title ?? "Exception";
            this.Set(exception, message);
        }
        public C_Result(string title, C_Exception exception, string message, string details)
        {
            this._title = title ?? "Exception";
            this.Set(exception, message, details);
        }
        public C_Result(string title, Exception exception)
        {
            this._title = title ?? "Exception";
            this.Set(exception);
        }
        public C_Result(string title, Exception exception, string message)
        {
            this._title = title ?? "Exception";
            this.Set(exception, message);
        }
        public C_Result(string title, Exception exception, string message, string details)
        {
            this._title = title ?? "Exception";
            this.Set(exception, message, details);
        }

        public C_Result(C_Result result, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = result.Title ?? "";
            this.Set(result, status);
        }
        public C_Result(C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(result, message, status);
        }
        public C_Result(C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(result, message, details, status);
        }
        public C_Result(C_Warning warning, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "Warning!";
            this.Set(warning, status);
        }
        public C_Result(C_Warning warning, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "Warning!";
            this.Set(warning, message, status);
        }
        public C_Result(C_Warning warning, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "Warning!";
            this.Set(warning, message, details, status);
        }
        public C_Result(C_Error error)
        {
            this._title = "Error!";
            this.Set(error);
        }
        public C_Result(C_Error error, string message)
        {
            this._title = "Error!";
            this.Set(error, message);
        }
        public C_Result(C_Error error, string message, string details)
        {
            this._title = "Error!";
            this.Set(error, message, details);
        }
        public C_Result(C_Exception exception)
        {
            this._title = "Exception!";
            this.Set(exception);
        }
        public C_Result(C_Exception exception, string message)
        {
            this._title = "Exception!";
            this.Set(exception, message);
        }
        public C_Result(C_Exception exception, string message, string details)
        {
            this._title = "Exception!";
            this.Set(exception, message, details);
        }
        public C_Result(Exception exception)
        {
            this._title = "Exception!";
            this.Set(exception);
        }
        public C_Result(Exception exception, string message)
        {
            this._title = "Exception!";
            this.Set(exception, message);
        }
        public C_Result(Exception exception, string message, string details)
        {
            this._title = "Exception!";
            this.Set(exception, message, details);
        }
        #endregion

        #region PUBLIC METHODS
        public void Set(bool result)
        {
            this.Status = result ? E_ResultStatus.SUCCESS : E_ResultStatus.FAIL;
        }
        public void Set(bool result, string details)
        {
            this.Set(result);
            this.AddDetails(details);
        }
        public void Set(bool result, string details, string message)
        {
            this.Set(result, message);
            this.AddDetails(details);
        }

        public void Set(E_ResultStatus status)
        {
            this.Status = status;
        }
        public void Set(string message, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Status = status;
            this._message = message;
        }
        public void Set(string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(message, status);
            this.AddDetails(details);
        }
        public void Set(C_Result result, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._type = result.Type;
            this.Status = status != E_ResultStatus.ND ? status : result.Status;
            this._message = result.Message;
            this.AddDetails(result);
            this._value = result.Value;
            this._log_info = result.LogInfo;
        }
        public void Set(C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this.Set(result, status);
            this._message = message ?? this._message;
        }
        public void Set(C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this.Set(result, message, status);
            this.AddDetails(details);
        }
        public void Set(C_Warning warning, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._type = E_ResultType.WARNING;
            this.Status = status != E_ResultStatus.ND ? status : warning.ErrorType == E_ErrorType.ND ? E_ResultStatus.SUCCESS : E_ResultStatus.FAIL;
            this._message = warning.Message;
            this.AddDetails(warning);
            this.Value = warning;
        }
        public void Set(C_Warning warning, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this.Set(warning, status);
            this._message = message;
        }
        public void Set(C_Warning warning, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this.Set(warning, message, status);
            this.AddDetails(details);
        }
        public void Set(C_Error error)
        {
            this._type = E_ResultType.ERROR;
            this.Status = E_ResultStatus.FAIL;
            this._message = error.Message;
            this.AddDetails(error);
            this.Value = error;
        }
        public void Set(C_Error error, string message)
        {
            this.Set(error);
            this._message = message;
        }
        public void Set(C_Error error, string message, string details)
        {
            this.Set(error, message);
            this.AddDetails(details);
        }
        public void Set(C_Exception exception)
        {
            Set(new C_Error(exception));
            this._exception = exception;
        }
        public void Set(C_Exception exception, string message)
        {
            this.Set(exception);
            this._message = message;
        }
        public void Set(C_Exception exception, string message, string details)
        {
            this.Set(exception, message);
            this.AddDetails(details);
        }
        public void Set(Exception exception)
        {
            this.Set(new C_Exception(exception));
        }
        public void Set(Exception exception, string message)
        {
            this.Set(exception);
            this._message = message;
        }
        public void Set(Exception exception, string message, string details)
        {
            this.Set(exception, message);
            this.AddDetails(details);
        }

        public void SetSuccess()
        {
            this.Set(E_ResultStatus.SUCCESS);
        }
        public void SetSuccess(string message)
        {
            this.SetSuccess();
            this._message = message;
        }
        public void SetSuccess(string message, string details)
        {
            this.SetSuccess();
            this._message = message;
            this.AddDetails(details);
        }
        public void SetSuccess(Object value)
        {
            SetSuccess();
            this._value = value;
        }
        public void SetSuccess(Object value, string message)
        {
            this.SetSuccess(value);
            this._message = message;
        }
        public void SetSuccess(Object value, string message, string details)
        {
            this.SetSuccess(value);
            this._message = message;
            this.AddDetails(details);
        }
        public void SetFail()
        {
            this.Set(E_ResultStatus.FAIL);
        }
        public void SetFail(string message)
        {
            this.SetFail();
            this._message = message ?? this._message;
        }
        public void SetFail(string message, string details)
        {
            this.SetFail(message);
            this.AddDetails(details);
        }

        public void AddMessage(string message)
        {
            if (message != null)
            {
                if (string.IsNullOrEmpty(this._message))
                    this._message = message;
                else
                    this._message += ". " + message;
            }
        }

        public void AddDetails(string details)
        {
            if (details != null)
            {
                if (string.IsNullOrEmpty(this._details))
                    this._details = details;
                else
                    this._details += "\r\n" + details;
                //this._details += "" + details;
            }
        }
        public void AddDetails(C_Result element)
        {
            if (element != null)
            {
                if (string.IsNullOrEmpty(this._details))
                    this._details = "\r\n" + element.Info();
                else
                    this._details += "\r\n" + element.Info();
                //this._details += "" + element.Info();
            }
        }
        public void AddDetails(C_Warning element)
        {
            if (element != null)
            {
                if (string.IsNullOrEmpty(this._details))
                    this._details = element.Info();
                else
                    this._details += element.Info();
                //this._details += "" + element.Info();
            }
        }
        public void AddDetails(C_Error element)
        {
            if (element != null)
            {
                if (string.IsNullOrEmpty(this._details))
                    this._details = element.Info();
                else
                    this._details += element.Info();
                //this._details += "" + element.Info();
            }
        }

        public void Add(E_ResultStatus status)
        {
            if (!this.Fail)
                this.Status = status != E_ResultStatus.ND ? status : this.Status;
        }
        public void Add(string message, E_ResultStatus status)
        {
            this.Add(status);
            this.AddMessage(message);
        }
        public void Add(string message, string details)
        {
            this.AddMessage(message);
            this.AddDetails(message);
        }
        public void Add(string message, string details, E_ResultStatus status)
        {
            this.Add(message, status);
            this.AddDetails(message);
        }
        public void Add(C_Result result)
        {
            this._type = result.Type;
            this.Add(result.Message, result.Details, result.Status);
        }
        public void Add(C_Error error)
        {
            this._type = E_ResultType.ERROR;
            this.Add(error.Message, error.Details, E_ResultStatus.FAIL);
            this.Value = error ?? this.Value;
        }
        public void Add(C_Exception exception)
        {
            this._type = E_ResultType.EXCEPTION;
            this.Add(exception.Message, exception.Details, E_ResultStatus.FAIL);
            this._exception = exception;
        }
        public void Add(Exception exception)
        {
            this.Add(new C_Exception(exception));
        }

        public string StatusToString()
        {
            switch (_status)
            {
                case E_ResultStatus.SUCCESS:
                    return "Success";
                case E_ResultStatus.FAIL:
                    return "Fail";
                default:
                    return "Undefined";
            }
        }
        public string TypeToString()
        {
            switch (_type)
            {
                case E_ResultType.EXCEPTION:
                    return "Exception";
                case E_ResultType.ERROR:
                    return "Error";
                case E_ResultType.WARNING:
                    return "Warning";
                case E_ResultType.ND:
                    return "Undefined";
                case E_ResultType.BOOLEAN:
                    return "Boolean";
                case E_ResultType.INT:
                    return "Integer";
                case E_ResultType.LONG:
                    return "Long";
                case E_ResultType.FLOAT:
                    return "Float";
                case E_ResultType.DOUBLE:
                    return "Double";
                case E_ResultType.STRING:
                    return "String";
                case E_ResultType.LIST:
                    return "List";
                case E_ResultType.OBJECT:
                    return "Object";
                default:
                    return "Undefined";
            }
        }

        public override string ToString()
        {
            string result = this.DateTime.ToString() + "> [" + this.Status.ToString() + "]";
            if (!string.IsNullOrEmpty(this.Title))
            {
                result += " " + this.Title;
                if (!string.IsNullOrEmpty(this.Message))
                    result += ": " + this.Message;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Message))
                    result += " " + this.Message;
            }
            if (this._exception != null)
            {
                result += "[Exception: " + this._exception.Message + "]";
            }
            result += ".";
            return result;
        }

        public string Info()
        {
            StringBuilder text = new StringBuilder();
            string dateTime = this.DateTime.ToString();
            text.AppendLine(dateTime + " > -- RESULT -----------------------------------------------------------------------");
            text.AppendLine(dateTime + " | Status: " + this.Status.ToString() + ".");
            text.AppendLine(dateTime + " | Type: '" + this.Type.ToString() + ".");
            text.AppendLine(dateTime + " | Title: '" + (this.Title ?? "") + ".");
            text.AppendLine(dateTime + " | Message: " + (this.Message ?? "") + ".");
            text.AppendLine(dateTime + " | Details: " + (this.Details ?? "") + ".");
            text.AppendLine(dateTime + " ^-------------------------------------------------------------------------------");
            if (this._exception != null)
            {
                text.AppendLine("Exception information: " + this.Exception.Info());
            }
            return text.ToString();
        }
        #endregion

        #region PROTECTED CONSTRUCTORS
        protected C_Result(string title, E_ResultType type, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, status);
        }
        protected C_Result(string title, E_ResultType type, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, message, status);
        }
        protected C_Result(string title, E_ResultType type, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, message, details, status);
        }
        protected C_Result(string title, E_ResultType type, C_Result result, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, result, status);
        }
        protected C_Result(string title, E_ResultType type, C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, result, message, status);
        }
        protected C_Result(string title, E_ResultType type, C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, result, message, details, status);
        }
        protected C_Result(string title, E_ResultType type, Object value, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, value, status);
        }
        protected C_Result(string title, E_ResultType type, Object value, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, value, message, status);
        }
        protected C_Result(string title, E_ResultType type, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(type, value, message, details, status);
        }
        protected C_Result(string title, Object value, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(value, status);
        }
        protected C_Result(string title, Object value, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(value, message, status);
        }
        protected C_Result(string title, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = title ?? "";
            this.Set(value, message, details, status);
        }

        protected C_Result(E_ResultType type, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, status);
        }
        protected C_Result(E_ResultType type, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, message, status);
        }
        protected C_Result(E_ResultType type, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, message, details, status);
        }
        protected C_Result(E_ResultType type, C_Result result, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, result, status);
        }
        protected C_Result(E_ResultType type, C_Result result, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, result, message, status);
        }
        protected C_Result(E_ResultType type, C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, result, message, details, status);
        }
        protected C_Result(E_ResultType type, Object value, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, value, status);
        }
        protected C_Result(E_ResultType type, Object value, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, value, message, status);
        }
        protected C_Result(E_ResultType type, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(type, value, message, details, status);
        }

        protected C_Result(C_Result result, Object value, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = result.Title ?? "";
            this.Set(result, value, status);
        }
        protected C_Result(C_Result result, Object value, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(result, value, message, status);
        }
        protected C_Result(C_Result result, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(result, value, message, details, status);
        }
        protected C_Result(Object value, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(value, status);
        }
        protected C_Result(Object value, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(value, message, status);
        }
        protected C_Result(Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._title = "";
            this.Set(value, message, details, status);
        }
        #endregion

        #region PROTECTED METHODS
        protected void Set(E_ResultType type, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this._type = type;
            this.Status = status;
        }
        protected void Set(E_ResultType type, string message, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, status);
            this._message = message;
        }
        protected void Set(E_ResultType type, string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, message, status);
            this.AddDetails(details);
        }
        protected void Set(E_ResultType type, C_Result result, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this._type = type;
            this.Status = status != E_ResultStatus.ND ? status : result.Status;
            this._message = result.Message;
            this.AddDetails(result.Details);
            this._value = result.Value;
            this._log_info = result.LogInfo;
        }
        protected void Set(E_ResultType type, C_Result result, string message, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, result, status);
            this._message = message;
        }
        protected void Set(E_ResultType type, C_Result result, string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, result, message, status);
            this.AddDetails(details);
        }

        protected void Set(E_ResultType type, Object value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, status);
            this._value = value;
        }
        protected void Set(E_ResultType type, Object value, string message, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, value, status);
            this._message = message;
        }
        protected void Set(E_ResultType type, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(type, value, message, status);
            this.AddDetails(details);
        }

        protected void Set(C_Result result, Object value, E_ResultStatus status = E_ResultStatus.ND)
        {
            this._type = E_ResultType.OBJECT;
            this.Status = status != E_ResultStatus.ND ? status : result.Status;
            this._message = result.Message;
            this.AddDetails(result.Details);
            this._value = value;
        }
        protected void Set(C_Result result, Object value, string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            this.Set(result, value, status);
            this._message = message ?? this._message;
        }
        protected void Set(C_Result result, Object value, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
        {
            this.Set(result, value, message, status);
            this.AddDetails(details);
        }
        protected void Set(Object value, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.OBJECT, value, status);
        }
        protected void Set(Object value, string message, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.OBJECT, value, message, status);
        }
        protected void Set(Object value, string message, string details, E_ResultStatus status = E_ResultStatus.SUCCESS)
        {
            this.Set(E_ResultType.OBJECT, value, message, details, status);
        }
        #endregion
    }
}
