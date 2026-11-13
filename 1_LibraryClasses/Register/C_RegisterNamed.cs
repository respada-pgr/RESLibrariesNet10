using _1_LibraryClasses;

namespace _1_LibraryClasses.Register
{
    /// <summary>
    /// Abstract base for named register entities (identity, audit, soft-delete, and name).
    /// <para>
    /// Extends <see cref="C_Register{TId}"/> and implements <see cref="I_Register{TId}"/> via base; exposes a Name property.
    /// </para>
    /// <para><i>[ES] Base abstracta para entidades de registro con nombre (identidad, auditoría, borrado lógico y nombre).
    /// Extiende <see cref="C_Register{TId}"/> e implementa <see cref="Name"/>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier.
    /// <para><i>[ES] El tipo subyacente del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_Customer : C_RegisterNamedGuid
    /// {
    ///     public C_Customer(string name) : base(name) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_RegisterNamed<TId> : C_Register<TId>, I_Register<TId>
    {
        /// <summary>
        /// Gets or sets the name of the register entity.
        /// <para><i>[ES] Obtiene o establece el nombre de la entidad de registro.</i></para>
        /// </summary>

#region PUBLIC PROPERTIES

        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance with empty name and <see cref="C_Register{TId}.CreateDate"/> = UTC now.
        /// <para><i>[ES] Inicializa una nueva instancia con nombre vacío y <see cref="C_Register{TId}.CreateDate"/> = UTC ahora.</i></para>
        /// </summary>
#endregion
#region CONSTRUCTORS

        protected C_RegisterNamed() : base()
        {
            this.Name = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance with identifier and optional name.
        /// <para><i>[ES] Inicializa una nueva instancia con identificador y nombre opcional.</i></para>
        /// </summary>
        protected C_RegisterNamed(TId? id, string? name = null) : base(id)
        {
            this.Name = name ?? string.Empty;
        }

        /// <summary>
        /// Copy constructor from an identifiable source.
        /// <para><i>[ES] Constructor de copia desde una fuente identificable.</i></para>
        /// </summary>
        protected C_RegisterNamed(I_Identifiable<TId>? source) : base(source)
        {
            this.Name = source is C_RegisterNamed<TId> nreg && !string.IsNullOrWhiteSpace(nreg.Name)
                ? nreg.Name
                : source is C_EntityNamed<TId> nent && !string.IsNullOrWhiteSpace(nent.Name)
                    ? nent.Name
                    : string.Empty;
        }

        /// <summary>
        /// Copy constructor from the same type. CreateDate = UTC now (base).
        /// <para><i>[ES] Constructor de copia del mismo tipo. CreateDate = UTC ahora.</i></para>
        /// </summary>
        protected C_RegisterNamed(C_RegisterNamed<TId>? source) : base(source)
        {
            this.Name = source?.Name ?? string.Empty;
        }

        /// <summary>
        /// Rehydration constructor (persistence). Does not overwrite timestamps with "now".
        /// <para><i>[ES] Constructor de rehidratación (persistencia). No pisa las fechas con "ahora".</i></para>
        /// </summary>
        protected C_RegisterNamed(
            TId? id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null,
            string? name = null)
            : base(id, createDate, updateDate, deleteDate)
        {
            this.Name = name ?? string.Empty;
        }

        /// <summary>
        /// Copy constructor from another register; name empty unless source is named.
        /// <para><i>[ES] Constructor de copia desde otro registro; nombre vacío salvo si la fuente es nombrada.</i></para>
        /// </summary>
        protected C_RegisterNamed(I_Register<TId>? source) : base(source)
        {
            this.Name = source is C_RegisterNamed<TId> nreg && !string.IsNullOrWhiteSpace(nreg.Name)
                ? nreg.Name
                : source is C_EntityNamed<TId> nent && !string.IsNullOrWhiteSpace(nent.Name)
                    ? nent.Name
                    : string.Empty;
        }
#endregion
}

    /// <summary>
    /// Named register specialized for <see cref="Guid"/> identity.
    /// <para><i>[ES] Registro nombrado especializado con identidad <see cref="Guid"/>.</i></para>
    /// </summary>
    public abstract class C_RegisterNamedGuid : C_RegisterNamed<Guid>
    {
#region PUBLIC PROPERTIES

        public new Guid Id
        {
            get
            {
                Guid? value = base.Id;
                return value.HasValue ? value.Value : Guid.Empty;
            }

            protected set => base.Id = value;
        }

        public new bool IsInitialized => this.Id != Guid.Empty;
#endregion
#region CONSTRUCTORS

        protected C_RegisterNamedGuid(string? name = null) : base(Guid.NewGuid(), name) { }

        protected C_RegisterNamedGuid(Guid id, string? name = null) : base(id, name) { }

        protected C_RegisterNamedGuid(I_Identifiable<Guid>? source) : base(source) { }

        protected C_RegisterNamedGuid(
            Guid id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null,
            string? name = null)
            : base(id, createDate, updateDate, deleteDate, name) { }

        protected C_RegisterNamedGuid(I_Register<Guid>? source) : base(source) { }

        protected C_RegisterNamedGuid(C_RegisterNamedGuid? source) : base(source) { }
#endregion
}

    /// <summary>
    /// Named register specialized for <see cref="int"/> identity. <c>0</c> means not initialized.
    /// <para><i>[ES] Registro nombrado con identidad <see cref="int"/>. <c>0</c> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_RegisterNamedInt : C_RegisterNamed<int>
    {
#region PUBLIC PROPERTIES

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
#endregion
#region CONSTRUCTORS

        protected C_RegisterNamedInt(string? name = null) : base(0, name) { }

        protected C_RegisterNamedInt(int id, string? name = null) : base(id, name) { }

        protected C_RegisterNamedInt(I_Identifiable<int>? source) : base(source) { }

        protected C_RegisterNamedInt(
            int id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null,
            string? name = null)
            : base(id, createDate, updateDate, deleteDate, name) { }

        protected C_RegisterNamedInt(I_Register<int>? source) : base(source) { }

        protected C_RegisterNamedInt(C_RegisterNamedInt? source) : base(source) { }
#endregion
}

    /// <summary>
    /// Named register specialized for <see cref="long"/> identity. <c>0L</c> means not initialized.
    /// <para><i>[ES] Registro nombrado con identidad <see cref="long"/>. <c>0L</c> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_RegisterNamedLong : C_RegisterNamed<long>
    {
#region PUBLIC PROPERTIES

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
#endregion
#region CONSTRUCTORS

        protected C_RegisterNamedLong(string? name = null) : base(0L, name) { }

        protected C_RegisterNamedLong(long id, string? name = null) : base(id, name) { }

        protected C_RegisterNamedLong(I_Identifiable<long>? source) : base(source) { }

        protected C_RegisterNamedLong(
            long id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null,
            string? name = null)
            : base(id, createDate, updateDate, deleteDate, name) { }

        protected C_RegisterNamedLong(I_Register<long>? source) : base(source) { }

        protected C_RegisterNamedLong(C_RegisterNamedLong? source) : base(source) { }
#endregion
}

    /// <summary>
    /// Named register specialized for <see cref="string"/> identity. <see cref="string.Empty"/> means not initialized.
    /// <para><i>[ES] Registro nombrado con identidad <see cref="string"/>. <see cref="string.Empty"/> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_RegisterNamedString : C_RegisterNamed<string>
    {
#region PUBLIC PROPERTIES

        public new string Id
        {
            get => base.Id ?? string.Empty;

            protected set => base.Id = value ?? string.Empty;
        }

        public new bool IsInitialized => !string.IsNullOrEmpty(this.Id);

        /// <summary>
        /// Initializes as not initialized (empty id) with optional name.
        /// <para><i>[ES] Inicializa sin id (vacío) y nombre opcional.</i></para>
        /// </summary>
#endregion
#region CONSTRUCTORS

        protected C_RegisterNamedString() : base(string.Empty, string.Empty) { }

        /// <summary>
        /// Initializes with identifier and optional name.
        /// To set only a name without id, use <c>new(..., name: "...")</c> or pass <see cref="string.Empty"/> as id.
        /// <para><i>[ES] Inicializa con identificador y nombre opcional.</i></para>
        /// </summary>
        protected C_RegisterNamedString(string? id, string? name = null) : base(id ?? string.Empty, name) { }

        protected C_RegisterNamedString(I_Identifiable<string>? source) : base(source) { }

        protected C_RegisterNamedString(
            string? id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null,
            string? name = null)
            : base(id ?? string.Empty, createDate, updateDate, deleteDate, name) { }

        protected C_RegisterNamedString(I_Register<string>? source) : base(source) { }

        protected C_RegisterNamedString(C_RegisterNamedString? source) : base(source) { }
#endregion
}
}
