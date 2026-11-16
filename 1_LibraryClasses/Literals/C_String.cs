using System;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Mutable instance wrapper around a single <see cref="string"/>.
    /// Transforms update <see cref="Value"/> in place and return <c>this</c>.
    /// Stateless operations remain on <see cref="C_Hlp_String"/>.
    /// <para><i>[ES] Contenedor mutable de un <see cref="string"/>.
    /// Transformaciones in-place; lo estático en <see cref="C_Hlp_String"/>.</i></para>
    /// </summary>
    public class C_String : I_Infoable
    {
#region PRIVATE FIELDS

        private string _value = string.Empty;

#endregion

#region PUBLIC PROPERTIES

        public string Value
        {
            get => this._value;
            set => this._value = value ?? string.Empty;
        }

        public int Length => this._value.Length;

        public bool IsNull => this._value is null;

        public bool IsNullOrEmpty => string.IsNullOrEmpty(this._value);

        public bool IsNullOrWhiteSpace => string.IsNullOrWhiteSpace(this._value);

        public bool IsWord => C_Hlp_String.IsWord(this._value).IsOK;

        public bool IsOnlyDigits => C_Hlp_String.IsOnlyDigits(this._value);

        public bool IsInteger => C_Hlp_String.IsInteger(this._value);

        public bool IsDecimal => C_Hlp_String.IsDecimal(this._value);

        public bool IsNumber => C_Hlp_String.IsNumber(this._value);

        public bool IsOnlyLetters => C_Hlp_String.IsOnlyLetters(this._value);

        public bool IsOnlyLettersAndNumbers => C_Hlp_String.IsOnlyLettersAndNumbers(this._value);

        public bool IsOnlyLettersAndNumbersAndUnderscore
            => C_Hlp_String.IsOnlyLettersAndNumbersAndUnderscore(this._value);

        public bool IsEmoji => C_Hlp_String.IsEmoji(this._value);

        public bool ContainsEmoji => C_Hlp_String.ContainsEmoji(this._value);

        public bool ContainsDigit => C_Hlp_String.ContainsOnlyDigits(this._value);

        public bool ContainsLetter => C_Hlp_String.ContainsLetter(this._value);

        public bool ContainsLetterOrDigit => C_Hlp_String.ContainsOnlyLetterOrDigit(this._value);

        public bool ContainsWhiteSpace => C_Hlp_String.ContainsWhiteSpace(this._value);

#endregion

#region CONSTRUCTORS

        public C_String()
        {
        }

        public C_String(string? value)
        {
            this._value = value ?? string.Empty;
        }

        public C_String(C_String? source)
        {
            this._value = source is null ? string.Empty : source._value;
        }

#endregion

#region PUBLIC METHODS — TryTo* / To*

        public bool TryToInt(out int value)
            => C_Hlp_String.TryToInt(this._value, out value);

        public bool TryToLong(out long value)
            => C_Hlp_String.TryToLong(this._value, out value);

        public bool TryToDouble(out double value)
            => C_Hlp_String.TryToDouble(this._value, out value);

        public bool TryToDecimal(out decimal value)
            => C_Hlp_String.TryToDecimal(this._value, out value);

        public int ToInt()
        {
            if (!this.TryToInt(out int value))
                throw new FormatException("Value is not an Int32.");
            return value;
        }

        public long ToLong()
        {
            if (!this.TryToLong(out long value))
                throw new FormatException("Value is not an Int64.");
            return value;
        }

        public double ToDouble()
        {
            if (!this.TryToDouble(out double value))
                throw new FormatException("Value is not a Double.");
            return value;
        }

        public decimal ToDecimal()
        {
            if (!this.TryToDecimal(out decimal value))
                throw new FormatException("Value is not a Decimal.");
            return value;
        }       

#endregion

#region PUBLIC METHODS — transform in-place

        public C_String NullToEmpty()
        {
            this._value = C_Hlp_String.NullToEmpty(this._value);
            return this;
        }

        public C_String CleanCrLf()
        {
            this._value = C_Hlp_String.CleanCrLf(this._value);
            return this;
        }

        public C_String CleanCrLfAndTabs()
        {
            this._value = C_Hlp_String.CleanCrLfAndTabs(this._value);
            return this;
        }

        public C_String FirstCharToUpper()
        {
            this._value = C_Hlp_String.FirstCharToUpper(this._value);
            return this;
        }

        public int CountChars(char toFind, int startIndex = 0)
            => C_Hlp_String.CountChars(this._value, toFind, startIndex);

        public int CountChars(char toFind, bool onlyFirst, int startIndex = 0)
            => C_Hlp_String.CountChars(this._value, toFind, onlyFirst, startIndex);

#endregion

#region PUBLIC METHODS

        public virtual C_String Clone()
            => new C_String(this);

        public override string ToString()
            => this._value;

        public string Info()
            => $"C_String Length={this.Length}, Value=\"{this._value}\", IsNull={this.IsNull}";

#endregion

#region OPERATORS

        public static implicit operator string(C_String s)
            => s?._value ?? string.Empty;

        public static implicit operator C_String(string? s)
            => new C_String(s);

#endregion
    }
}
