namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for domain entities that record a creation timestamp.
    /// <para>
    /// Extends <see cref="I_Entity{TId}"/> with <see cref="CreateDate"/>.
    /// Implementations should use UTC (e.g. <see cref="DateTime.UtcNow"/>).
    /// </para>
    /// <para><i>[ES] Define un contrato para entidades de dominio que registran fecha de creación.
    /// Extiende <see cref="I_Entity{TId}"/> con <see cref="CreateDate"/>.
    /// Las implementaciones deberían usar UTC (p. ej. <see cref="DateTime.UtcNow"/>).</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The type of the unique identifier.
    /// <para><i>[ES] El tipo del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_Order : I_Register&lt;string&gt;
    /// {
    ///     public string? Id { get; private set; }
    ///     public bool IsInitialized =&gt; !string.IsNullOrEmpty(Id);
    ///     public DateTime CreateDate { get; private set; }
    /// }
    /// </code>
    /// </example>
    public interface I_Register<TId> : I_Entity<TId>
    {
        /// <summary>
        /// Gets the UTC date and time when the entity was created.
        /// <para><i>[ES] Obtiene la fecha y hora UTC de creación de la entidad.</i></para>
        /// </summary>
        /// <value>
        /// The creation timestamp in UTC.
        /// <para><i>[ES] La marca de tiempo de creación en UTC.</i></para>
        /// </value>
        DateTime CreateDate { get; }
    }
}
