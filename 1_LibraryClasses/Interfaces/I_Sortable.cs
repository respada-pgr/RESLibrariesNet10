namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that expose a sort order for display or sequencing.
    /// <para>
    /// Lower values typically appear first. Use <c>0</c> as a default when no explicit order is assigned.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que exponen un orden de clasificación para visualización o secuencia.
    /// Los valores más bajos suelen aparecer primero. Use <c>0</c> como valor por defecto cuando no haya un orden explícito.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_MenuItem : I_Sortable
    /// {
    ///     public int SortOrder { get; set; }
    /// }
    ///
    /// var items = list.OrderBy(x => x.SortOrder);
    /// </code>
    /// </example>
    public interface I_Sortable
    {
        /// <summary>
        /// Gets or sets the sort order used for display or sequencing.
        /// <para>
        /// Lower values typically come first. Default is often <c>0</c>.
        /// </para>
        /// <para><i>[ES] Obtiene o establece el orden de clasificación usado para visualización o secuencia.
        /// Los valores más bajos suelen ir primero. El valor por defecto suele ser <c>0</c>.</i></para>
        /// </summary>
        /// <value>
        /// The sort order value.
        /// <para><i>[ES] El valor del orden de clasificación.</i></para>
        /// </value>
        int SortOrder { get; set; }
    }
}
