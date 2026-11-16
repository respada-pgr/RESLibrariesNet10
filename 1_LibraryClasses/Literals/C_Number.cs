using System;
using _1_LibraryClasses.Result;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Mutable non-null numeric value stored as <see cref="decimal"/> (invariant parsing/formatting).
    /// For optional numbers use <see cref="C_NumberNullable"/>.
    /// Factories: <see cref="Create(decimal)"/> / <see cref="TryCreate(string)"/>.
    /// Parse/convert: <see cref="C_Hlp_Number"/>.
    /// <para><i>[ES] Valor numérico mutable no nulo en <see cref="decimal"/>.
    /// Para opcionales: <see cref="C_NumberNullable"/>.
    /// Factorías Create / TryCreate. Parseo/conversión en <see cref="C_Hlp_Number"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// C_Number n = C_Number.Create(12.5m);
    /// C_Number? parsed = C_Number.TryCreate("42");
    /// </code>
    /// </example>
    public class C_Number : I_Infoable
    {
#region PRIVATE FIELDS

        private decimal _value;

#endregion

#region PUBLIC PROPERTIES

        public decimal Value
        {
            get => this._value;
            set => this._value = value;
        }

        public bool IsInteger => this._value == decimal.Truncate(this._value);

        public bool IsZero => this._value == 0m;

        public bool IsPositive => this._value > 0m;

        public bool IsNegative => this._value < 0m;

#endregion

#region CONSTRUCTORS

        public C_Number()
        {
            this._value = 0m;
        }

        public C_Number(decimal value)
        {
            this._value = value;
        }

        public C_Number(int value)
        {
            this._value = value;
        }

        public C_Number(long value)
        {
            this._value = value;
        }

        public C_Number(double value)
        {
            this._value = (decimal)value;
        }

        /// <summary>
        /// Parses text (invariant). Throws if invalid — prefer <see cref="TryCreate(string)"/>.
        /// </summary>
        public C_Number(string text)
        {
            this._value = C_Hlp_String.ToDecimal(text);
        }

        public C_Number(C_Number? source)
        {
            this._value = source?._value ?? 0m;
        }

#endregion

#region PUBLIC METHODS

        // ----- static (factory) -----

        public static C_Number Create(decimal value) => new C_Number(value);

        public static C_Number Create(int value) => new C_Number(value);

        public static C_Number Create(long value) => new C_Number(value);

        public static C_Number Create(double value) => new C_Number(value);

        public static C_Number Create(string text) => new C_Number(text);

        public static C_Number Create(C_Number? source) => new C_Number(source);

        /// <summary>
        /// Tries to create from text; <see langword="null"/> if invalid or empty.
        /// <para><i>[ES] Intenta crear desde texto; <see langword="null"/> si no es válido o está vacío.</i></para>
        /// </summary>
        /// <summary>
        /// Tries to create from text; <see langword="null"/> if empty/white-space or not a number.
        /// <para><i>[ES] Intenta crear desde texto; <see langword="null"/> si está vacío o no es número.</i></para>
        /// </summary>
        public static C_Number? TryCreate(string? text) => C_Hlp_Number.TryCreate(text);

        // ----- instance -----

        public bool TryToInt(out int value)
        {
            C_Result<int> r = C_Hlp_Number.TryToInt(this._value);
            if (r.IsFail)
            {
                value = 0;
                return false;
            }
            value = r.Value;
            return true;
        }

        public bool TryToInt(out int value, out decimal discarded)
        {
            C_Result<int> r = C_Hlp_Number.TryToInt(this._value, out discarded);
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
            C_Result<long> r = C_Hlp_Number.TryToLong(this._value);
            if (r.IsFail)
            {
                value = 0;
                return false;
            }
            value = r.Value;
            return true;
        }

        public bool TryToLong(out long value, out decimal discarded)
        {
            C_Result<long> r = C_Hlp_Number.TryToLong(this._value, out discarded);
            if (r.IsFail)
            {
                value = 0;
                return false;
            }
            value = r.Value;
            return true;
        }

        public C_Result<int> TryToInt() => C_Hlp_Number.TryToInt(this._value);

        public C_Result<int> TryToInt(out decimal discarded)
            => C_Hlp_Number.TryToInt(this._value, out discarded);

        public C_Result<long> TryToLong() => C_Hlp_Number.TryToLong(this._value);

        public C_Result<long> TryToLong(out decimal discarded)
            => C_Hlp_Number.TryToLong(this._value, out discarded);

        public C_Result<double> TryToDouble() => C_Hlp_Number.TryToDouble(this._value);

        public C_Result<double> TryToDouble(out decimal discarded)
            => C_Hlp_Number.TryToDouble(this._value, out discarded);

        public C_Result<float> TryToFloat(out decimal discarded)
            => C_Hlp_Number.TryToFloat(this._value, out discarded);

        public int ToInt() => C_Hlp_Number.ToInt(this._value);

        public long ToLong() => C_Hlp_Number.ToLong(this._value);

        public double ToDouble() => C_Hlp_Number.ToDouble(this._value);

        public float ToFloat() => C_Hlp_Number.ToFloat(this._value);

        public C_Result<float> TryToFloat() => C_Hlp_Number.TryToFloat(this._value);

        public decimal ToDecimal() => this._value;

        public C_Number Clone() => new C_Number(this);

        public override string ToString() => this._value.ToString();

        public string Info()
            => $"C_Number Value={this}, IsInteger={this.IsInteger}, IsZero={this.IsZero}";

#endregion

#region OPERATORS

        public static implicit operator decimal(C_Number? n) => n?._value ?? 0m;

        public static implicit operator C_Number(decimal value) => new C_Number(value);

        public static implicit operator C_Number(int value) => new C_Number(value);

        public static implicit operator C_Number(long value) => new C_Number(value);

        public static explicit operator C_Number(double value) => new C_Number(value);

        public static explicit operator C_Number(string text) => new C_Number(text);

#endregion
    }
}
