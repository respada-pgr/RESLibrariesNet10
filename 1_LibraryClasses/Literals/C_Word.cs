using System;

namespace _1_LibraryClasses
{
    /// <summary>
    /// A single word: non-empty text with no whitespace inside.
    /// Does not inherit <see cref="C_String"/>; holds a private <see cref="C_String"/> value.
    /// <para><i>[ES] Una sola palabra: texto no vacío sin espacios.
    /// No hereda de <see cref="C_String"/>; encapsula un <see cref="C_String"/> interno.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var w = new C_Word("Hello");
    /// bool ok = C_Word.TryCreate("a b", out C_Word? bad); // false
    /// C_String s = w.AsString;
    /// </code>
    /// </example>
    public class C_Word : I_Infoable
    {
#region PRIVATE FIELDS

        private readonly C_String _value = new C_String();

#endregion

#region CONSTRUCTORS

        public C_Word()
        {
        }

        /// <summary>
        /// Creates a word. Throws if <paramref name="value"/> is not a word.
        /// <para><i>[ES] Crea una palabra. Lanza si no es una palabra válida.</i></para>
        /// </summary>
        public C_Word(string value)
        {
            this.SetWord(value);
        }

        /// <summary>
        /// Creates a word from a <see cref="C_String"/>. Throws if not a word.
        /// <para><i>[ES] Crea una palabra desde <see cref="C_String"/>. Lanza si no es válida.</i></para>
        /// </summary>
        public C_Word(C_String? source)
        {
            this.SetWord(source?.Value);
        }

        /// <summary>
        /// Copy constructor. Copies the word if valid; otherwise leaves empty.
        /// <para><i>[ES] Constructor de copia. Copia si es válida; si no, queda vacío.</i></para>
        /// </summary>
        public C_Word(C_Word? source)
        {
            if (source is not null && C_Hlp_String.IsWord(source.Value))
                this._value.Value = source.Value;
        }

#endregion

#region PUBLIC PROPERTIES

        /// <summary>
        /// Word text. Setter throws if the value is not a single word.
        /// <para><i>[ES] Texto. El setter lanza si no es una sola palabra.</i></para>
        /// </summary>
        public string Value
        {
            get => this._value.Value;
            set => this.SetWord(value);
        }

        /// <summary>
        /// Underlying string container (read-only reference to internal state).
        /// <para><i>[ES] Contenedor string subyacente (referencia al estado interno).</i></para>
        /// </summary>
        public C_String AsString => this._value;

        public int Length => this._value.Length;

        public bool IsNullOrEmpty => this._value.IsNullOrEmpty;

        public bool IsWord => C_Hlp_String.IsWord(this._value.Value);

        public bool IsDigits => C_Hlp_String.IsDigits(this._value.Value);

        public bool IsInteger => C_Hlp_String.IsInteger(this._value.Value);

        public bool IsDecimal => C_Hlp_String.IsDecimal(this._value.Value);

        public bool IsNumber => C_Hlp_String.IsNumber(this._value.Value);

        public bool IsOnlyLetters => C_Hlp_String.IsOnlyLetters(this._value.Value);

        public bool IsOnlyLettersAndNumbers => C_Hlp_String.IsOnlyLettersAndNumbers(this._value.Value);

        public bool IsOnlyLettersAndNumbersAndUnderscore
            => C_Hlp_String.IsOnlyLettersAndNumbersAndUnderscore(this._value.Value);

        public bool IsEmoji => C_Hlp_String.IsEmoji(this._value.Value);

        public bool ContainsEmoji => C_Hlp_String.ContainsEmoji(this._value.Value);

        public bool ContainsDigit => C_Hlp_String.ContainsDigit(this._value.Value);

        public bool ContainsLetter => C_Hlp_String.ContainsLetter(this._value.Value);

        public bool ContainsLetterOrDigit => C_Hlp_String.ContainsLetterOrDigit(this._value.Value);

        public bool ContainsWhiteSpace => C_Hlp_String.ContainsWhiteSpace(this._value.Value);

#endregion

#region PUBLIC METHODS

        public void SetWord(string? value)
        {
            if (!C_Hlp_String.IsWord(value))
                throw new ArgumentException("Value must be a single non-empty word without whitespace.", nameof(value));
            this._value.Value = value!;
        }

        public bool TrySet(string? value)
        {
            if (!C_Hlp_String.IsWord(value))
                return false;
            this._value.Value = value!;
            return true;
        }

        public static bool TryCreate(string? value, out C_Word? word)
        {
            if (!C_Hlp_String.IsWord(value))
            {
                word = null;
                return false;
            }
            word = new C_Word(value!);
            return true;
        }

#region PUBLIC METHODS — TryTo* / To*

        public bool TryToInt(out int value)
            => C_Hlp_String.TryToInt(this.Value, out value);

        public bool TryToLong(out long value)
            => C_Hlp_String.TryToLong(this.Value, out value);

        public bool TryToDouble(out double value)
            => C_Hlp_String.TryToDouble(this.Value, out value);

        public bool TryToDecimal(out decimal value)
            => C_Hlp_String.TryToDecimal(this.Value, out value);

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

        public C_String ToCString()
            => new C_String(this.Value);

        public C_Text ToText()
            => new C_Text(this.Value);

#endregion

        public C_Word Clone()
            => new C_Word(this);

        public override string ToString()
            => this._value.Value;

        public string Info()
            => $"C_Word IsWord={this.IsWord}, Value=\"{this._value.Value}\"";

#endregion

#region OPERATORS

        public static implicit operator string(C_Word w)
            => w is null ? string.Empty : w.Value;

        public static explicit operator C_Word(string s)
            => new C_Word(s);

#endregion
    }
}
