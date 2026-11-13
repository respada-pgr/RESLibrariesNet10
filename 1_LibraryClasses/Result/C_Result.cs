using _1_LibraryClasses.Event;
using System;
using System.Text;

namespace _1_LibraryClasses.Result
{
    /// <summary>
    /// Untyped operation result (status derived from type, message/details, optional exception).
    /// <para><i>[ES] Resultado de operación sin payload (estado derivado del tipo, mensaje/detalles, excepción opcional).</i></para>
    /// </summary>
    public class C_Result : I_Infoable, I_Result
    {
#region PRIVATE FIELDS

        private E_ResultType _type = E_ResultType.ND;
        private DateTime _createDate = DateTime.Now;
        private string _message = "";
        private string _details = "";
        private C_Exception? _exception = null;

#endregion

#region PUBLIC PROPERTIES

        /// <summary>
        /// Derived from <see cref="Type"/>: OK/WARNING → OK; FAIL/ERROR/EXCEPTION → FAIL; ND → ND.
        /// <para><i>[ES] Derivado de <see cref="Type"/>.</i></para>
        /// </summary>
        public E_ResultStatus Status => this._type switch
        {
            E_ResultType.OK or E_ResultType.WARNING => E_ResultStatus.OK,
            E_ResultType.ND => E_ResultStatus.ND,
            _ => E_ResultStatus.FAIL,
        };

        public E_ResultType Type
        {
            get => this._type;
            protected set => this._type = value;
        }

        /// <summary>
        /// Numeric severity of <see cref="Type"/> (same as cast to int). Higher = worse.
        /// <para><i>[ES] Severidad numérica de <see cref="Type"/>. Mayor = peor.</i></para>
        /// </summary>
        public int WarningLevel => (int)this._type;

        public DateTime CreateDate => this._createDate;

        public string Message
        {
            get => this._message ?? "";
            protected set => this._message = value ?? "";
        }

        public string Details
        {
            get => this._details ?? "";
            protected set => this._details = value ?? "";
        }

        public C_Exception? Exception => this._exception;

        public bool IsOK => this.Status == E_ResultStatus.OK;
        public bool IsFail => this.Status == E_ResultStatus.FAIL;
        public bool IsWarning => this._type == E_ResultType.WARNING;
        public bool IsError => this.WarningLevel >= (int)E_ResultType.ERROR;
        public bool HasException => this._exception is not null || this._type == E_ResultType.EXCEPTION;

#endregion

#region CONSTRUCTORS

        protected C_Result()
        {
        }

        protected C_Result(E_ResultType type)
        {
            this.Set(type);
        }

        protected C_Result(E_ResultStatus status)
        {
            this.Set(status);
        }

        public C_Result(string message, E_ResultType type = E_ResultType.FAIL)
        {
            this.Set(message, type);
        }

        public C_Result(string message, E_ResultStatus status = E_ResultStatus.FAIL)
        {
            this.Set(message, status);
        }

        public C_Result(string message, string details, E_ResultType type = E_ResultType.FAIL)
        {
            this.Set(message, details, type);
        }

        public C_Result(string message, string details, E_ResultStatus status = E_ResultStatus.FAIL)
        {
            this.Set(message, details, status);
        }

        public C_Result(C_Result result)
        {
            this.Set(result);
        }

        public C_Result(C_Result result, string message)
        {
            this.Set(result, message);
        }

        /// <summary>
        /// Clones this result. <see cref="CreateDate"/> is the clone creation time (not copied from source).
        /// <para><i>[ES] Clona el resultado. <see cref="CreateDate"/> es el del clon, no el del origen.</i></para>
        /// </summary>
        public virtual C_Result Clone()
            => new C_Result(this);

        public C_Result(C_Warning warning)
        {
            this.Set(warning);
        }

        public C_Result(C_Warning warning, string message)
        {
            this.Set(warning, message);
        }

        public C_Result(C_Error error)
        {
            this.Set(error);
        }

        public C_Result(C_Error error, string message)
        {
            this.Set(error, message);
        }

        public C_Result(C_Exception exception)
        {
            this.Set(exception);
        }

        public C_Result(C_Exception exception, string message)
        {
            this.Set(exception, message);
        }

        public C_Result(Exception exception)
        {
            this.Set(exception);
        }

        public C_Result(Exception exception, string message)
        {
            this.Set(exception, message);
        }

#endregion

#region PUBLIC METHODS

        public string StatusToString()
            => this.Status switch
            {
                E_ResultStatus.OK => "Ok",
                E_ResultStatus.FAIL => "Fail",
                _ => "Undefined",
            };

        public string TypeToString()
            => this._type switch
            {
                E_ResultType.EXCEPTION => "Exception",
                E_ResultType.ERROR => "Error",
                E_ResultType.FAIL => "Fail",
                E_ResultType.WARNING => "Warning",
                E_ResultType.OK => "Ok",
                _ => "Undefined",
            };

        public override string ToString()
            => this._type switch
            {
                E_ResultType.EXCEPTION => "Exception!!!",
                E_ResultType.ERROR => "Error!!",
                E_ResultType.FAIL => "Fail!",
                E_ResultType.WARNING => "Warning!",
                E_ResultType.OK => "Success!",
                _ => "<Undefined>",
            };

        public string ToInfoLine()
        {
            var text = new StringBuilder();
            text.AppendLine(this.CreateDate.ToString() + "> " + this._type.ToString()
                + (string.IsNullOrEmpty(this.Message) ? "." : ": " + this.Message + "."));
            if (this._exception != null)
                text.AppendLine("Exception: " + this._exception.Message + ".");
            return text.ToString();
        }

        public string Info()
        {
            var text = new StringBuilder();
            string dateTime = this.CreateDate.ToString();
            text.AppendLine(dateTime + " > -- RESULT -----------------------------------------------------------------------");
            text.AppendLine(dateTime + " | Status: " + this.Status + ".");
            text.AppendLine(dateTime + " | Type: " + this.Type + ".");
            text.AppendLine(dateTime + " | WarningLevel: " + this.WarningLevel + ".");
            text.AppendLine(dateTime + " | Message: " + this.Message + ".");
            if (!string.IsNullOrEmpty(this.Details))
                text.AppendLine(dateTime + " | Details: " + this.Details + ".");
            text.AppendLine(dateTime + " ^-------------------------------------------------------------------------------");
            if (this._exception != null)
                text.AppendLine("Exception information: " + this._exception.Info());
            return text.ToString();
        }

#endregion

#region PROTECTED METHODS

        protected void Set(E_ResultType type)
        {
            this._type = type;
        }

        protected void Set(E_ResultStatus status)
        {
            this._type = status switch
            {
                E_ResultStatus.OK => E_ResultType.OK,
                E_ResultStatus.FAIL => E_ResultType.FAIL,
                _ => E_ResultType.ND,
            };
        }

        protected void Set(string message, E_ResultType type = E_ResultType.FAIL)
        {
            this.Set(type);
            this._message = message ?? string.Empty;
        }

        protected void Set(string message, E_ResultStatus status = E_ResultStatus.FAIL)
        {
            this.Set(status);
            this._message = message ?? string.Empty;
        }

        protected void Set(string message, string details, E_ResultType type = E_ResultType.FAIL)
        {
            this.Set(message, type);
            this._details = details ?? string.Empty;
        }

        protected void Set(string message, string details, E_ResultStatus status = E_ResultStatus.FAIL)
        {
            this.Set(message, status);
            this._details = details ?? string.Empty;
        }

        protected void Set(I_Result result)
        {
            if (result is null)
                return;

            this._type = result.Type;
            this._message = result.Message ?? string.Empty;
            this._details = result.Details ?? string.Empty;

            if (result is C_Result concrete)
                this._exception = concrete.Exception;
            else if (!result.HasException)
                this._exception = null;
        }

        protected void Set(I_Result result, string message)
        {
            this.Set(result);
            this._message = message ?? string.Empty;
        }

        protected void Set(C_Result result)
        {
            if (result is null)
                return;

            this.Set((I_Result)result);
            this._exception = result.Exception;
        }

        protected void Set(C_Result result, string message)
        {
            this.Set(result);
            this._message = message ?? string.Empty;
        }

        protected void Set(C_Warning warning)
        {
            this._type = E_ResultType.WARNING;
            this._exception = null;
            this._message = warning?.Message ?? string.Empty;
            this._details = warning?.Details ?? string.Empty;
        }

        protected void Set(C_Warning warning, string message)
        {
            this.Set(warning);
            this._message = message ?? string.Empty;
        }

        protected void Set(C_Error error)
        {
            this._type = E_ResultType.ERROR;
            this._exception = null;
            this._message = error?.Message ?? string.Empty;
            this._details = error?.Details ?? string.Empty;
        }

        protected void Set(C_Error error, string message)
        {
            this.Set(error);
            this._message = message ?? string.Empty;
        }

        protected void Set(C_Exception exception)
        {
            this._type = E_ResultType.EXCEPTION;
            this._message = exception?.Message ?? string.Empty;
            this._details = exception?.Details ?? string.Empty;
            this._exception = exception;
        }

        protected void Set(C_Exception exception, string message)
        {
            this.Set(exception);
            this._message = message ?? string.Empty;
        }

        protected void Set(Exception exception)
            => this.Set(new C_Exception(exception));

        protected void Set(Exception exception, string message)
            => this.Set(new C_Exception(message, exception));

        protected void SetOk()
            => this.Set(E_ResultType.OK);

        protected void SetOk(string message)
            => this.Set(message, E_ResultType.OK);

        protected void SetOk(string message, string details)
            => this.Set(message, details, E_ResultType.OK);

        protected void SetOk(C_Warning warning)
            => this.Set(warning);

        protected void SetOk(C_Warning warning, string message)
            => this.Set(warning, message);

        protected void SetFail()
            => this.Set(E_ResultType.FAIL);

        protected void SetFail(string message)
            => this.Set(message, E_ResultType.FAIL);

        protected void SetFail(string message, string details)
            => this.Set(message, details, E_ResultType.FAIL);

        protected void SetFail(C_Error error)
            => this.Set(error);

        protected void SetFail(C_Error error, string message)
            => this.Set(error, message);

        protected void SetFail(C_Exception exception)
            => this.Set(exception);

        protected void SetFail(C_Exception exception, string message)
            => this.Set(exception, message);

        protected void SetFail(Exception exception)
            => this.Set(exception);

        protected void SetFail(Exception exception, string message)
            => this.Set(exception, message);

#endregion

#region PUBLIC STATIC METHODS — factories

        public static C_Result Ok()
            => new C_Result(E_ResultType.OK);

        public static C_Result Ok(string message)
            => new C_Result(message, E_ResultType.OK);

        public static C_Result Ok(string message, string details)
            => new C_Result(message, details, E_ResultType.OK);

        public static C_Result Ok(C_Warning warning)
            => new C_Result(warning);

        public static C_Result Ok(C_Warning warning, string message)
            => new C_Result(warning, message);

        public static C_Result Fail()
            => new C_Result(E_ResultType.FAIL);

        public static C_Result Fail(string message)
            => new C_Result(message, E_ResultType.FAIL);

        public static C_Result Fail(string message, string details)
            => new C_Result(message, details, E_ResultType.ERROR);

        public static C_Result Fail(C_Error error)
            => new C_Result(error);

        public static C_Result Fail(C_Error error, string message)
            => new C_Result(error, message);

        public static C_Result Fail(C_Exception exception)
            => new C_Result(exception);

        public static C_Result Fail(C_Exception exception, string message)
            => new C_Result(exception, message);

        public static C_Result Fail(Exception exception)
            => new C_Result(exception);

        public static C_Result Fail(Exception exception, string message)
            => new C_Result(exception, message);

        public static C_Result<TValue> Ok<TValue>(TValue value)
            => C_Result<TValue>.Ok(value);

        public static C_Result<TValue> Ok<TValue>(TValue value, string message)
            => C_Result<TValue>.Ok(value, message);

        public static C_Result<TValue> Fail<TValue>()
            => C_Result<TValue>.Fail();

        public static C_Result<TValue> Fail<TValue>(string message)
            => C_Result<TValue>.Fail(message);

#endregion
    }

    /// <summary>
    /// Typed operation result: <see cref="C_Result"/> plus payload <see cref="Value"/>.
    /// <para><i>[ES] Resultado tipado: <see cref="C_Result"/> más payload <see cref="Value"/>.</i></para>
    /// </summary>
    public class C_Result<T> : C_Result, I_Result<T>
    {
#region PRIVATE FIELDS

        private T _value = default!;

#endregion

#region PUBLIC PROPERTIES

        public T Value
        {
            get => this._value;
            protected set => this._value = value;
        }

#endregion

#region CONSTRUCTORS

        protected C_Result()
            : base()
        {
        }

        protected C_Result(E_ResultType type)
            : base(type)
        {
        }

        protected C_Result(E_ResultStatus status)
            : base(status)
        {
        }

        public C_Result(T value, E_ResultType type = E_ResultType.OK)
            : base(type)
        {
            this._value = value;
        }

        public C_Result(T value, E_ResultStatus status = E_ResultStatus.OK)
            : base(status)
        {
            this._value = value;
        }

        public C_Result(T value, string message, E_ResultType type = E_ResultType.OK)
            : base(message, type)
        {
            this._value = value;
        }

        public C_Result(T value, string message, E_ResultStatus status = E_ResultStatus.OK)
            : base(message, status)
        {
            this._value = value;
        }

        public C_Result(T value, string message, string details, E_ResultType type = E_ResultType.OK)
            : base(message, details, type)
        {
            this._value = value;
        }

        public C_Result(T value, string message, string details, E_ResultStatus status = E_ResultStatus.OK)
            : base(message, details, status)
        {
            this._value = value;
        }

        public C_Result(string message, E_ResultType type = E_ResultType.FAIL)
            : base(message, type)
        {
        }

        public C_Result(string message, E_ResultStatus status = E_ResultStatus.FAIL)
            : base(message, status)
        {
        }

        public C_Result(string message, string details, E_ResultType type = E_ResultType.FAIL)
            : base(message, details, type)
        {
        }

        public C_Result(string message, string details, E_ResultStatus status = E_ResultStatus.FAIL)
            : base(message, details, status)
        {
        }

        public C_Result(C_Result<T> result)
            : base(result)
        {
            if (result is not null)
                this._value = result.Value;
        }

        public C_Result(C_Result<T> result, string message)
            : base(result, message)
        {
            if (result is not null)
                this._value = result.Value;
        }

        public new C_Result<T> Clone()
            => new C_Result<T>(this);

        public C_Result(C_Warning warning)
            : base(warning)
        {
        }

        public C_Result(C_Warning warning, string message)
            : base(warning, message)
        {
        }

        public C_Result(C_Error error)
            : base(error)
        {
        }

        public C_Result(C_Error error, string message)
            : base(error, message)
        {
        }

        public C_Result(C_Exception exception)
            : base(exception)
        {
        }

        public C_Result(C_Exception exception, string message)
            : base(exception, message)
        {
        }

        public C_Result(Exception exception)
            : base(exception)
        {
        }

        public C_Result(Exception exception, string message)
            : base(exception, message)
        {
        }

#endregion

#region PROTECTED METHODS

        protected void Set(T value, E_ResultType type = E_ResultType.OK)
        {
            this._value = value;
            this.Set(type);
        }

        protected void Set(T value, E_ResultStatus status = E_ResultStatus.OK)
        {
            this._value = value;
            this.Set(status);
        }

        protected void Set(T value, string message, E_ResultType type = E_ResultType.OK)
        {
            this._value = value;
            this.Set(message, type);
        }

        protected void Set(T value, string message, E_ResultStatus status = E_ResultStatus.OK)
        {
            this._value = value;
            this.Set(message, status);
        }

        protected void Set(T value, string message, string details, E_ResultType type = E_ResultType.OK)
        {
            this._value = value;
            this.Set(message, details, type);
        }

        protected void Set(T value, string message, string details, E_ResultStatus status = E_ResultStatus.OK)
        {
            this._value = value;
            this.Set(message, details, status);
        }

        protected void Set(I_Result<T> result)
        {
            if (result is null)
                return;

            this.Set((I_Result)result);
            this._value = result.Value;
        }

        protected void Set(I_Result<T> result, string message)
        {
            this.Set(result);
            this.Message = message ?? string.Empty;
        }

        protected void Set(C_Result<T> result)
        {
            if (result is null)
                return;

            this.Set((I_Result<T>)result);
        }

        protected void SetOk(T value)
            => this.Set(value, E_ResultType.OK);

        protected void SetOk(T value, string message)
            => this.Set(value, message, E_ResultType.OK);

        protected void SetOk(T value, string message, string details)
            => this.Set(value, message, details, E_ResultType.OK);

#endregion

#region PUBLIC STATIC METHODS — factories

        public static new C_Result<T> Ok(T value)
            => new C_Result<T>(value, E_ResultType.OK);

        public static new C_Result<T> Ok(T value, string message)
            => new C_Result<T>(value, message, E_ResultType.OK);

        public static new C_Result<T> Ok(T value, string message, string details)
            => new C_Result<T>(value, message, details, E_ResultType.OK);

        public static new C_Result<T> Ok(C_Warning warning)
            => new C_Result<T>(warning);

        public static new C_Result<T> Ok(C_Warning warning, string message)
            => new C_Result<T>(warning, message);

        public static new C_Result<T> Fail()
            => new C_Result<T>(E_ResultType.FAIL);

        public static new C_Result<T> Fail(string message)
            => new C_Result<T>(message, E_ResultType.FAIL);

        public static new C_Result<T> Fail(string message, string details)
            => new C_Result<T>(message, details, E_ResultType.ERROR);

        public static new C_Result<T> Fail(C_Error error)
            => new C_Result<T>(error);

        public static new C_Result<T> Fail(C_Error error, string message)
            => new C_Result<T>(error, message);

        public static new C_Result<T> Fail(C_Exception exception)
            => new C_Result<T>(exception);

        public static new C_Result<T> Fail(C_Exception exception, string message)
            => new C_Result<T>(exception, message);

        public static new C_Result<T> Fail(Exception exception)
            => new C_Result<T>(exception);

        public static new C_Result<T> Fail(Exception exception, string message)
            => new C_Result<T>(exception, message);

#endregion
    }
}
