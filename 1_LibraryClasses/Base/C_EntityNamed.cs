using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Base
{
    /// <summary>
    /// Abstract base for domain entities that expose a display or logical name.
    /// <para>
    /// Extends <see cref="C_Entity{TId}"/> and implements <see cref="I_Named"/>.
    /// </para>
    /// <para><i>[ES] Base abstracta para entidades de dominio que exponen un nombre lógico o de visualización.
    /// Extiende <see cref="C_Entity{TId}"/> e implementa <see cref="I_Named"/>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier.
    /// <para><i>[ES] El tipo subyacente del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_Category : C_EntityNamed&lt;string&gt;
    /// {
    ///     public C_Category(string id, string name) : base(id, name) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_EntityNamed<TId> : C_Entity<TId>, I_Entity<TId>, I_Named
    {
        /// <summary>
        /// Gets or sets the name of the entity.
        /// <para><i>[ES] Obtiene o establece el nombre de la entidad.</i></para>
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance with an unassigned identifier and empty name.
        /// <para><i>[ES] Inicializa una nueva instancia sin identificador y nombre vacío.</i></para>
        /// </summary>
        protected C_EntityNamed() : base()
        {
            this.Name = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance with a specific identifier and optional name.
        /// <para><i>[ES] Inicializa una nueva instancia con identificador y nombre opcional.</i></para>
        /// </summary>
        protected C_EntityNamed(TId? id, string? name = null) : base(id)
        {
            this.Name = name ?? string.Empty;
        }

        /// <summary>
        /// Copy constructor from an identifiable source; name starts empty unless source is named.
        /// <para><i>[ES] Constructor de copia desde una fuente identificable; el nombre queda vacío salvo si la fuente es nombrada.</i></para>
        /// </summary>
        protected C_EntityNamed(I_Identifiable<TId>? source) : base(source)
        {
            this.Name = source is I_Named named ? named.Name : string.Empty;
        }
    }

    /// <summary>
    /// Named entity specialized for <see cref="Guid"/> identity.
    /// <para><i>[ES] Entidad nombrada especializada con identidad <see cref="Guid"/>.</i></para>
    /// </summary>
    public abstract class C_EntityNamedGuid : C_EntityNamed<Guid>
    {
        /// <inheritdoc cref="C_EntityGuid.Id"/>
        public new Guid Id
        {
            get
            {
                Guid? value = base.Id;
                return value.HasValue ? value.Value : Guid.Empty;
            }
            protected set => base.Id = value;
        }

        /// <inheritdoc cref="C_EntityGuid.IsInitialized"/>
        public new bool IsInitialized => this.Id != Guid.Empty;

        /// <summary>
        /// Initializes with a newly generated <see cref="Guid"/> and optional name.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo y nombre opcional.</i></para>
        /// </summary>
        protected C_EntityNamedGuid(string? name = null) : base(Guid.NewGuid(), name) { }

        /// <summary>
        /// Initializes with a specified <see cref="Guid"/> and optional name.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> concreto y nombre opcional.</i></para>
        /// </summary>
        protected C_EntityNamedGuid(Guid id, string? name = null) : base(id, name) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{Guid}"/> source.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{Guid}"/>.</i></para>
        /// </summary>
        protected C_EntityNamedGuid(I_Identifiable<Guid>? source)
            : base(source?.Id is Guid id && id != Guid.Empty ? id : Guid.Empty)
        {
            if (source is I_Named named)
                this.Name = named.Name;
        }
    }

    /// <summary>
    /// Named entity specialized for <see cref="int"/> identity. <c>0</c> means not initialized.
    /// <para><i>[ES] Entidad nombrada con identidad <see cref="int"/>. <c>0</c> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_EntityNamedInt : C_EntityNamed<int>
    {
        public new int Id
        {
            get
            {
                int? value = base.Id;
                return value.HasValue ? value.Value : 0;
            }
            protected set => base.Id = value;
        }

        public new bool IsInitialized => this.Id != 0;

        protected C_EntityNamedInt(string? name = null) : base(0, name) { }

        protected C_EntityNamedInt(int id, string? name = null) : base(id, name) { }

        protected C_EntityNamedInt(I_Identifiable<int>? source)
            : base(source?.Id is int id && id != 0 ? id : 0)
        {
            if (source is I_Named named)
                this.Name = named.Name;
        }
    }

    /// <summary>
    /// Named entity specialized for <see cref="long"/> identity. <c>0L</c> means not initialized.
    /// <para><i>[ES] Entidad nombrada con identidad <see cref="long"/>. <c>0L</c> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_EntityNamedLong : C_EntityNamed<long>
    {
        public new long Id
        {
            get
            {
                long? value = base.Id;
                return value.HasValue ? value.Value : 0L;
            }
            protected set => base.Id = value;
        }

        public new bool IsInitialized => this.Id != 0L;

        protected C_EntityNamedLong(string? name = null) : base(0L, name) { }

        protected C_EntityNamedLong(long id, string? name = null) : base(id, name) { }

        protected C_EntityNamedLong(I_Identifiable<long>? source)
            : base(source?.Id is long id && id != 0L ? id : 0L)
        {
            if (source is I_Named named)
                this.Name = named.Name;
        }
    }

    /// <summary>
    /// Named entity specialized for <see cref="string"/> identity. <see cref="string.Empty"/> means not initialized.
    /// <para><i>[ES] Entidad nombrada con identidad <see cref="string"/>. <see cref="string.Empty"/> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_EntityNamedString : C_EntityNamed<string>
    {
        public new string Id
        {
            get => base.Id ?? string.Empty;
            protected set => base.Id = value ?? string.Empty;
        }

        public new bool IsInitialized => !string.IsNullOrEmpty(this.Id);

        protected C_EntityNamedString() : base(string.Empty, string.Empty) { }

        protected C_EntityNamedString(string? id, string? name = null) : base(id ?? string.Empty, name) { }

        protected C_EntityNamedString(I_Identifiable<string>? source)
            : base(source is null ? string.Empty : (source.Id ?? string.Empty))
        {
            if (source is I_Named named)
                this.Name = named.Name;
        }
    }
}
