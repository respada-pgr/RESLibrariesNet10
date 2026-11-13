using System.Text;

namespace _1_LibraryClasses.Collection
{
    /// <summary>
    /// Specialized <see cref="C_List{T}"/> for <see cref="string"/> elements (e.g. lines of text).
    /// <para>
    /// Inherits the full <see cref="I_List{T}"/> / <see cref="C_List{T}"/> API and adds text-oriented helpers
    /// (<see cref="Import"/>, <see cref="Join"/>, <see cref="TrimLines"/>, etc.).
    /// </para>
    /// <para><i>[ES] Especialización de <see cref="C_List{T}"/> para elementos <see cref="string"/> (p. ej. líneas de texto).
    /// Hereda la API completa de <see cref="I_List{T}"/> / <see cref="C_List{T}"/> y añade ayudas orientadas a texto
    /// (<see cref="Import"/>, <see cref="Join"/>, <see cref="TrimLines"/>, etc.).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var lines = new C_List_String();
    /// lines.Import("header\nbody\nfooter");
    /// lines.TrimLines();
    /// lines.RemoveEmptyLines();
    /// bool ok = lines.ContainsLine("body");
    /// string text = lines.Join("|");
    /// </code>
    /// </example>
    public class C_List_String : C_List<string>
    {
#region CONSTRUCTORS

        /// <summary>
        /// Initializes an empty string list.
        /// <para><i>[ES] Inicializa una lista de cadenas vacía.</i></para>
        /// </summary>
        public C_List_String() { }

        /// <summary>
        /// Initializes an empty string list with the specified capacity.
        /// <para><i>[ES] Inicializa una lista de cadenas vacía con la capacidad indicada.</i></para>
        /// </summary>
        /// <param name="capacity">
        /// The number of elements that the new list can initially store.
        /// <para><i>[ES] Número de elementos que la lista puede almacenar inicialmente.</i></para>
        /// </param>
        public C_List_String(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a string list with elements copied from the specified collection.
        /// <para><i>[ES] Inicializa una lista de cadenas con elementos copiados de la colección indicada.</i></para>
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements are copied to the new list.
        /// <para><i>[ES] Colección cuyos elementos se copian a la nueva lista.</i></para>
        /// </param>
        public C_List_String(IEnumerable<string> collection) : base(collection) { }

        /// <summary>
        /// Initializes a string list with elements copied from another <see cref="C_List_String"/>.
        /// <para><i>[ES] Inicializa una lista de cadenas con elementos copiados de otro <see cref="C_List_String"/>.</i></para>
        /// </summary>
        /// <param name="source">
        /// The source list to copy.
        /// <para><i>[ES] Lista de origen a copiar.</i></para>
        /// </param>
        public C_List_String(C_List_String source) : base(source) { }

        /// <summary>
        /// Initializes a string list from the content of a <see cref="StringBuilder"/> (split into lines).
        /// <para><i>[ES] Inicializa una lista de cadenas a partir del contenido de un <see cref="StringBuilder"/> (partido en líneas).</i></para>
        /// </summary>
        /// <param name="builder">
        /// The source builder; if <see langword="null"/> or empty, the list remains empty.
        /// <para><i>[ES] Builder de origen; si es <see langword="null"/> o vacío, la lista queda vacía.</i></para>
        /// </param>
        public C_List_String(StringBuilder? builder)
        {
            if (builder is not null && builder.Length > 0)
                this.Import(builder.ToString());
        }

#endregion

#region PUBLIC METHODS — Get

        /// <summary>
        /// Joins all lines using the specified separator.
        /// <para><i>[ES] Une todas las líneas usando el separador indicado.</i></para>
        /// </summary>
        /// <param name="separator">
        /// The separator between lines (use <see cref="string.Empty"/> to concatenate with no separator).
        /// <para><i>[ES] Separador entre líneas (use <see cref="string.Empty"/> para concatenar sin separador).</i></para>
        /// </param>
        /// <returns>
        /// The joined text, or <see cref="string.Empty"/> if the list is empty.
        /// <para><i>[ES] El texto unido, o <see cref="string.Empty"/> si la lista está vacía.</i></para>
        /// </returns>
        /// <example>
        /// <code>
        /// string csv = lines.Join(",");
        /// </code>
        /// </example>
        public string Join(string separator)
        {
            if (this.Count == 0)
                return string.Empty;
            return string.Join(separator ?? string.Empty, this);
        }

        /// <summary>
        /// Joins all lines using the platform newline (no trailing newline).
        /// <para><i>[ES] Une todas las líneas con el salto de línea de la plataforma (sin salto final).</i></para>
        /// </summary>
        public override string ToString() => this.Join(Environment.NewLine);

        /// <summary>
        /// Builds a <see cref="StringBuilder"/> with all lines separated by the platform newline.
        /// <para><i>[ES] Construye un <see cref="StringBuilder"/> con todas las líneas separadas por el salto de línea de la plataforma.</i></para>
        /// </summary>
        /// <returns>
        /// A new <see cref="StringBuilder"/> (never <see langword="null"/>).
        /// <para><i>[ES] Un <see cref="StringBuilder"/> nuevo (nunca <see langword="null"/>).</i></para>
        /// </returns>
        public StringBuilder ToStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                if (i > 0)
                    sb.AppendLine();
                sb.Append(this[i]);
            }
            return sb;
        }

        /// <summary>
        /// Determines whether the list contains the specified line (ordinal comparison).
        /// <para><i>[ES] Indica si la lista contiene la línea indicada (comparación ordinal).</i></para>
        /// </summary>
        public bool ContainsLine(string? line) => this.Contains(line!);

        /// <summary>
        /// Returns the zero-based index of the first occurrence of the specified line, or <c>-1</c>.
        /// <para><i>[ES] Devuelve el índice base cero de la primera aparición de la línea, o <c>-1</c>.</i></para>
        /// </summary>
        public int IndexOfLine(string? line) => this.IndexOf(line!);

        /// <summary>
        /// Returns the zero-based index of the last occurrence of the specified line, or <c>-1</c>.
        /// <para><i>[ES] Devuelve el índice base cero de la última aparición de la línea, o <c>-1</c>.</i></para>
        /// </summary>
        public int LastIndexOfLine(string? line) => this.LastIndexOf(line!);

        /// <summary>
        /// Returns <see langword="true"/> if the first line starts with <paramref name="value"/>.
        /// <para><i>[ES] Devuelve <see langword="true"/> si la primera línea empieza por <paramref name="value"/>.</i></para>
        /// </summary>
        public bool StartsWith(string? value)
        {
            if (this.Count == 0 || value is null)
                return false;
            return this[0]?.StartsWith(value, StringComparison.Ordinal) == true;
        }

        /// <summary>
        /// Returns <see langword="true"/> if the last line ends with <paramref name="value"/>.
        /// <para><i>[ES] Devuelve <see langword="true"/> si la última línea termina por <paramref name="value"/>.</i></para>
        /// </summary>
        public bool EndsWith(string? value)
        {
            if (this.Count == 0 || value is null)
                return false;
            return this[this.Count - 1]?.EndsWith(value, StringComparison.Ordinal) == true;
        }

        /// <summary>
        /// Returns <see langword="true"/> if any line contains <paramref name="value"/>.
        /// <para><i>[ES] Devuelve <see langword="true"/> si alguna línea contiene <paramref name="value"/>.</i></para>
        /// </summary>
        public bool AnyLineContains(string? value)
        {
            if (value is null)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i]?.Contains(value, StringComparison.Ordinal) == true)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns <see langword="true"/> if any line starts with <paramref name="value"/>.
        /// <para><i>[ES] Devuelve <see langword="true"/> si alguna línea empieza por <paramref name="value"/>.</i></para>
        /// </summary>
        public bool AnyLineStartsWith(string? value)
        {
            if (value is null)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i]?.StartsWith(value, StringComparison.Ordinal) == true)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns <see langword="true"/> if any line ends with <paramref name="value"/>.
        /// <para><i>[ES] Devuelve <see langword="true"/> si alguna línea termina por <paramref name="value"/>.</i></para>
        /// </summary>
        public bool AnyLineEndsWith(string? value)
        {
            if (value is null)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i]?.EndsWith(value, StringComparison.Ordinal) == true)
                    return true;
            }
            return false;
        }

#endregion

#region PUBLIC METHODS — Add

        /// <summary>
        /// Splits <paramref name="text"/> into lines and appends them to the list.
        /// <para>
        /// Recognizes <c>\r\n</c>, <c>\n</c> and <c>\r</c> as line breaks.
        /// </para>
        /// <para><i>[ES] Parte <paramref name="text"/> en líneas y las añade a la lista.
        /// Reconoce <c>\r\n</c>, <c>\n</c> y <c>\r</c> como saltos de línea.</i></para>
        /// </summary>
        /// <param name="text">
        /// The text to import; <see langword="null"/> or empty is ignored.
        /// <para><i>[ES] Texto a importar; <see langword="null"/> o vacío se ignora.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// lines.Import("a\nb\nc");
        /// </code>
        /// </example>
        public void Import(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            foreach (string line in SplitLines(text))
                this.Add(line);
        }

        /// <summary>
        /// Appends the content of a <see cref="StringBuilder"/> as a single element (not split into lines).
        /// <para><i>[ES] Añade el contenido de un <see cref="StringBuilder"/> como un solo elemento (sin partir en líneas).</i></para>
        /// </summary>
        /// <param name="builder">
        /// The builder to append; <see langword="null"/> is ignored.
        /// <para><i>[ES] Builder a añadir; <see langword="null"/> se ignora.</i></para>
        /// </param>
        public void Add(StringBuilder? builder)
        {
            if (builder is null)
                return;
            this.Add(builder.ToString());
        }

#endregion

#region PUBLIC METHODS — Replace / modify

        /// <summary>
        /// Trims leading and trailing white-space on every line.
        /// <para><i>[ES] Elimina espacios en blanco al inicio y al final de cada línea.</i></para>
        /// </summary>
        public void TrimLines()
        {
            for (int i = 0; i < this.Count; i++)
            {
                string? line = this[i];
                this[i] = line?.Trim() ?? string.Empty;
            }
        }

#endregion

#region PUBLIC METHODS — Remove

        /// <summary>
        /// Removes null, empty, or white-space-only lines.
        /// <para><i>[ES] Elimina líneas null, vacías o solo con espacios en blanco.</i></para>
        /// </summary>
        /// <returns>
        /// The number of lines removed.
        /// <para><i>[ES] Número de líneas eliminadas.</i></para>
        /// </returns>
        public int RemoveEmptyLines()
        {
            return this.RemoveAll(static s => string.IsNullOrWhiteSpace(s));
        }

#endregion

#region PRIVATE METHODS

        /// <summary>
        /// Splits text into lines recognizing <c>\r\n</c>, <c>\n</c> and <c>\r</c>.
        /// <para><i>[ES] Parte el texto en líneas reconociendo <c>\r\n</c>, <c>\n</c> y <c>\r</c>.</i></para>
        /// </summary>
        private static IEnumerable<string> SplitLines(string text)
        {
            int start = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c == '\r')
                {
                    yield return text[start..i];
                    if (i + 1 < text.Length && text[i + 1] == '\n')
                        i++;
                    start = i + 1;
                }
                else if (c == '\n')
                {
                    yield return text[start..i];
                    start = i + 1;
                }
            }
            if (start <= text.Length)
                yield return text[start..];
        }

#endregion
    }
}
