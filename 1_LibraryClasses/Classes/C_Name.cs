using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes;

/// <summary>
/// Represents a value object base class for handling logical or display names.
/// <para><i>[ES] Representa una clase base de objeto de valor para el manejo de nombres lógicos o de visualización.</i></para>
/// </summary>
/// <example>
/// Basic creation and implicit conversion examples:
/// <code>
/// // [EN] Direct creation with validation
/// // [ES] Creación directa con validación
/// C_Name name1 = new("Laptop Gaming");
/// 
/// // [EN] Implicit conversion string -> C_Name
/// // [ES] Conversión implícita string -> C_Name
/// C_Name name2 = "Teclado Mecánico";
/// 
/// // [EN] Implicit conversion C_Name -> string
/// // [ES] Conversión implícita C_Name -> string
/// string rawString = name1; 
/// 
/// // [EN] Direct equality comparison
/// // [ES] Comparación directa de igualdad
/// bool isEqual = (name1 == new C_Name("Laptop Gaming")); // true
/// </code>
/// </example>
public class C_Name : I_Named, IEquatable<C_Name>
{
    /// <summary>
    /// Gets the normalized name value.
    /// <para><i>[ES] Obtiene el valor del nombre normalizado.</i></para>
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="C_Name"/> class with a specified name.
    /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Name"/> con un nombre especificado.</i></para>
    /// </summary>
    /// <param name="name">
    /// The name text. Cannot be <see langword="null"/>, empty, or consist only of white-space characters.
    /// <para><i>[ES] El texto del nombre. No puede ser <see langword="null"/>, estar vacío ni contener solo espacios en blanco.</i></para>
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is <see langword="null"/>, empty, or white-space.
    /// <para><i>[ES] Se lanza cuando <paramref name="name"/> es <see langword="null"/>, está vacío o solo contiene espacios en blanco.</i></para>
    /// </exception>
    /// <example>
    /// <code>
    /// C_Name categoryName = new("Electrónica");
    /// </code>
    /// </example>
    public C_Name(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name.Trim();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="C_Name"/> class by copying another instance.
    /// <para><i>[ES] Inicializa una nueva instancia de la clase <see cref="C_Name"/> copiando otra instancia.</i></para>
    /// </summary>
    /// <param name="source">
    /// The source instance to copy from.
    /// <para><i>[ES] La instancia de origen desde la cual copiar.</i></para>
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="source"/> is <see langword="null"/>.
    /// <para><i>[ES] Se lanza cuando <paramref name="source"/> es <see langword="null"/>.</i></para>
    /// </exception>
    /// <example>
    /// <code>
    /// C_Name original = new("Producto A");
    /// C_Name copy = new(original);
    /// </code>
    /// </example>
    public C_Name(C_Name source)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentException.ThrowIfNullOrWhiteSpace(source.Name);
        Name = source.Name;
    }

    /// <summary>
    /// Determines whether the current name is equal to another <see cref="C_Name"/> instance using ordinal case-insensitive comparison by default.
    /// <para><i>[ES] Determina si el nombre actual es igual a otra instancia de <see cref="C_Name"/> usando comparación ordinal sin distinción de mayúsculas por defecto.</i></para>
    /// </summary>
    /// <param name="other">The object to compare with this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the names match; otherwise, <see langword="false"/>.
    /// <para><i>[ES] <see langword="true"/> si los nombres coinciden; de lo contrario, <see langword="false"/>.</i></para>
    /// </returns>
    public bool Equals(C_Name? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// <para><i>[ES] Determina si el objeto especificado es igual al objeto actual.</i></para>
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as C_Name);

    /// <summary>
    /// Serves as the default hash function for the normalized name.
    /// <para><i>[ES] Sirve como la función hash predeterminada para el nombre normalizado.</i></para>
    /// </summary>
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Name);

    /// <summary>
    /// Returns the string representation of the name.
    /// <para><i>[ES] Devuelve la representación en texto del nombre.</i></para>
    /// </summary>
    public override string ToString() => Name;

    /// <summary>
    /// Compares two <see cref="C_Name"/> instances for equality.
    /// <para><i>[ES] Compara dos instancias de <see cref="C_Name"/> para verificar su igualdad.</i></para>
    /// </summary>
    public static bool operator ==(C_Name? left, C_Name? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="C_Name"/> instances for inequality.
    /// <para><i>[ES] Compara dos instancias de <see cref="C_Name"/> para verificar su desigualdad.</i></para>
    /// </summary>
    public static bool operator !=(C_Name? left, C_Name? right) => !(left == right);

    /// <summary>
    /// Implicitly converts a <see cref="C_Name"/> instance to a <see cref="string"/>.
    /// <para><i>[ES] Convierte implícitamente una instancia de <see cref="C_Name"/> a un <see cref="string"/>.</i></para>
    /// </summary>
    /// <param name="nameContainer">The name container instance.</param>
    public static implicit operator string(C_Name nameContainer)
    {
        ArgumentNullException.ThrowIfNull(nameContainer);
        return nameContainer.Name;
    }

    /// <summary>
    /// Implicitly converts a <see cref="string"/> to a new <see cref="C_Name"/> instance.
    /// <para><i>[ES] Convierte implícitamente un <see cref="string"/> a una nueva instancia de <see cref="C_Name"/>.</i></para>
    /// </summary>
    /// <param name="name">The raw string name.</param>
    public static implicit operator C_Name(string name) => new(name);
}