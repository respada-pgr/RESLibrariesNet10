namespace _1_LibraryClassesNet10.Classes
{
    /// <summary>
    /// Represents a typed value captured or recorded at a specific point in time.
    /// <para>
    /// Use for readings, snapshots, or historical samples that do not require entity identity.
    /// <see cref="Date"/> is the instant when the value was taken (preferably UTC).
    /// </para>
    /// <para><i>[ES] Representa un valor tipado capturado o registrado en un instante concreto.
    /// Útil para lecturas, instantáneas o muestras históricas que no requieren identidad de entidad.
    /// <see cref="Date"/> es el momento en que se tomó el valor (preferiblemente UTC).</i></para>
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the stored value.
    /// <para><i>[ES] El tipo del valor almacenado.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// var sample = new C_ValueDatedInt(42, DateTime.UtcNow);
    /// int n = sample.Value;
    /// </code>
    /// </example>
    public class C_ValueDated<TValue>
    {
        /// <summary>
        /// Gets or sets the value that was recorded.
        /// <para><i>[ES] Obtiene o establece el valor que fue registrado.</i></para>
        /// </summary>
        public TValue Value { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the value was taken (preferably UTC).
        /// <para><i>[ES] Obtiene o establece la fecha y hora en que se tomó el valor (preferiblemente UTC).</i></para>
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Initializes a new instance with a default value and <see cref="Date"/> = UTC now.
        /// <para><i>[ES] Inicializa una nueva instancia con valor por defecto y <see cref="Date"/> = UTC ahora.</i></para>
        /// </summary>
        public C_ValueDated()
        {
            this.Value = default!;
            this.Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance with the specified value and capture date.
        /// <para><i>[ES] Inicializa una nueva instancia con el valor y la fecha de captura indicados.</i></para>
        /// </summary>
        /// <param name="value">The recorded value.</param>
        /// <param name="date">When the value was taken. If omitted, UTC now is used.</param>
        public C_ValueDated(TValue value, DateTime? date = null)
        {
            this.Value = value;
            this.Date = date ?? DateTime.UtcNow;
        }
    }

    /// <summary>
    /// An <see cref="int"/> value recorded at a specific date/time.
    /// <para><i>[ES] Un valor <see cref="int"/> registrado en una fecha/hora concreta.</i></para>
    /// </summary>
    public class C_ValueDatedInt : C_ValueDated<int>
    {
        /// <summary>
        /// Initializes with value <c>0</c> and <see cref="C_ValueDated{TValue}.Date"/> = UTC now.
        /// <para><i>[ES] Inicializa con valor <c>0</c> y fecha UTC ahora.</i></para>
        /// </summary>
        public C_ValueDatedInt() : base(0, DateTime.UtcNow) { }

        /// <summary>
        /// Initializes with the specified value and optional capture date (UTC now if omitted).
        /// <para><i>[ES] Inicializa con el valor indicado y fecha de captura opcional (UTC ahora si se omite).</i></para>
        /// </summary>
        public C_ValueDatedInt(int value, DateTime? date = null) : base(value, date) { }
    }

    /// <summary>
    /// A <see cref="float"/> value recorded at a specific date/time.
    /// <para><i>[ES] Un valor <see cref="float"/> registrado en una fecha/hora concreta.</i></para>
    /// </summary>
    public class C_ValueDatedFloat : C_ValueDated<float>
    {
        /// <summary>
        /// Initializes with value <c>0</c> and <see cref="C_ValueDated{TValue}.Date"/> = UTC now.
        /// <para><i>[ES] Inicializa con valor <c>0</c> y fecha UTC ahora.</i></para>
        /// </summary>
        public C_ValueDatedFloat() : base(0f, DateTime.UtcNow) { }

        /// <summary>
        /// Initializes with the specified value and optional capture date (UTC now if omitted).
        /// <para><i>[ES] Inicializa con el valor indicado y fecha de captura opcional (UTC ahora si se omite).</i></para>
        /// </summary>
        public C_ValueDatedFloat(float value, DateTime? date = null) : base(value, date) { }
    }

    /// <summary>
    /// A <see cref="string"/> value recorded at a specific date/time.
    /// <para><i>[ES] Un valor <see cref="string"/> registrado en una fecha/hora concreta.</i></para>
    /// </summary>
    public class C_ValueDatedString : C_ValueDated<string>
    {
        /// <summary>
        /// Initializes with an empty string and <see cref="C_ValueDated{TValue}.Date"/> = UTC now.
        /// <para><i>[ES] Inicializa con cadena vacía y fecha UTC ahora.</i></para>
        /// </summary>
        public C_ValueDatedString() : base(string.Empty, DateTime.UtcNow) { }

        /// <summary>
        /// Initializes with the specified value and optional capture date (UTC now if omitted).
        /// <para><i>[ES] Inicializa con el valor indicado y fecha de captura opcional (UTC ahora si se omite).</i></para>
        /// </summary>
        public C_ValueDatedString(string? value, DateTime? date = null)
            : base(value ?? string.Empty, date) { }
    }
}
