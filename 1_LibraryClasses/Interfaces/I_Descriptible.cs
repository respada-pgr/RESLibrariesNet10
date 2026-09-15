namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that expose a human-readable description.
    /// <para>
    /// This interface is intended for entities, value objects, or any type that provides
    /// explanatory or detail text beyond a short name.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que exponen una descripción legible.
    /// Está pensada para entidades, value objects o cualquier tipo que aporte un texto
    /// explicativo o de detalle más allá de un nombre corto.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Product : I_Descriptible
    /// {
    ///     public string Description { get; set; } = string.Empty;
    /// }
    ///
    /// I_Descriptible item = new C_Product { Description = "Wireless mouse" };
    /// Console.WriteLine(item.Description);
    /// </code>
    /// </example>
    public interface I_Descriptible
    {
        /// <summary>
        /// Gets or sets the description of the object.
        /// <para>
        /// Implementations should prefer a non-null value (e.g. <see cref="string.Empty"/> when unknown).
        /// </para>
        /// <para><i>[ES] Obtiene o establece la descripción del objeto.
        /// Las implementaciones deberían preferir un valor no nulo (p. ej. <see cref="string.Empty"/> si se desconoce).</i></para>
        /// </summary>
        /// <value>
        /// The description text of the object.
        /// <para><i>[ES] El texto de descripción del objeto.</i></para>
        /// </value>
        string Description { get; set; }
    }
}
