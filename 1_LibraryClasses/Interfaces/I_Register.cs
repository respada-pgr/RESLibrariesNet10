namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for domain register entities: identity, audit timestamps, and soft delete.
    /// <para>
    /// Extends <see cref="I_Entity{TId}"/>, <see cref="I_Auditable"/>, and <see cref="I_SoftDeletable"/>.
    /// All timestamps should use UTC.
    /// </para>
    /// <para><i>[ES] Define un contrato para entidades de registro de dominio: identidad, auditoría y borrado lógico.
    /// Extiende <see cref="I_Entity{TId}"/>, <see cref="I_Auditable"/> e <see cref="I_SoftDeletable"/>.
    /// Todas las marcas de tiempo deberían usar UTC.</i></para>
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
    ///     public DateTime? UpdateDate { get; private set; }
    ///     public bool IsDeleted { get; private set; }
    ///     public DateTime? DeleteDate { get; private set; }
    /// }
    /// </code>
    /// </example>
    public interface I_Register<TId> : I_Entity<TId>, I_Auditable, I_SoftDeletable
    {
    }
}
