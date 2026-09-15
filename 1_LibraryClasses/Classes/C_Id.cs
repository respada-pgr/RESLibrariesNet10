using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes
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
    public class C_Id<TId> : I_Id<TId> where TId : IEquatable<TId>
    {
        /// <summary>
        /// Gets the unique identifier of the object.
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
        /// An object is considered initialized when it has a non-default identity assigned.
        /// </para>
        /// <para><i>[ES] Obtiene un valor que indica si el objeto ha sido inicializado correctamente.
        /// Un objeto se considera inicializado cuando tiene una identidad no predeterminada asignada.</i></para>
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the object has a valid identity assigned; otherwise, <see langword="false"/>.
        /// <para><i>[ES] <see langword="true"/> si el objeto tiene una identidad válida asignada; de lo contrario, <see langword="false"/>.</i></para>
        /// </value>
        /// <example>
        /// <code>
        /// var id = new C_Id&lt;string&gt;("USR-1024");
        /// if (id.IsInitialized)
        /// {
        ///     // Object is initialized
        /// }
        /// </code>
        /// </example>
        public bool IsInitialized => this.Id is not null && !EqualityComparer<TId>.Default.Equals(this.Id, default!);

        /// <summary>
        /// Initializes a new instance of the <see cref="C_Id{TId}"/> class with an unassigned identifier (<see langword="null"/>).
        /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Id{TId}"/> con un identificador no asignado (<see langword="null"/>).</i></para>
        /// </summary>
        /// <example>
        /// <code>
        /// var id = new C_Id&lt;string&gt;();
        /// </code>
        /// </example>
        public C_Id()
        {
            this.Id = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="C_Id{TId}"/> class with a specific identifier value.
        /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Id{TId}"/> con un valor de identificador específico.</i></para>
        /// </summary>
        /// <param name="id">
        /// The unique identifier value.
        /// <para><i>[ES] El valor del identificador único.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// var id = new C_Id&lt;string&gt;("USR-1024");
        /// </code>
        /// </example>
        public C_Id(TId? id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="C_Id{TId}"/> class by copying another instance.
        /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Id{TId}"/> copiando otra instancia.</i></para>
        /// </summary>
        /// <param name="source">
        /// The source instance to copy from.
        /// <para><i>[ES] La instancia de origen desde la cual copiar.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// var id = new C_Id&lt;string&gt;("USR-1024");
        /// var id2 = new C_Id&lt;string&gt;(id);
        /// </code>
        /// </example>
        public C_Id(C_Id<TId>? source)
        {
            this.Id = source is null ? default : source.Id;
        }

        /// <summary>
        /// Determines whether the current instance is equal to another <see cref="I_Id{TId}"/> object.
        /// <para><i>[ES] Determina si la instancia actual es igual a otro objeto <see cref="I_Id{TId}"/>.</i></para>
        /// </summary>
        /// <param name="other">
        /// The object to compare with the current object.
        /// <para><i>[ES] El objeto a comparar con el objeto actual.</i></para>
        /// </param>
        /// <returns>
        /// <see langword="true"/> if both instances have the same identifier value; otherwise, <see langword="false"/>.
        /// <para><i>[ES] <see langword="true"/> si ambas instancias tienen el mismo valor de identificador; de lo contrario, <see langword="false"/>.</i></para>
        /// </returns>
        public bool Equals(I_Id<TId>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.Id is null) return other.Id is null;
            return this.Id.Equals(other.Id);
        }

        /// <summary>
        /// Determines whether the current instance is equal to another <see cref="C_Id{TId}"/> object.
        /// <para><i>[ES] Determina si la instancia actual es igual a otro objeto <see cref="C_Id{TId}"/>.</i></para>
        /// </summary>
        /// <param name="other">The object to compare with this instance.</param>
        /// <returns>
        /// <see langword="true"/> if both instances have the same identifier value; otherwise, <see langword="false"/>.
        /// <para><i>[ES] <see langword="true"/> si ambas instancias tienen el mismo valor de identificador; de lo contrario, <see langword="false"/>.</i></para>
        /// </returns>
        /// <example>
        /// <code>
        /// var id1 = new C_Id&lt;string&gt;("USR-1024");
        /// var id2 = new C_Id&lt;string&gt;("USR-1024");
        /// var id3 = new C_Id&lt;string&gt;("USR-1025");
        /// bool areEqual1 = id1.Equals(id2); // true
        /// bool areEqual2 = id1.Equals(id3); // false
        /// </code>
        /// </example>
        public bool Equals(C_Id<TId>? other) => this.Equals((I_Id<TId>?)other);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// <para><i>[ES] Determina si el objeto especificado es igual al objeto actual.</i></para>
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        /// <para><i>[ES] <see langword="true"/> si el objeto especificado es igual al objeto actual; de lo contrario, <see langword="false"/>.</i></para>
        /// </returns>
        public override bool Equals(object? obj) => this.Equals(obj as C_Id<TId>);

        /// <summary>
        /// Serves as the default hash function for the unique identifier.
        /// <para><i>[ES] Sirve como la función hash predeterminada para el identificador único.</i></para>
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// <para><i>[ES] Un código hash para el objeto actual.</i></para>
        /// </returns>
        public override int GetHashCode() => this.Id is null ? 0 : this.Id.GetHashCode();

        /// <summary>
        /// Returns a string that represents the current identifier.
        /// <para><i>[ES] Devuelve una cadena que representa el identificador actual.</i></para>
        /// </summary>
        /// <returns>
        /// A string representation of the identifier value, or an empty string if <see cref="Id"/> is <see langword="null"/>.
        /// <para><i>[ES] Una representación en cadena del valor del identificador, o una cadena vacía si <see cref="Id"/> es <see langword="null"/>.</i></para>
        /// </returns>
        public override string ToString() => this.Id?.ToString() ?? string.Empty;

        /// <summary>
        /// Compares two <see cref="C_Id{TId}"/> instances for equality.
        /// <para><i>[ES] Compara dos instancias de <see cref="C_Id{TId}"/> para verificar su igualdad.</i></para>
        /// </summary>
        public static bool operator ==(C_Id<TId>? left, C_Id<TId>? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="C_Id{TId}"/> instances for inequality.
        /// <para><i>[ES] Compara dos instancias de <see cref="C_Id{TId}"/> para verificar su desigualdad.</i></para>
        /// </summary>
        public static bool operator !=(C_Id<TId>? left, C_Id<TId>? right) => !(left == right);

        /// <summary>
        /// Implicitly converts a <see cref="C_Id{TId}"/> instance to its underlying <typeparamref name="TId"/> value.
        /// <para><i>[ES] Convierte implícitamente una instancia de <see cref="C_Id{TId}"/> a su valor <typeparamref name="TId"/> subyacente.</i></para>
        /// </summary>
        public static implicit operator TId?(C_Id<TId>? idContainer)
        {
            return idContainer is null ? default : idContainer.Id;
        }
    }

    /// <summary>
    /// Specialized class for <see cref="Guid"/>-based object identification.
    /// <para>
    /// Uses only <see cref="Guid"/> (not <c>Guid?</c>). <see cref="Guid.Empty"/> means not initialized.
    /// </para>
    /// <para><i>[ES] Clase especializada para la identificación de objetos basada en <see cref="Guid"/>.
    /// Usa solo <see cref="Guid"/> (no <c>Guid?</c>). <see cref="Guid.Empty"/> significa no inicializado.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// C_IdGuid myId1 = new();
    /// C_IdGuid myId2 = new(Guid.NewGuid());
    /// C_IdGuid empty = new(Guid.Empty);
    /// Guid raw = myId2;
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
