namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for domain entities with a generic identifier.
    /// <para>
    /// Extends <see cref="I_Identifiable{TId}"/>. Use this interface to mark types that represent
    /// domain entities (as opposed to identity value objects such as <c>C_IdGuid</c>).
    /// Concrete specializations include <c>C_EntityGuid</c>, <c>C_EntityInt</c>, <c>C_EntityLong</c>, and <c>C_EntityString</c>.
    /// </para>
    /// <para><i>[ES] Define un contrato para entidades de dominio con un identificador genérico.
    /// Extiende <see cref="I_Identifiable{TId}"/>. Use esta interfaz para marcar tipos que representan
    /// entidades de dominio (a diferencia de value objects de identidad como <c>C_IdGuid</c>).
    /// Las especializaciones concretas incluyen <c>C_EntityGuid</c>, <c>C_EntityInt</c>, <c>C_EntityLong</c> y <c>C_EntityString</c>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The type of the unique identifier.
    /// <para><i>[ES] El tipo del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_Order : I_Entity&lt;string&gt;
    /// {
    ///     public string? Id { get; private set; }
    ///
    ///     public bool IsInitialized =&gt; !string.IsNullOrEmpty(Id);
    /// }
    /// </code>
    /// </example>
    public interface I_Entity<TId> : I_Identifiable<TId>
    {
    }
}
