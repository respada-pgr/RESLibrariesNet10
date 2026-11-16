using System;
using _1_LibraryClasses.Result;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Mutable optional numeric value stored as <see cref="Nullable{T}"/> of <see cref="decimal"/>.
    /// Use <see cref="C_Number"/> when a value is always required.
    /// Factories: <see cref="Create(decimal?)"/> / <see cref="CreateNull"/> / <see cref="TryCreate(string)"/>.
    /// Logic: <see cref="C_Hlp_NumberNullable"/> (parse/format: <see cref="C_Hlp_Number"/>).
    /// <para><i>[ES] Valor numérico opcional (<see cref="decimal"/> anulable).
    /// Use <see cref="C_Number"/> cuando el valor es obligatorio.
    /// Factorías Create / CreateNull / TryCreate. Lógica en <see cref="C_Hlp_NumberNullable"/> (parseo/formato: <see cref="C_Hlp_Number"/>).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// C_NumberNullable n = C_NumberNullable.CreateNull();
    /// C_NumberNullable x = C_NumberNullable.Create(12.5m);
    /// C_NumberNullable? bad = C_NumberNullable.TryCreate("abc"); // null method result
    /// </code>
    /// </example>
    public class C_NumberNullable : I_Infoable
    {
#region PRIVATE FIELDS

        private decimal? _value;

#endregion

#region PUBLIC PROPERTIES

        public decimal? Value
        {
            get => this._value;
            set => this._value = value;
        }

        public bool IsNull => this._value is null;

        public bool HasValue => this._value is not null;

        public bool IsInteger
            => this._value is decimal v && v == decimal.Truncate(v);

        public bool IsZero
            => this._value is decimal v && v == 0m;

        public bool IsPositive
            => this._value is decimal v && v > 0m;

        public bool IsNegative
            => this._value is decimal v && v < 0m;

#endregion

#region CONSTRUCTORS

        public C_NumberNullable()
        {
            this._value = null;
        }

        public C_NumberNullable(decimal value)
        {
            this._value = value;
        }

        public C_NumberNullable(decimal? value)
        {
            this._value = value;
        }

        public C_NumberNullable(int value)
        {
            this._value = value;
        }

        public C_NumberNullable(int? value)
        {
            this._value = value;
        }

        public C_NumberNullable(long value)
        {
            this._value = value;
        }

        public C_NumberNullable(long? value)
        {
            this._value = value;
        }

        public C_NumberNullable(double value)
        {
            this._value = (decimal)value;
        }

        public C_NumberNullable(double? value)
        {
            this._value = value is null ? null : (decimal)value.Value;
        }

        /// <summary>
        /// Parses text. Empty/white-space → null value. Invalid non-empty throws — prefer <see cref="TryCreate(string)"/>.
        /// </summary>
        public C_NumberNullable(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                this._value = null;
                return;
            }
            this._value = C_Hlp_Number.ToDecimal(text);
        }

        public C_NumberNullable(C_Number? source)
        {
            this._value = source?.Value;
        }

        public C_NumberNullable(C_NumberNullable? source)
        {
            this._value = source?._value;
        }

#endregion

#region PUBLIC METHODS

        // ----- static (factory) -----

        public static C_NumberNullable Create(decimal value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(decimal? value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(int value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(int? value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(long value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(long? value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(double value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(double? value) => new C_NumberNullable(value);

        public static C_NumberNullable Create(string? text) => new C_NumberNullable(text);

        public static C_NumberNullable Create(C_Number? source) => new C_NumberNullable(source);

        public static C_NumberNullable Create(C_NumberNullable? source) => new C_NumberNullable(source);

        public static C_NumberNullable CreateNull() => new C_NumberNullable((decimal?)null);

        /// <summary>
        /// Null/empty text → instance with <see cref="IsNull"/>.
        /// Invalid non-empty text → method returns <see langword="null"/>.
        /// </summary>
        /// <summary>
        /// Tries to create from text; <see langword="null"/> if empty/white-space or not a number.
        /// For an explicit null value use <see cref="CreateNull"/>.
        /// <para><i>[ES] Intenta crear desde texto; <see langword="null"/> si está vacío o no es número.
        /// Para un valor null explícito use <see cref="CreateNull"/>.</i></para>
        /// </summary>
        public static C_NumberNullable? TryCreate(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            if (!C_Hlp_Number.TryToDecimal(text, out decimal value))
                return null;
            return new C_NumberNullable(value);
        }

        // ----- instance -----

        public bool TryToInt(out int value)
        {
            C_Result<int> r = C_Hlp_NumberNullable.TryToInt(this._value);
            if (r.IsFail)
            {
                value = 0;
                return false;
            }
            value = r.Value;
            return true;
        }

        public bool TryToLong(out long value)
        {
            C_Result<long> r = C_Hlp_NumberNullable.TryToLong(this._value);
            if (r.IsFail)
            {
                value = 0;
                return false;
            }
            value = r.Value;
            return true;
        }

        public C_Result<int> TryToInt() => C_Hlp_NumberNullable.TryToInt(this._value);

        public C_Result<int> TryToInt(out decimal discarded)
            => C_Hlp_NumberNullable.TryToInt(this._value, out discarded);

        public C_Result<long> TryToLong() => C_Hlp_NumberNullable.TryToLong(this._value);

        public C_Result<long> TryToLong(out decimal discarded)
            => C_Hlp_NumberNullable.TryToLong(this._value, out discarded);

        public C_Result<double> TryToDouble() => C_Hlp_NumberNullable.TryToDouble(this._value);

        public C_Result<double> TryToDouble(out decimal discarded)
            => C_Hlp_NumberNullable.TryToDouble(this._value, out discarded);

        /// <summary>
        /// Returns a non-null <see cref="C_Number"/> when <see cref="HasValue"/>; otherwise <see langword="null"/>.
        /// </summary>
        public C_Number? ToNumberOrNull()
            => this._value is decimal v ? C_Number.Create(v) : null;

        public C_Number ToNumberOrDefault(decimal defaultValue = 0m)
            => C_Number.Create(this._value ?? defaultValue);

        public int ToInt()
        {
            if (this._value is null)
                throw new InvalidOperationException("Value is null.");
            return C_Hlp_Number.ToInt(this._value.Value);
        }

        public long ToLong()
        {
            if (this._value is null)
                throw new InvalidOperationException("Value is null.");
            return C_Hlp_Number.ToLong(this._value.Value);
        }

        public double ToDouble()
        {
            if (this._value is null)
                throw new InvalidOperationException("Value is null.");
            return C_Hlp_Number.ToDouble(this._value.Value);
        }

        public float ToFloat()
        {
            if (this._value is null)
                throw new InvalidOperationException("Value is null.");
            return C_Hlp_Number.ToFloat(this._value.Value);
        }

        public C_Result<float> TryToFloat() => C_Hlp_NumberNullable.TryToFloat(this._value);

        public C_Result<float> TryToFloat(out decimal discarded)
            => C_Hlp_NumberNullable.TryToFloat(this._value, out discarded);

        public decimal ToDecimal()
        {
            if (this._value is null)
                throw new InvalidOperationException("Value is null.");
            return this._value.Value;
        }


        /// <summary>
        /// Returns <see cref="int"/>? without throwing (null if <see cref="IsNull"/> or not convertible).
        /// <para><i>[ES] Devuelve <see cref="int"/>? sin lanzar (null si IsNull o no convertible).</i></para>
        /// </summary>
        public int? ToIntOrNull() => C_Hlp_NumberNullable.ToIntOrNull(this._value);

        public long? ToLongOrNull() => C_Hlp_NumberNullable.ToLongOrNull(this._value);

        public double? ToDoubleOrNull() => C_Hlp_NumberNullable.ToDoubleOrNull(this._value);

        /// <summary>
        /// Same as <see cref="Value"/>. / Igual que <see cref="Value"/>.
        /// </summary>
        public decimal? ToDecimalOrNull() => this._value;


        public C_String ToCString()
            => new C_String(this._value is null ? string.Empty : C_Hlp_Number.FromDecimal(this._value.Value));

        public C_Text ToText()
            => new C_Text(this._value is null ? string.Empty : C_Hlp_Number.FromDecimal(this._value.Value));

        public C_NumberNullable Clone() => new C_NumberNullable(this);

        public override string ToString()
            => this._value is null ? string.Empty : C_Hlp_Number.FromDecimal(this._value.Value);

        public string Info()
            => this.IsNull
                ? "C_NumberNullable IsNull=true"
                : $"C_NumberNullable Value={this}, IsInteger={this.IsInteger}, IsZero={this.IsZero}";

#endregion

#region OPERATORS

        public static implicit operator decimal?(C_NumberNullable? n) => n?._value;

        /// <summary>
        /// Collapses null to <c>0m</c>. Prefer <see cref="Value"/> to preserve null.
        /// </summary>
        public static implicit operator decimal(C_NumberNullable? n) => n?._value ?? 0m;

        public static implicit operator C_NumberNullable(decimal value) => new C_NumberNullable(value);

        public static implicit operator C_NumberNullable(decimal? value) => new C_NumberNullable(value);

        public static implicit operator C_NumberNullable(int value) => new C_NumberNullable(value);

        public static implicit operator C_NumberNullable(int? value) => new C_NumberNullable(value);

        public static implicit operator C_NumberNullable(long value) => new C_NumberNullable(value);

        public static implicit operator C_NumberNullable(long? value) => new C_NumberNullable(value);

        public static explicit operator C_NumberNullable(double value) => new C_NumberNullable(value);

        public static explicit operator C_NumberNullable(double? value) => new C_NumberNullable(value);

        public static explicit operator C_NumberNullable(string? text) => new C_NumberNullable(text);

        public static explicit operator int?(C_NumberNullable? n)
            => n is null ? null : C_Hlp_NumberNullable.ToIntOrNull(n._value);

        public static explicit operator long?(C_NumberNullable? n)
            => n is null ? null : C_Hlp_NumberNullable.ToLongOrNull(n._value);

        public static explicit operator double?(C_NumberNullable? n)
            => n is null ? null : C_Hlp_NumberNullable.ToDoubleOrNull(n._value);

#endregion
    }
}
