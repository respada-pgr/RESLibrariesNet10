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
    public class C_IdInt : C_Id<int>, I_Id<int>
    {
        /// <summary>
        /// Gets the unique identifier. <c>0</c> means not initialized.
        /// <para><i>[ES] Obtiene el identificador único. <c>0</c> significa no inicializado.</i></para>
        /// </summary>
        public new int Id
        {
            get
            {
                int? value = base.Id;
                return value.HasValue ? value.Value : 0;
            }
            protected set => base.Id = value;
        }

        /// <summary>
        /// Gets a value indicating whether a valid identity is assigned (<c>Id != 0</c>).
        /// <para><i>[ES] Indica si hay una identidad válida asignada (<c>Id != 0</c>).</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != 0;

        /// <summary>
        /// Initializes a new instance as not initialized (<c>0</c>).
        /// <para><i>[ES] Inicializa una nueva instancia como no inicializada (<c>0</c>).</i></para>
        /// </summary>
        public C_IdInt() : base(0) { }

        /// <summary>
        /// Initializes a new instance with a specified value. <c>0</c> means not initialized.
        /// <para><i>[ES] Inicializa una nueva instancia con un valor concreto. <c>0</c> significa no inicializado.</i></para>
        /// </summary>
        public C_IdInt(int id) : base(id) { }

        /// <summary>
        /// Copy constructor. If <paramref name="source"/> is <see langword="null"/>, uses <c>0</c>.
        /// <para><i>[ES] Constructor de copia. Si <paramref name="source"/> es <see langword="null"/>, usa <c>0</c>.</i></para>
        /// </summary>
        public C_IdInt(C_IdInt? source) : base(source is null ? 0 : source.Id) { }

        /// <summary>
        /// Initializes with the specified value. Throws if already initialized or value is <c>0</c>.
        /// <para><i>[ES] Inicializa con el valor indicado. Lanza si ya está inicializado o el valor es <c>0</c>.</i></para>
        /// </summary>
        public int Init(int value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (value == 0)
                throw new ArgumentException("Id cannot be 0.", nameof(value));
            this.Id = value;
            return value;
        }

        /// <summary>
        /// Implicit conversion to <see cref="int"/>.
        /// <para><i>[ES] Conversión implícita a <see cref="int"/>.</i></para>
        /// </summary>
        public static implicit operator int(C_IdInt? id) => id is null ? 0 : id.Id;

        /// <summary>
        /// Implicit conversion from <see cref="int"/>.
        /// <para><i>[ES] Conversión implícita desde <see cref="int"/>.</i></para>
        /// </summary>
        public static implicit operator C_IdInt(int id) => new(id);
    }

    /// <summary>
    /// Specialized class for <see cref="long"/>-based object identification.
    /// <para>
    /// Uses only <see cref="long"/> (not nullable). <c>0L</c> means not initialized.
    /// There is no parameterless factory that generates a new identity; assign a value via constructor or <see cref="Init"/>.
    /// </para>
    /// <para><i>[ES] Clase especializada para identificación basada en <see cref="long"/>.
    /// Usa solo <see cref="long"/> (no anulable). <c>0L</c> significa no inicializado.
    /// No hay fábrica que genere una identidad nueva; asigne un valor por constructor o <see cref="Init"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var id = new C_IdLong();           // not initialized (0L)
    /// var id2 = new C_IdLong(42);        // initialized
    /// id.Init(100);
    /// </code>
    /// </example>
}
