namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that possess a unique identifier.
    /// <para>
    /// This interface is intended for entities, value objects, or any type that must be uniquely identifiable
    /// within a domain or persistence context.
    /// At the interface level, <see cref="Id"/> is <typeparamref name="TId"/>?
    /// (nullable). Concrete specializations such as <c>C_IdGuid</c> / <c>C_EntityGuid</c> may expose
    /// a non-nullable <see cref="Guid"/> and treat <see cref="Guid.Empty"/> as not initialized.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que poseen un identificador único.
    /// Está pensada para entidades, value objects o cualquier tipo que deba ser identificable
    /// de forma única dentro de un dominio o contexto de persistencia.
    /// A nivel de interfaz, <see cref="Id"/> es <typeparamref name="TId"/>?
    /// (anulable). Las especializaciones concretas como <c>C_IdGuid</c> / <c>C_EntityGuid</c> pueden exponer
    /// un <see cref="Guid"/> no anulable y tratar <see cref="Guid.Empty"/> como no inicializado.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The type of the unique identifier.
    /// <para><i>[ES] El tipo del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// // Via interface (Id is Guid?)
    /// I_Identifiable&lt;Guid&gt; item = C_IdGuid.New();
    /// Guid? raw = item.Id;
    ///
    /// // Via specialization (Id is Guid; Empty = not initialized)
    /// C_IdGuid id = new(Guid.Empty);
    /// bool ok = id.IsInitialized; // false
    /// </code>
    /// </example>
    public interface I_Identifiable<TId> : I_Initialized
    {
        /// <summary>
        /// Gets the unique identifier of the object.
        /// <para>
        /// When not initialized, the value may be <see langword="null"/> (“not assigned”) or
        /// the default value of <typeparamref name="TId"/> (for example <see cref="Guid.Empty"/> — “empty sentinel”).
        /// Both are not initialized, but they are not the same information.
        /// Use <see cref="I_Initialized.IsInitialized"/> to determine whether a valid identity is present.
        /// </para>
        /// <para><i>[ES] Si no está inicializado, el valor puede ser <see langword="null"/> (“no asignado”) o
        /// el valor por defecto de <typeparamref name="TId"/> (por ejemplo <see cref="Guid.Empty"/> — “sentinela vacío”).
        /// Ambos no están inicializados, pero no aportan la misma información.
        /// Use <see cref="I_Initialized.IsInitialized"/> para saber si hay una identidad válida.</i></para>
        /// </summary>
        /// <value>
        /// The unique identifier. <see langword="null"/> and <c>default</c> are both not initialized, with different meaning.
        /// <para><i>[ES] El identificador único. <see langword="null"/> y <c>default</c> no están inicializados, con distinto significado.</i></para>
        /// </value>
        TId? Id { get; }
    }
}
