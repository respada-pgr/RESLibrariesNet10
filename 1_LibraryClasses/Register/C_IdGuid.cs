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
    public class C_IdGuid : C_Id<Guid>, I_Id<Guid>, I_Initializable<Guid>
    {
        /// <summary>
        /// Gets the unique identifier as a non-nullable <see cref="Guid"/>.
        /// <para><see cref="Guid.Empty"/> means not initialized.</para>
        /// <para><i>[ES] Obtiene el identificador único como <see cref="Guid"/> no anulable.
        /// <see cref="Guid.Empty"/> significa no inicializado.</i></para>
        /// </summary>
        public new Guid Id
        {
            get
            {
                Guid? value = base.Id;
                return value.HasValue ? value.Value : Guid.Empty;
            }
            protected set => base.Id = value;
        }

        /// <summary>
        /// Gets a value indicating whether a valid (non-empty) identity is assigned.
        /// <para><i>[ES] Indica si hay una identidad válida (no vacía) asignada.</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != Guid.Empty;

        /// <summary>
        /// Initializes a new instance with a newly generated <see cref="Guid"/>.
        /// <para><i>[ES] Inicializa una nueva instancia con un <see cref="Guid"/> recién generado.</i></para>
        /// </summary>
        public C_IdGuid() : base(Guid.NewGuid()) { }

        /// <summary>
        /// Initializes a new instance with a specified <see cref="Guid"/>.
        /// <para><see cref="Guid.Empty"/> means not initialized.</para>
        /// <para><i>[ES] Inicializa una nueva instancia con un <see cref="Guid"/> especificado.
        /// <see cref="Guid.Empty"/> significa no inicializado.</i></para>
        /// </summary>
        /// <param name="id">The unique <see cref="Guid"/> value.</param>
        public C_IdGuid(Guid id) : base(id) { }

        /// <summary>
        /// Copy constructor. If <paramref name="source"/> is <see langword="null"/>, uses <see cref="Guid.Empty"/>.
        /// <para><i>[ES] Constructor de copia. Si <paramref name="source"/> es <see langword="null"/>, usa <see cref="Guid.Empty"/>.</i></para>
        /// </summary>
        public C_IdGuid(C_IdGuid? source) : base(source is null ? Guid.Empty : source.Id) { }

        /// <summary>
        /// Creates a new instance with a newly generated <see cref="Guid"/>.
        /// <para><i>[ES] Crea una nueva instancia con un <see cref="Guid"/> recién generado.</i></para>
        /// </summary>
        public static C_IdGuid New() => new(Guid.NewGuid());

        /// <summary>
        /// Initializes with a newly generated <see cref="Guid"/>. Throws if already initialized.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo. Lanza si ya está inicializado.</i></para>
        /// </summary>
        public Guid Init()
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            var newId = Guid.NewGuid();
            this.Id = newId;
            return newId;
        }

        /// <summary>
        /// Initializes with the specified <see cref="Guid"/>. Throws if already initialized or value is empty.
        /// <para><i>[ES] Inicializa con el <see cref="Guid"/> indicado. Lanza si ya está inicializado o el valor es vacío.</i></para>
        /// </summary>
        public Guid Init(Guid value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (value == Guid.Empty)
                throw new ArgumentException("Id cannot be Guid.Empty.", nameof(value));
            this.Id = value;
            return value;
        }

        /// <summary>
        /// Implicit conversion to <see cref="Guid"/>. Returns <see cref="Guid.Empty"/> if null or not initialized.
        /// <para><i>[ES] Conversión implícita a <see cref="Guid"/>. Devuelve <see cref="Guid.Empty"/> si es null o no está inicializado.</i></para>
        /// </summary>
        public static implicit operator Guid(C_IdGuid? id) => id is null ? Guid.Empty : id.Id;

        /// <summary>
        /// Implicit conversion from <see cref="Guid"/>.
        /// <para><i>[ES] Conversión implícita desde <see cref="Guid"/>.</i></para>
        /// </summary>
        public static implicit operator C_IdGuid(Guid id) => new(id);
    }

    /// <summary>
    /// Specialized class for <see cref="int"/>-based object identification.
    /// <para>
    /// Uses only <see cref="int"/> (not nullable). <c>0</c> means not initialized.
    /// There is no parameterless factory that generates a new identity; assign a value via constructor or <see cref="Init"/>.
    /// </para>
    /// <para><i>[ES] Clase especializada para identificación basada en <see cref="int"/>.
    /// Usa solo <see cref="int"/> (no anulable). <c>0</c> significa no inicializado.
    /// No hay fábrica que genere una identidad nueva; asigne un valor por constructor o <see cref="Init"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// var id = new C_IdInt();           // not initialized (0)
    /// var id2 = new C_IdInt(42);        // initialized
    /// id.Init(100);
    /// </code>
    /// </example>
}
