using System;
using _1_LibraryClasses.Event;

namespace _1_LibraryClasses.Result
{
    /// <summary>
    /// Rich operation report: extends <see cref="C_Result"/> with <see cref="Title"/> and <see cref="Details"/>.
    /// <para><i>[ES] Informe rico: amplía <see cref="C_Result"/> con <see cref="Title"/> y <see cref="Details"/>.</i></para>
    /// </summary>
    public class C_ResultReport : C_Result
    {
#region PRIVATE FIELDS

        private string _title = "";
        private string _details = null;
        private object? _logInfo = null;

#endregion

#region PUBLIC PROPERTIES

        /// <summary>Report title. / Título del informe.</summary>
        public string Title
        {
            get => _title;
            protected set => _title = value ?? "";
        }

        /// <summary>Extended details. / Detalles ampliados.</summary>
        public string Details
        {
            get => _details;
            protected set => _details = value;
        }

        /// <summary>
        /// Optional log payload (e.g. <c>C_LoggedLine</c>). Not part of core Result.
        /// <para><i>[ES] Carga de log opcional (p. ej. C_LoggedLine). No forma parte del Result núcleo.</i></para>
        /// </summary>
        public object? LogInfo
        {
            get => this._logInfo;
            protected set => this._logInfo = value;
        }

#endregion

#region CONSTRUCTORS

        public C_ResultReport() : base() { }

        public C_ResultReport(E_ResultStatus status) : base(status) { }

        public C_ResultReport(string title, E_ResultStatus status = E_ResultStatus.ND)
            : base(status)
        {
            Title = title;
        }

        public C_ResultReport(string title, string message, E_ResultStatus status = E_ResultStatus.ND)
            : base(status)
        {
            Title = title;
            Message = message;
        }

        public C_ResultReport(string title, string message, string details, E_ResultStatus status = E_ResultStatus.ND)
            : base(status)
        {
            Title = title;
            Message = message;
            Details = details;
        }

        public C_ResultReport(C_Result result, E_ResultStatus status = E_ResultStatus.ND)
            : base(result, status)
        {
            if (result is C_ResultReport report)
            {
                Title = report.Title;
                Details = report.Details;
            }
        }

        /// <summary>
        /// Copy constructor (Title/Details + base result state). CreateDate is not copied from source (new instance time).
        /// <para><i>[ES] Constructor de copia. CreateDate no se reutiliza del origen.</i></para>
        /// </summary>
        public C_ResultReport(C_ResultReport? source)
            : base(source)
        {
            if (source is null)
                return;
            this.Title = source.Title;
            this.Details = source.Details;
            this.LogInfo = source.LogInfo;
        }

        public C_ResultReport(C_Error error) : base(error) { }

        public C_ResultReport(C_Error error, string message) : base(error, message) { }

        public C_ResultReport(Exception exception) : base(exception) { }

        public C_ResultReport(Exception exception, string message) : base(exception, message) { }

#endregion

#region PUBLIC METHODS

        /// <summary>
        /// Clones this report; <see cref="C_Result.CreateDate"/> is new on the clone.
        /// <para><i>[ES] Clona el informe; <see cref="C_Result.CreateDate"/> es nuevo en el clon.</i></para>
        /// </summary>
        public C_ResultReport Clone()
            => new C_ResultReport(this);

#endregion

#region PROTECTED METHODS

        protected override void AppendMessage(string text)
        {
            if (text is null)
                return;
            if (string.IsNullOrEmpty(_details))
                _details = text;
            else
                _details += "\r\n" + text;
        }

#endregion

#region PUBLIC METHODS — factories

        public static new C_ResultReport Ok(string message = null)
        {
            var r = new C_ResultReport(E_ResultStatus.OK);
            r.Type = E_ResultType.OK;
            if (!string.IsNullOrEmpty(message))
                r.Message = message;
            return r;
        }

        public static C_ResultReport Ok(string title, string message, string details = null)
        {
            var r = new C_ResultReport(title, message, details, E_ResultStatus.OK);
            r.Type = E_ResultType.OK;
            return r;
        }

        /// <summary>
        /// Failed report with title and message.
        /// <para><i>[ES] Informe de fallo con título y mensaje.</i></para>
        /// </summary>
        public static new C_ResultReport Fail(string title, string message = null)
        {
            var r = new C_ResultReport(title ?? "", message, E_ResultStatus.FAIL);
            r.Type = E_ResultType.ERROR;
            return r;
        }

        public static C_ResultReport Fail(string title, string message, string details)
        {
            var r = new C_ResultReport(title ?? "", message, details, E_ResultStatus.FAIL);
            r.Type = E_ResultType.ERROR;
            return r;
        }

        public static C_ResultReport FromResult(C_Result result)
            => result is C_ResultReport report ? report : new C_ResultReport(result);

#endregion

#region PUBLIC METHODS — info

        public override string ToString()
        {
            string result = base.ToString();
            if (!string.IsNullOrEmpty(Title))
                result += " " + Title;
            if (!string.IsNullOrEmpty(Details))
                result += " | " + Details;
            return result;
        }

#endregion
    }

    /// <summary>
    /// Rich report with typed value. / Informe rico con valor tipado.
    /// </summary>
    public class C_ResultReport<T> : C_ResultReport
    {
        public new T Value
        {
            get => base.Value is T v ? v : default!;
            protected set
            {
                base.Value = value;
                Type = E_ResultType.OK;
            }
        }

        public C_ResultReport() : base() { }

        public C_ResultReport(E_ResultStatus status) : base(status) { }

        public C_ResultReport(string title, E_ResultStatus status = E_ResultStatus.ND)
            : base(title, status) { }

        public C_ResultReport(string title, string message, E_ResultStatus status = E_ResultStatus.ND)
            : base(title, message, status) { }

        public C_ResultReport(T value, E_ResultStatus status = E_ResultStatus.OK)
            : base(status)
        {
            Value = value;
        }

        public C_ResultReport(string title, T value, string message = null, E_ResultStatus status = E_ResultStatus.OK)
            : base(title, message, status)
        {
            Value = value;
        }

        public C_ResultReport(C_Error error) : base(error) { }

        public C_ResultReport(Exception exception) : base(exception) { }

        public C_ResultReport(C_ResultReport<T>? source)
            : base(source)
        {
            if (source is not null)
                this.Value = source.Value;
        }

        public new C_ResultReport<T> Clone()
            => new C_ResultReport<T>(this);

        public static C_ResultReport<T> Ok(T value, string message = null)
        {
            var r = new C_ResultReport<T>(value, E_ResultStatus.OK);
            if (!string.IsNullOrEmpty(message))
                r.Message = message;
            return r;
        }

        public static C_ResultReport<T> Ok(string title, T value, string message = null)
            => new C_ResultReport<T>(title, value, message, E_ResultStatus.OK);

        public static new C_ResultReport<T> Fail(string title = null, string message = null)
        {
            var r = new C_ResultReport<T>(title ?? typeof(T).Name, message, E_ResultStatus.FAIL);
            r.Type = E_ResultType.ERROR;
            return r;
        }

        public static C_ResultReport<T> Fail(string title, string message, string details)
        {
            var r = new C_ResultReport<T>(title ?? typeof(T).Name, message, E_ResultStatus.FAIL);
            r.Type = E_ResultType.ERROR;
            r.Details = details;
            return r;
        }
    }
}
