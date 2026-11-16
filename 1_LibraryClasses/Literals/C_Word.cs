using System;
using static System.Net.Mime.MediaTypeNames;

namespace _1_LibraryClasses
{
    /// <summary>
    /// A single word: non-empty text with no whitespace inside.
    /// Does not inherit <see cref="C_String"/>; holds a private <see cref="C_String"/>.
    /// Factories: <see cref="Create(string)"/> / <see cref="TryCreate(string)"/>.
    /// Logic: <see cref="C_Hlp_Word"/>.
    /// <para><i>[ES] Una sola palabra. Factorías Create / TryCreate. Lógica en <see cref="C_Hlp_Word"/>.</i></para>
    /// </summary>
    public class C_Word : I_Infoable
    {
#region PRIVATE FIELDS

        private readonly C_String _value = new C_String();

#endregion

#region PUBLIC PROPERTIES

        public string Value {
            get => this._value.Value; 
            set => this._value.Value = value;
        }

        public int Length => this._value.Length;
        public bool IsNullOrEmpty => this._value.IsNullOrEmpty;
        public bool IsOnlyDigits => this._value.IsOnlyDigits;
        public bool IsInteger => this._value.IsInteger;
        public bool IsDecimal => this._value.IsDecimal;
        public bool IsNumber => this._value.IsNumber;
        public bool IsOnlyLetters => this._value.IsOnlyLetters;
        public bool IsOnlyLettersAndNumbers => this._value.IsOnlyLettersAndNumbers;
        public bool IsOnlyLettersAndNumbersAndUnderscore
            => this._value.IsOnlyLettersAndNumbersAndUnderscore;
        public bool IsEmoji => this._value.IsEmoji;
        public bool ContainsEmoji => this._value.ContainsEmoji;
        public bool ContainsDigit => this._value.ContainsDigit;
        public bool ContainsLetter => this._value.ContainsLetter;
        public bool ContainsLetterOrDigit => this._value.ContainsLetterOrDigit;
        public bool ContainsWhiteSpace => this._value.ContainsWhiteSpace;

#endregion

#region CONSTRUCTORS

        public C_Word()
        {
        }

        /// <summary>
        /// Creates a word. Throws if invalid — prefer <see cref="TryCreate(string)"/>.
        /// </summary>
        public C_Word(string? value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value must be a single non-empty word.", nameof(value));

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i]))
                    throw new ArgumentException("Value must be a single non-empty word without whitespace.", nameof(value));
            }
            this._value.Value = value;
        }

        public C_Word(C_String? source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(source.Value))
                throw new ArgumentException("Value must be a single non-empty word.", nameof(source.Value));

            for (int i = 0; i < source.Value.Length; i++)
            {
                if (char.IsWhiteSpace(source.Value[i]))
                    throw new ArgumentException("Value must be a single non-empty word without whitespace.", nameof(source.Value));
            }
            this._value.Value = source.Value;
        }

        public C_Word(C_Word? source)
        {
            if(source is null)
                throw new ArgumentNullException(nameof(source));

            this.Value = source.Value;
        }

        #endregion

        #region PUBLIC METHODS — factory
        public static C_Word Create(string? text) => new C_Word(text);

        public static C_Word? TryCreate(string? text)
        {
            try
            {
                return new C_Word(text);
            }
            catch
            {
                return null;
            }
        }

        public static C_Word Create(C_Word? source)
        {
            return new C_Word(source);
        }

#endregion

#region PUBLIC METHODS — TryTo* / To*

        public bool TryToInt(out int value) => int.TryParse(this.Value, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out value);
        public bool TryToLong(out long value) => long.TryParse(this.Value, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out value);
        public bool TryToDouble(out double value) => double.TryParse(this.Value, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands,
                System.Globalization.CultureInfo.InvariantCulture, out value);
        public bool TryToDecimal(out decimal value) => decimal.TryParse(this.Value, System.Globalization.NumberStyles.Currency,
                System.Globalization.CultureInfo.InvariantCulture, out value);

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

#region PUBLIC METHODS

        public C_Word Clone() => new C_Word(this);
        public override string ToString() => this._value.Value;
        public string Info() => this._value.Info();

        #endregion

        #region OPERATORS

        public static implicit operator string(C_Word w) => w is null ? string.Empty : w.Value;
        public static explicit operator C_Word(string s) => C_Word.Create(s);

#endregion
    }
}
