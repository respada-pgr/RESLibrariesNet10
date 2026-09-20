using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Base
{
    /// <summary>
    /// Represents the abstract base class for domain entities with a generic identifier, implementing <see cref="I_Entity{TId}"/>.
    /// <para><i>[ES] Representa la clase base abstracta para entidades de dominio con un identificador genérico, implementando <see cref="I_Entity{TId}"/>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier.
    /// <para><i>[ES] El tipo subyacente del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_Order : C_Entity&lt;string&gt;
    /// {
    ///     public C_Order() : base("ORD-001") { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_Entity<TId> : I_Entity<TId>
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// <para>
        /// When not initialized, the value may be <see langword="null"/> (“not assigned”) or
        /// <c>default(TId)</c> / <see cref="Guid.Empty"/> (“empty sentinel”). Both yield
        /// <see cref="IsInitialized"/> = <see langword="false"/>, but they are not the same information.
        /// </para>
        /// <para><i>[ES] Si no está inicializado, el valor puede ser <see langword="null"/> (“no asignado”) o
        /// <c>default(TId)</c> / <see cref="Guid.Empty"/> (“sentinela vacío”). Ambos dan
        /// <see cref="IsInitialized"/> = <see langword="false"/>, pero no aportan la misma información.</i></para>
        /// </summary>
        /// <value>
        /// The unique identifier of type <typeparamref name="TId"/>; <see langword="null"/> or <c>default</c> if unassigned.
        /// <para><i>[ES] El identificador único de tipo <typeparamref name="TId"/>; <see langword="null"/> o <c>default</c> si no está asignado.</i></para>
        /// </value>
        public TId? Id { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the object has been properly initialized.
        /// <para>
        /// An entity is considered initialized when it has a non-default identity assigned.
        /// </para>
        /// <para><i>[ES] Obtiene un valor que indica si el objeto ha sido inicializado correctamente.
        /// Una entidad se considera inicializada cuando tiene una identidad no predeterminada asignada.</i></para>
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the entity has a valid identity assigned; otherwise, <see langword="false"/>.
        /// <para><i>[ES] <see langword="true"/> si la entidad tiene una identidad válida asignada; de lo contrario, <see langword="false"/>.</i></para>
        /// </value>
        /// <example>
        /// <code>
        /// var order = new C_Order();
        /// if (order.IsInitialized)
        /// {
        ///     // Entity is initialized
        /// }
        /// </code>
        /// </example>
        public bool IsInitialized => this.Id is not null && !EqualityComparer<TId>.Default.Equals(this.Id, default!);

        /// <summary>
        /// Initializes a new instance of the <see cref="C_Entity{TId}"/> class with an unassigned identifier.
        /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Entity{TId}"/> con un identificador no asignado.</i></para>
        /// </summary>
        /// <example>
        /// <code>
        /// var order = new C_Order();
        /// </code>
        /// </example>
        protected C_Entity()
        {
            this.Id = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="C_Entity{TId}"/> class with a specific identifier.
        /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Entity{TId}"/> con un identificador específico.</i></para>
        /// </summary>
        /// <param name="id">
        /// The unique identifier value.
        /// <para><i>[ES] El valor del identificador único.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// var order = new C_Order("ORD-001");
        /// </code>
        /// </example>
        protected C_Entity(TId? id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Copy constructor for <see cref="C_Entity{TId}"/> from an <see cref="I_Identifiable{TId}"/> instance.
        /// <para><i>[ES] Constructor de copia para <see cref="C_Entity{TId}"/> a partir de una instancia de <see cref="I_Identifiable{TId}"/>.</i></para>
        /// </summary>
        /// <param name="source">
        /// The source instance to copy identity from.
        /// <para><i>[ES] La instancia de origen de la cual copiar la identidad.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// var order = new C_Order(sourceOrder);
        /// </code>
        /// </example>
        protected C_Entity(I_Identifiable<TId>? source)
        {
            this.Id = source is null ? default : source.Id;
        }
    }

    /// <summary>
    /// Specialized abstract base class for domain entities using <see cref="Guid"/> as identifier.
    /// <para>
    /// Uses only <see cref="Guid"/> (not <c>Guid?</c>). <see cref="Guid.Empty"/> means not initialized.
    /// </para>
    /// <para><i>[ES] Clase base abstracta especializada para entidades de dominio con identificador <see cref="Guid"/>.
    /// Usa solo <see cref="Guid"/> (no <c>Guid?</c>). <see cref="Guid.Empty"/> significa no inicializado.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_User : C_EntityGuid
    /// {
    ///     public C_User() : base() { }
    ///     public C_User(Guid id) : base(id) { }
    /// }
    ///
    /// var user = new C_User(Guid.Empty);
    /// Guid id = user.Init();
    /// </code>
    /// </example>
    public abstract class C_EntityGuid : C_Entity<Guid>, I_Entity<Guid>, I_Initializable<Guid>
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
        protected C_EntityGuid() : base(Guid.NewGuid()) { }

        /// <summary>
        /// Initializes a new instance with a specified <see cref="Guid"/>.
        /// <para><see cref="Guid.Empty"/> means not initialized.</para>
        /// <para><i>[ES] Inicializa una nueva instancia con un <see cref="Guid"/> especificado.
        /// <see cref="Guid.Empty"/> significa no inicializado.</i></para>
        /// </summary>
        protected C_EntityGuid(Guid id) : base(id) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{Guid}"/> instance.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{Guid}"/>.</i></para>
        /// </summary>
        protected C_EntityGuid(I_Identifiable<Guid>? source)
            : base(source?.Id is Guid id && id != Guid.Empty ? id : Guid.Empty) { }

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
    }

    /// <summary>
    /// Specialized abstract base class for domain entities using <see cref="int"/> as identifier.
    /// <para>
    /// Uses only <see cref="int"/>. <c>0</c> means not initialized.
    /// Identity is typically assigned by the persistence layer (e.g. identity column).
    /// </para>
    /// <para><i>[ES] Clase base abstracta para entidades con identificador <see cref="int"/>.
    /// Usa solo <see cref="int"/>. <c>0</c> significa no inicializado.
    /// La identidad suele asignarla la capa de persistencia (p. ej. columna identity).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Order : C_EntityInt
    /// {
    ///     public C_Order() : base() { }
    ///     public C_Order(int id) : base(id) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_EntityInt : C_Entity<int>, I_Entity<int>
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
        /// Gets a value indicating whether a valid identity is assigned.
        /// <para><i>[ES] Indica si hay una identidad válida asignada.</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != 0;

        /// <summary>
        /// Initializes a new instance as not initialized (<c>0</c>).
        /// <para><i>[ES] Inicializa una nueva instancia como no inicializada (<c>0</c>).</i></para>
        /// </summary>
        protected C_EntityInt() : base(0) { }

        /// <summary>
        /// Initializes a new instance with a specified identifier.
        /// <para><i>[ES] Inicializa una nueva instancia con un identificador concreto.</i></para>
        /// </summary>
        protected C_EntityInt(int id) : base(id) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{int}"/> instance.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{int}"/>.</i></para>
        /// </summary>
        protected C_EntityInt(I_Identifiable<int>? source)
            : base(source?.Id is int id && id != 0 ? id : 0) { }

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
    }

    /// <summary>
    /// Specialized abstract base class for domain entities using <see cref="long"/> as identifier.
    /// <para>
    /// Uses only <see cref="long"/>. <c>0L</c> means not initialized.
    /// Identity is typically assigned by the persistence layer (e.g. identity column).
    /// </para>
    /// <para><i>[ES] Clase base abstracta para entidades con identificador <see cref="long"/>.
    /// Usa solo <see cref="long"/>. <c>0L</c> significa no inicializado.
    /// La identidad suele asignarla la capa de persistencia (p. ej. columna identity).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Order : C_EntityLong
    /// {
    ///     public C_Order() : base() { }
    ///     public C_Order(long id) : base(id) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_EntityLong : C_Entity<long>, I_Entity<long>
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
        /// Gets a value indicating whether a valid identity is assigned.
        /// <para><i>[ES] Indica si hay una identidad válida asignada.</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != 0L;

        /// <summary>
        /// Initializes a new instance as not initialized (<c>0L</c>).
        /// <para><i>[ES] Inicializa una nueva instancia como no inicializada (<c>0L</c>).</i></para>
        /// </summary>
        protected C_EntityLong() : base(0L) { }

        /// <summary>
        /// Initializes a new instance with a specified identifier.
        /// <para><i>[ES] Inicializa una nueva instancia con un identificador concreto.</i></para>
        /// </summary>
        protected C_EntityLong(long id) : base(id) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{long}"/> instance.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{long}"/>.</i></para>
        /// </summary>
        protected C_EntityLong(I_Identifiable<long>? source)
            : base(source?.Id is long id && id != 0L ? id : 0L) { }

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
    }

    /// <summary>
    /// Specialized abstract base class for domain entities using <see cref="string"/> as identifier.
    /// <para>
    /// Uses only <see cref="string"/> (never <see langword="null"/> in this specialization).
    /// <see cref="string.Empty"/> means not initialized.
    /// </para>
    /// <para><i>[ES] Clase base abstracta para entidades con identificador <see cref="string"/>.
    /// Usa solo <see cref="string"/> (nunca <see langword="null"/> en esta especialización).
    /// <see cref="string.Empty"/> significa no inicializado.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Order : C_EntityString
    /// {
    ///     public C_Order() : base() { }
    ///     public C_Order(string id) : base(id) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_EntityString : C_Entity<string>, I_Entity<string>
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
        protected C_EntityString() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance with a specified identifier. Null is stored as <see cref="string.Empty"/>.
        /// <para><i>[ES] Inicializa una nueva instancia con un identificador concreto. Null se almacena como <see cref="string.Empty"/>.</i></para>
        /// </summary>
        protected C_EntityString(string? id) : base(id ?? string.Empty) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{TId}"/> of <see cref="string"/>.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{TId}"/> de <see cref="string"/>.</i></para>
        /// </summary>
        protected C_EntityString(I_Identifiable<string>? source)
            : base(source is null ? string.Empty : (source.Id ?? string.Empty)) { }

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
    }
}
