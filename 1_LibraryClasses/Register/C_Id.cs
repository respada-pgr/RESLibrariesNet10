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
}
