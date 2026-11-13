using _1_LibraryClasses;

namespace _1_LibraryClasses.Register
{
    /// <summary>
    /// Domain register entity: identity, create/update timestamps, and soft delete.
    /// <para><i>[ES] Entidad de registro: identidad, fechas de creación/actualización y borrado lógico.</i></para>
    /// </summary>
    /// <typeparam name="TId">Identifier type. / Tipo del identificador.</typeparam>
    public interface I_Register<TId> : I_Entity<TId>
    {
        /// <summary>UTC creation timestamp. / Fecha de creación UTC.</summary>
        DateTime CreateDate { get; }

        /// <summary>UTC last update, or null if never updated. / Última actualización UTC, o null.</summary>
        DateTime? UpdateDate { get; }

        /// <summary>True if <see cref="UpdateDate"/> has been set. / True si hay fecha de actualización.</summary>
        bool IsUpdateDated { get; }

        /// <summary>UTC soft-delete timestamp, or null. / Fecha de borrado lógico UTC, o null.</summary>
        DateTime? DeleteDate { get; }

        /// <summary>True if soft-deleted. / True si está borrado lógicamente.</summary>
        bool IsDeleted { get; }
    }
}
