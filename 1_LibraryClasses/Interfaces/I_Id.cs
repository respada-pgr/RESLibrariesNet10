namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for identifier containers that extend <see cref="I_Identifiable{TId}"/>
    /// and support value-based equality.
    /// <para>
    /// This interface is intended for Value Objects that wrap a unique identifier and need
    /// to be compared by their identity value rather than by reference.
    /// Concrete specializations include <c>C_IdGuid</c>, <c>C_IdInt</c>, <c>C_IdLong</c>, and <c>C_IdString</c>.
    /// </para>
    /// <para><i>[ES] Define un contrato para contenedores de identificador que extienden
    /// <see cref="I_Identifiable{TId}"/> y soportan igualdad basada en valor.
    /// Está pensada para Value Objects que encapsulan un identificador único y deben
    /// compararse por su valor de identidad en lugar de por referencia.
    /// Las especializaciones concretas incluyen <c>C_IdGuid</c>, <c>C_IdInt</c>, <c>C_IdLong</c> y <c>C_IdString</c>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier. Must implement <see cref="IEquatable{TId}"/>.
    /// <para><i>[ES] El tipo subyacente del identificador único. Debe implementar <see cref="IEquatable{TId}"/>.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// I_Id&lt;Guid&gt; id = C_IdGuid.New();
    /// if (id.IsInitialized)
    /// {
    ///     Guid? value = id.Id;
    /// }
    /// </code>
    /// </example>
    public interface I_Id<TId> : I_Identifiable<TId>, IEquatable<I_Id<TId>> where TId : IEquatable<TId>
    {
    }
}
