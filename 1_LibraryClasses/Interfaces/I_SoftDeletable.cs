namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that support logical (soft) deletion.
    /// <para>
    /// A soft-deleted object remains stored but is marked as deleted.
    /// <see cref="IsDeleted"/> is typically equivalent to <c>DeleteDate is not null</c>.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que soportan borrado lógico.
    /// El objeto sigue almacenado pero marcado como eliminado.
    /// <see cref="IsDeleted"/> suele equivaler a <c>DeleteDate is not null</c>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Item : I_SoftDeletable
    /// {
    ///     public bool IsDeleted { get; private set; }
    ///     public DateTime? DeleteDate { get; private set; }
    ///
    ///     public void SoftDelete()
    ///     {
    ///         IsDeleted = true;
    ///         DeleteDate = DateTime.UtcNow;
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface I_SoftDeletable
    {
        /// <summary>
        /// Gets a value indicating whether the object has been soft-deleted.
        /// <para><i>[ES] Obtiene un valor que indica si el objeto ha sido eliminado de forma lógica.</i></para>
        /// </summary>
        bool IsDeleted { get; }

        /// <summary>
        /// Gets the UTC date and time when the object was soft-deleted, or <see langword="null"/> if not deleted.
        /// <para><i>[ES] Obtiene la fecha y hora UTC del borrado lógico, o <see langword="null"/> si no está eliminado.</i></para>
        /// </summary>
        DateTime? DeleteDate { get; }
    }
}
