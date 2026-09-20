namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that track creation and last-update timestamps.
    /// <para>
    /// Implementations should use UTC. <see cref="UpdateDate"/> is <see langword="null"/> until the first update.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que rastrean fechas de creación y última modificación.
    /// Las implementaciones deberían usar UTC. <see cref="UpdateDate"/> es <see langword="null"/> hasta la primera actualización.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Document : I_Auditable
    /// {
    ///     public DateTime CreateDate { get; private set; } = DateTime.UtcNow;
    ///     public DateTime? UpdateDate { get; private set; }
    /// }
    /// </code>
    /// </example>
    public interface I_Auditable
    {
        /// <summary>
        /// Gets the UTC date and time when the object was created.
        /// <para><i>[ES] Obtiene la fecha y hora UTC de creación del objeto.</i></para>
        /// </summary>
        DateTime CreateDate { get; }

        /// <summary>
        /// Gets the UTC date and time of the last update, or <see langword="null"/> if never updated.
        /// <para><i>[ES] Obtiene la fecha y hora UTC de la última actualización, o <see langword="null"/> si nunca se actualizó.</i></para>
        /// </summary>
        DateTime? UpdateDate { get; }
    }
}
