namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that have a display or logical name.
    /// <para>
    /// This interface is intended for entities, value objects, or any type that exposes a human-readable
    /// or logical name used for identification, display, or filtering purposes.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que poseen un nombre lógico o de visualización.
    /// Está pensada para entidades, value objects o cualquier tipo que exponga un nombre legible
    /// o lógico utilizado para identificación, visualización o filtrado.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Category : I_Named
    /// {
    ///     public string Name { get; init; } = string.Empty;
    /// }
    ///
    /// I_Named item = new C_Category { Name = "Electronics" };
    /// Console.WriteLine(item.Name); // Output: Electronics
    /// </code>
    /// </example>
    public interface I_Named
    {
        /// <summary>
        /// Gets the name of the object.
        /// <para>
        /// The name is typically a non-empty, human-readable string used for display or logical identification.
        /// Implementations should guarantee that the returned value is never <see langword="null"/>.
        /// </para>
        /// <para><i>[ES] Obtiene el nombre del objeto.
        /// El nombre suele ser una cadena no vacía y legible utilizada para visualización o identificación lógica.
        /// Las implementaciones deben garantizar que el valor devuelto nunca sea <see langword="null"/>.</i></para>
        /// </summary>
        /// <value>
        /// The name of the object. Never <see langword="null"/>.
        /// <para><i>[ES] El nombre del objeto. Nunca es <see langword="null"/>.</i></para>
        /// </value>
        /// <example>
        /// <code>
        /// I_Named namedObject = GetNamedObject();
        /// string displayName = namedObject.Name;
        /// </code>
        /// </example>
        string Name { get; }
    }
}
