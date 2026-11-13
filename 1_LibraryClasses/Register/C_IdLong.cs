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
    public class C_IdLong : C_Id<long>, I_Id<long>
    {
        /// <summary>
        /// Gets the unique identifier. <c>0L</c> means not initialized.
        /// <para><i>[ES] Obtiene el identificador único. <c>0L</c> significa no inicializado.</i></para>
        /// </summary>
        public new long Id
        {
            get
            {
                long? value = base.Id;
                return value.HasValue ? value.Value : 0L;
            }
            protected set => base.Id = value;
        }

        /// <summary>
        /// Gets a value indicating whether a valid identity is assigned (<c>Id != 0L</c>).
        /// <para><i>[ES] Indica si hay una identidad válida asignada (<c>Id != 0L</c>).</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != 0L;

        /// <summary>
        /// Initializes a new instance as not initialized (<c>0L</c>).
        /// <para><i>[ES] Inicializa una nueva instancia como no inicializada (<c>0L</c>).</i></para>
        /// </summary>
        public C_IdLong() : base(0L) { }

        /// <summary>
        /// Initializes a new instance with a specified value. <c>0L</c> means not initialized.
        /// <para><i>[ES] Inicializa una nueva instancia con un valor concreto. <c>0L</c> significa no inicializado.</i></para>
        /// </summary>
        public C_IdLong(long id) : base(id) { }

        /// <summary>
        /// Copy constructor. If <paramref name="source"/> is <see langword="null"/>, uses <c>0L</c>.
        /// <para><i>[ES] Constructor de copia. Si <paramref name="source"/> es <see langword="null"/>, usa <c>0L</c>.</i></para>
        /// </summary>
        public C_IdLong(C_IdLong? source) : base(source is null ? 0L : source.Id) { }

        /// <summary>
        /// Initializes with the specified value. Throws if already initialized or value is <c>0L</c>.
        /// <para><i>[ES] Inicializa con el valor indicado. Lanza si ya está inicializado o el valor es <c>0L</c>.</i></para>
        /// </summary>
        public long Init(long value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (value == 0L)
                throw new ArgumentException("Id cannot be 0L.", nameof(value));
            this.Id = value;
            return value;
        }

        /// <summary>
        /// Implicit conversion to <see cref="long"/>.
        /// <para><i>[ES] Conversión implícita a <see cref="long"/>.</i></para>
        /// </summary>
        public static implicit operator long(C_IdLong? id) => id is null ? 0L : id.Id;

        /// <summary>
        /// Implicit conversion from <see cref="long"/>.
        /// <para><i>[ES] Conversión implícita desde <see cref="long"/>.</i></para>
        /// </summary>
        public static implicit operator C_IdLong(long id) => new(id);
    }

    /// <summary>
    /// Specialized class for <see cref="string"/>-based object identification.
    /// <para>
    /// Uses only <see cref="string"/> (never <see langword="null"/> in this specialization).
    /// <see cref="string.Empty"/> means not initialized.
    /// </para>
    /// <para><i>[ES] Clase especializada para identificación basada en <see cref="string"/>.
    /// Usa solo <see cref="string"/> (nunca <see langword="null"/> en esta especialización).
    /// <see cref="string.Empty"/> significa no inicializado.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var id = new C_IdString();              // not initialized
    /// var id2 = new C_IdString("ORD-001");    // initialized
    /// id.Init("USR-100");
    /// </code>
    /// </example>
}
