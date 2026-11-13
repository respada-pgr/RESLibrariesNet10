using _1_LibraryClasses;

namespace _1_LibraryClasses.Register
{
    /// <summary>
    /// Represents a generic Value Object container for unique identification that implements <see cref="I_Id{TId}"/>.
    /// <para><i>[ES] Representa un contenedor de Objeto de Valor genérico para identificación única que implementa <see cref="I_Id{TId}"/>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier. Must implement <see cref="IEquatable{TId}"/>.
    /// <para><i>[ES] El tipo subyacente del identificador único. Debe implementar <see cref="IEquatable{TId}"/>.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// C_Id&lt;string&gt; customId = new("USR-1024");
    /// string? rawValue = customId; // Implicit conversion
    /// </code>
    /// </example>
    public class C_IdString : C_Id<string>, I_Id<string>
    {
        /// <summary>
        /// Gets the unique identifier. <see cref="string.Empty"/> means not initialized.
        /// <para><i>[ES] Obtiene el identificador único. <see cref="string.Empty"/> significa no inicializado.</i></para>
        /// </summary>
        public new string Id
        {
            get => base.Id ?? string.Empty;
            protected set => base.Id = value ?? string.Empty;
        }

        /// <summary>
        /// Gets a value indicating whether a non-empty identity is assigned.
        /// <para><i>[ES] Indica si hay una identidad no vacía asignada.</i></para>
        /// </summary>
        public new bool IsInitialized => !string.IsNullOrEmpty(this.Id);

        /// <summary>
        /// Initializes a new instance as not initialized (<see cref="string.Empty"/>).
        /// <para><i>[ES] Inicializa una nueva instancia como no inicializada (<see cref="string.Empty"/>).</i></para>
        /// </summary>
        public C_IdString() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance with a specified value. Null is stored as <see cref="string.Empty"/>.
        /// <para><i>[ES] Inicializa una nueva instancia con un valor concreto. Null se almacena como <see cref="string.Empty"/>.</i></para>
        /// </summary>
        public C_IdString(string? id) : base(id ?? string.Empty) { }

        /// <summary>
        /// Copy constructor. If <paramref name="source"/> is <see langword="null"/>, uses <see cref="string.Empty"/>.
        /// <para><i>[ES] Constructor de copia. Si <paramref name="source"/> es <see langword="null"/>, usa <see cref="string.Empty"/>.</i></para>
        /// </summary>
        public C_IdString(C_IdString? source) : base(source is null ? string.Empty : source.Id) { }

        /// <summary>
        /// Initializes with the specified value. Throws if already initialized or value is null/empty.
        /// <para><i>[ES] Inicializa con el valor indicado. Lanza si ya está inicializado o el valor es null/vacío.</i></para>
        /// </summary>
        public string Init(string value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Id cannot be null or empty.", nameof(value));
            this.Id = value;
            return value;
        }

        /// <summary>
        /// Implicit conversion to <see cref="string"/>.
        /// <para><i>[ES] Conversión implícita a <see cref="string"/>.</i></para>
        /// </summary>
        public static implicit operator string(C_IdString? id) => id is null ? string.Empty : id.Id;

        /// <summary>
        /// Implicit conversion from <see cref="string"/>.
        /// <para><i>[ES] Conversión implícita desde <see cref="string"/>.</i></para>
        /// </summary>
        public static implicit operator C_IdString(string? id) => new(id);
    }
}
