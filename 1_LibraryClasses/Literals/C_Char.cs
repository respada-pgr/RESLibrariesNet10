using System;

namespace _1_LibraryClasses
{
    /// <summary>
    /// Mutable wrapper around a single <see cref="char"/>.
    /// Factories: <see cref="Create(char)"/> / <see cref="TryCreate(string)"/>.
    /// Logic: <see cref="C_Hlp_Char"/>.
    /// <para><i>[ES] Contenedor mutable de un <see cref="char"/>.
    /// Factorías Create / TryCreate. Lógica en <see cref="C_Hlp_Char"/>.</i></para>
    /// </summary>
    public class C_Char : I_Infoable
    {
#region PRIVATE FIELDS

        private char _value;

#endregion

#region PUBLIC PROPERTIES

        public char Value
        {
            get => this._value;
            set => this._value = value;
        }

        public bool IsDigit => C_Hlp_Char.IsDigit(this._value);
        public bool IsAsciiDigit => C_Hlp_Char.IsAsciiDigit(this._value);
        public bool IsLetter => C_Hlp_Char.IsLetter(this._value);
        public bool IsLetterOrDigit => C_Hlp_Char.IsLetterOrDigit(this._value);
        public bool IsWhiteSpace => C_Hlp_Char.IsWhiteSpace(this._value);
        public bool IsUpper => C_Hlp_Char.IsUpper(this._value);
        public bool IsLower => C_Hlp_Char.IsLower(this._value);
        public bool IsPunctuation => C_Hlp_Char.IsPunctuation(this._value);
        public bool IsSymbol => C_Hlp_Char.IsSymbol(this._value);
        public bool IsControl => C_Hlp_Char.IsControl(this._value);
        public bool IsNumber => C_Hlp_Char.IsNumber(this._value);

#endregion

#region CONSTRUCTORS

        public C_Char() { this._value = default; }
        public C_Char(char value) { this._value = value; }

        public C_Char(string? text)
        {
            if (!C_Hlp_Char.TryParse(text, out this._value))
                this._value = default;
        }

        public C_Char(C_Char? source)
        {
            this._value = source?._value ?? default;
        }

#endregion

#region PUBLIC METHODS — factory

        public static C_Char Create(char value) => C_Hlp_Char.Create(value);
        public static C_Char Create(string text) => C_Hlp_Char.Create(text);
        public static C_Char Create(C_Char? source) => C_Hlp_Char.Create(source);

        /// <summary>
        /// Tries to create from the first character of text; <see langword="null"/> if empty.
        /// </summary>
        public static C_Char? TryCreate(string? text) => C_Hlp_Char.TryCreate(text);

#endregion

#region PUBLIC METHODS — TryTo* / To*

        public bool TryToInt(out int value) => C_Hlp_Char.TryToInt(this._value, out value);
        public int ToInt() => C_Hlp_Char.ToInt(this._value);
        public C_String ToCString() => new C_String(this._value.ToString());
        public C_Text ToText() => new C_Text(this._value.ToString());

#endregion

#region PUBLIC METHODS — transform in-place

        public C_Char ToUpperInPlace()
        {
            this._value = C_Hlp_Char.ToUpper(this._value);
            return this;
        }

        public C_Char ToLowerInPlace()
        {
            this._value = C_Hlp_Char.ToLower(this._value);
            return this;
        }

#endregion

#region PUBLIC METHODS

        public C_Char Clone() => new C_Char(this);
        public override string ToString() => this._value.ToString();
        public string Info() => $"C_Char Value='{this._value}', IsLetter={this.IsLetter}, IsDigit={this.IsDigit}";

#endregion

#region OPERATORS

        public static implicit operator char(C_Char c) => c?._value ?? default;
        public static implicit operator C_Char(char c) => new C_Char(c);
        public static explicit operator C_Char(string s) => new C_Char(s);

#endregion
    }
}
