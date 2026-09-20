using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes
{
    /// <summary>
    /// Abstract base for domain register entities: identity, audit timestamps, and soft delete.
    /// <para>
    /// Extends <see cref="C_Entity{TId}"/> and implements <see cref="I_Register{TId}"/>
    /// (<see cref="I_Auditable"/> + <see cref="I_SoftDeletable"/>). Timestamps use UTC.
    /// </para>
    /// <para><i>[ES] Base abstracta para entidades de registro: identidad, auditoría y borrado lógico.
    /// Extiende <see cref="C_Entity{TId}"/> e implementa <see cref="I_Register{TId}"/>
    /// (<see cref="I_Auditable"/> + <see cref="I_SoftDeletable"/>). Las fechas usan UTC.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier.
    /// <para><i>[ES] El tipo subyacente del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_Order : C_Register&lt;string&gt;
    /// {
    ///     public C_Order(string id) : base(id) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_Register<TId> : C_Entity<TId>, I_Register<TId>
    {
        /// <summary>
        /// Gets the UTC date and time when the entity was created.
        /// <para><i>[ES] Obtiene la fecha y hora UTC de creación de la entidad.</i></para>
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        /// Gets the UTC date and time of the last update, or <see langword="null"/> if never updated.
        /// <para><i>[ES] Obtiene la fecha y hora UTC de la última actualización, o <see langword="null"/> si nunca se actualizó.</i></para>
        /// </summary>
        public DateTime? UpdateDate { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the entity has been soft-deleted.
        /// <para><i>[ES] Obtiene un valor que indica si la entidad ha sido eliminada de forma lógica.</i></para>
        /// </summary>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// Gets the UTC date and time when the entity was soft-deleted, or <see langword="null"/> if not deleted.
        /// <para><i>[ES] Obtiene la fecha y hora UTC del borrado lógico, o <see langword="null"/> si no está eliminada.</i></para>
        /// </summary>
        public DateTime? DeleteDate { get; protected set; }

        /// <summary>
        /// Initializes a new instance with an unassigned identifier and <see cref="CreateDate"/> = UTC now.
        /// <para><i>[ES] Inicializa una nueva instancia sin identificador y <see cref="CreateDate"/> = UTC ahora.</i></para>
        /// </summary>
        protected C_Register() : base()
        {
            this.CreateDate = DateTime.UtcNow;
            this.UpdateDate = null;
            this.IsDeleted = false;
            this.DeleteDate = null;
        }

        /// <summary>
        /// Initializes a new instance with a specific identifier and <see cref="CreateDate"/> = UTC now.
        /// <para><i>[ES] Inicializa una nueva instancia con identificador y <see cref="CreateDate"/> = UTC ahora.</i></para>
        /// </summary>
        protected C_Register(TId? id) : base(id)
        {
            this.CreateDate = DateTime.UtcNow;
            this.UpdateDate = null;
            this.IsDeleted = false;
            this.DeleteDate = null;
        }

        /// <summary>
        /// Copy constructor from an identifiable source; sets <see cref="CreateDate"/> = UTC now.
        /// <para><i>[ES] Constructor de copia desde una fuente identificable; asigna <see cref="CreateDate"/> = UTC ahora.</i></para>
        /// </summary>
        protected C_Register(I_Identifiable<TId>? source) : base(source)
        {
            this.CreateDate = DateTime.UtcNow;
            this.UpdateDate = null;
            this.IsDeleted = false;
            this.DeleteDate = null;
        }

        /// <summary>
        /// Rehydration constructor for loading from persistence (does not overwrite timestamps with "now").
        /// <para>
        /// <see cref="IsDeleted"/> is derived from <paramref name="deleteDate"/>.
        /// </para>
        /// <para><i>[ES] Constructor de rehidratación para cargar desde persistencia (no pisa las fechas con "ahora").
        /// <see cref="IsDeleted"/> se deriva de <paramref name="deleteDate"/>.</i></para>
        /// </summary>
        /// <param name="id">The identifier value.</param>
        /// <param name="createDate">UTC creation timestamp from storage.</param>
        /// <param name="updateDate">UTC last-update timestamp, or <see langword="null"/>.</param>
        /// <param name="deleteDate">UTC soft-delete timestamp, or <see langword="null"/> if not deleted.</param>
        protected C_Register(
            TId? id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null)
            : base(id)
        {
            this.CreateDate = createDate;
            this.UpdateDate = updateDate;
            this.DeleteDate = deleteDate;
            this.IsDeleted = deleteDate is not null;
        }

        /// <summary>
        /// Copy constructor from another register (identity + audit + soft-delete state).
        /// <para><i>[ES] Constructor de copia desde otro registro (identidad + auditoría + borrado lógico).</i></para>
        /// </summary>
        protected C_Register(I_Register<TId>? source) : base(source)
        {
            if (source is null)
            {
                this.CreateDate = DateTime.UtcNow;
                this.UpdateDate = null;
                this.IsDeleted = false;
                this.DeleteDate = null;
            }
            else
            {
                this.CreateDate = source.CreateDate;
                this.UpdateDate = source.UpdateDate;
                this.DeleteDate = source.DeleteDate;
                this.IsDeleted = source.IsDeleted;
            }
        }

        /// <summary>
        /// Marks the entity as updated (<see cref="UpdateDate"/> = UTC now).
        /// <para><i>[ES] Marca la entidad como actualizada (<see cref="UpdateDate"/> = UTC ahora).</i></para>
        /// </summary>
        public virtual void MarkUpdated()
        {
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Soft-deletes the entity. Sets <see cref="IsDeleted"/> and <see cref="DeleteDate"/>.
        /// <para><i>[ES] Elimina lógicamente la entidad. Asigna <see cref="IsDeleted"/> y <see cref="DeleteDate"/>.</i></para>
        /// </summary>
        public virtual void SoftDelete()
        {
            this.IsDeleted = true;
            this.DeleteDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Clears the soft-delete mark.
        /// <para><i>[ES] Quita la marca de borrado lógico.</i></para>
        /// </summary>
        public virtual void Restore()
        {
            this.IsDeleted = false;
            this.DeleteDate = null;
        }
    }

    /// <summary>
    /// Register entity specialized for <see cref="Guid"/> identity.
    /// <para><i>[ES] Entidad de registro especializada con identidad <see cref="Guid"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_User : C_RegisterGuid
    /// {
    ///     public C_User() : base() { }
    ///     public C_User(Guid id) : base(id) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_RegisterGuid : C_Register<Guid>
    {
        /// <summary>
        /// Gets the unique identifier as a non-nullable <see cref="Guid"/>.
        /// <para><see cref="Guid.Empty"/> means not initialized.</para>
        /// <para><i>[ES] Identificador como <see cref="Guid"/> no anulable. <see cref="Guid.Empty"/> = no inicializado.</i></para>
        /// </summary>
        public new Guid Id
        {
            get
            {
                Guid? value = base.Id;
                return value.HasValue ? value.Value : Guid.Empty;
            }
            protected set => base.Id = value;
        }

        /// <summary>
        /// Gets whether a valid (non-empty) identity is assigned.
        /// <para><i>[ES] Indica si hay identidad válida (no vacía).</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != Guid.Empty;

        /// <summary>
        /// Initializes with a newly generated <see cref="Guid"/> and UTC <see cref="C_Register{TId}.CreateDate"/>.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo y <see cref="C_Register{TId}.CreateDate"/> UTC.</i></para>
        /// </summary>
        protected C_RegisterGuid() : base(Guid.NewGuid()) { }

        /// <summary>
        /// Initializes with a specified <see cref="Guid"/>. <see cref="Guid.Empty"/> means not initialized.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> concreto. <see cref="Guid.Empty"/> = no inicializado.</i></para>
        /// </summary>
        protected C_RegisterGuid(Guid id) : base(id) { }

        /// <summary>
        /// Rehydration constructor for loading from persistence.
        /// <para><i>[ES] Constructor de rehidratación para cargar desde persistencia.</i></para>
        /// </summary>
        protected C_RegisterGuid(
            Guid id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null)
            : base(id, createDate, updateDate, deleteDate) { }

        /// <summary>
        /// Copy constructor from another <see cref="I_Register{TId}"/> of <see cref="Guid"/>.
        /// <para><i>[ES] Constructor de copia desde otro <see cref="I_Register{TId}"/> de <see cref="Guid"/>.</i></para>
        /// </summary>
        protected C_RegisterGuid(I_Register<Guid>? source) : base(source) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{Guid}"/> source.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{Guid}"/>.</i></para>
        /// </summary>
        protected C_RegisterGuid(I_Identifiable<Guid>? source)
            : base(source?.Id is Guid id && id != Guid.Empty ? id : Guid.Empty) { }

        /// <summary>
        /// Initializes with a newly generated <see cref="Guid"/>. Throws if already initialized.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo. Lanza si ya está inicializado.</i></para>
        /// </summary>
        public Guid Init()
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            var newId = Guid.NewGuid();
            this.Id = newId;
            return newId;
        }

        /// <summary>
        /// Initializes with the specified <see cref="Guid"/>. Throws if already initialized or empty.
        /// <para><i>[ES] Inicializa con el <see cref="Guid"/> indicado. Lanza si ya está inicializado o es vacío.</i></para>
        /// </summary>
        public Guid Init(Guid value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (value == Guid.Empty)
                throw new ArgumentException("Id cannot be Guid.Empty.", nameof(value));
            this.Id = value;
            return value;
        }
    }

    /// <summary>
    /// Register entity specialized for <see cref="int"/> identity. <c>0</c> means not initialized.
    /// <para><i>[ES] Entidad de registro con identidad <see cref="int"/>. <c>0</c> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_RegisterInt : C_Register<int>
    {
        /// <summary>
        /// Gets the unique identifier. <c>0</c> means not initialized.
        /// <para><i>[ES] Identificador único. <c>0</c> = no inicializado.</i></para>
        /// </summary>
        public new int Id
        {
            get
            {
                int? value = base.Id;
                return value.HasValue ? value.Value : 0;
            }
            protected set => base.Id = value;
        }

        /// <summary>
        /// Gets whether a valid identity is assigned.
        /// <para><i>[ES] Indica si hay identidad válida.</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != 0;

        /// <summary>
        /// Initializes as not initialized (<c>0</c>).
        /// <para><i>[ES] Inicializa como no inicializado (<c>0</c>).</i></para>
        /// </summary>
        protected C_RegisterInt() : base(0) { }

        /// <summary>
        /// Initializes with a specified identifier.
        /// <para><i>[ES] Inicializa con un identificador concreto.</i></para>
        /// </summary>
        protected C_RegisterInt(int id) : base(id) { }

        /// <summary>
        /// Rehydration constructor for loading from persistence.
        /// <para><i>[ES] Constructor de rehidratación para cargar desde persistencia.</i></para>
        /// </summary>
        protected C_RegisterInt(
            int id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null)
            : base(id, createDate, updateDate, deleteDate) { }

        /// <summary>
        /// Copy constructor from another <see cref="I_Register{TId}"/> of <see cref="int"/>.
        /// <para><i>[ES] Constructor de copia desde otro <see cref="I_Register{TId}"/> de <see cref="int"/>.</i></para>
        /// </summary>
        protected C_RegisterInt(I_Register<int>? source) : base(source) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{TId}"/> of <see cref="int"/>.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{TId}"/> de <see cref="int"/>.</i></para>
        /// </summary>
        protected C_RegisterInt(I_Identifiable<int>? source)
            : base(source?.Id is int id && id != 0 ? id : 0) { }

        /// <summary>
        /// Initializes with the specified value. Throws if already initialized or value is <c>0</c>.
        /// <para><i>[ES] Inicializa con el valor indicado. Lanza si ya está inicializado o el valor es <c>0</c>.</i></para>
        /// </summary>
        public int Init(int value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (value == 0)
                throw new ArgumentException("Id cannot be 0.", nameof(value));
            this.Id = value;
            return value;
        }
    }

    /// <summary>
    /// Register entity specialized for <see cref="long"/> identity. <c>0L</c> means not initialized.
    /// <para><i>[ES] Entidad de registro con identidad <see cref="long"/>. <c>0L</c> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_RegisterLong : C_Register<long>
    {
        /// <summary>
        /// Gets the unique identifier. <c>0L</c> means not initialized.
        /// <para><i>[ES] Identificador único. <c>0L</c> = no inicializado.</i></para>
        /// </summary>
        public new long Id
        {
            get
            {
                long? value = base.Id;
                return value.HasValue ? value.Value : 0L;
            }
            protected set => base.Id = value;
        }

        /// <summary>
        /// Gets whether a valid identity is assigned.
        /// <para><i>[ES] Indica si hay identidad válida.</i></para>
        /// </summary>
        public new bool IsInitialized => this.Id != 0L;

        /// <summary>
        /// Initializes as not initialized (<c>0L</c>).
        /// <para><i>[ES] Inicializa como no inicializado (<c>0L</c>).</i></para>
        /// </summary>
        protected C_RegisterLong() : base(0L) { }

        /// <summary>
        /// Initializes with a specified identifier.
        /// <para><i>[ES] Inicializa con un identificador concreto.</i></para>
        /// </summary>
        protected C_RegisterLong(long id) : base(id) { }

        /// <summary>
        /// Rehydration constructor for loading from persistence.
        /// <para><i>[ES] Constructor de rehidratación para cargar desde persistencia.</i></para>
        /// </summary>
        protected C_RegisterLong(
            long id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null)
            : base(id, createDate, updateDate, deleteDate) { }

        /// <summary>
        /// Copy constructor from another <see cref="I_Register{TId}"/> of <see cref="long"/>.
        /// <para><i>[ES] Constructor de copia desde otro <see cref="I_Register{TId}"/> de <see cref="long"/>.</i></para>
        /// </summary>
        protected C_RegisterLong(I_Register<long>? source) : base(source) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{TId}"/> of <see cref="long"/>.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{TId}"/> de <see cref="long"/>.</i></para>
        /// </summary>
        protected C_RegisterLong(I_Identifiable<long>? source)
            : base(source?.Id is long id && id != 0L ? id : 0L) { }

        /// <summary>
        /// Initializes with the specified value. Throws if already initialized or value is <c>0L</c>.
        /// <para><i>[ES] Inicializa con el valor indicado. Lanza si ya está inicializado o el valor es <c>0L</c>.</i></para>
        /// </summary>
        public long Init(long value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (value == 0L)
                throw new ArgumentException("Id cannot be 0L.", nameof(value));
            this.Id = value;
            return value;
        }
    }

    /// <summary>
    /// Register entity specialized for <see cref="string"/> identity. <see cref="string.Empty"/> means not initialized.
    /// <para><i>[ES] Entidad de registro con identidad <see cref="string"/>. <see cref="string.Empty"/> = no inicializado.</i></para>
    /// </summary>
    public abstract class C_RegisterString : C_Register<string>
    {
        /// <summary>
        /// Gets the unique identifier. <see cref="string.Empty"/> means not initialized.
        /// <para><i>[ES] Identificador único. <see cref="string.Empty"/> = no inicializado.</i></para>
        /// </summary>
        public new string Id
        {
            get => base.Id ?? string.Empty;
            protected set => base.Id = value ?? string.Empty;
        }

        /// <summary>
        /// Gets whether a non-empty identity is assigned.
        /// <para><i>[ES] Indica si hay identidad no vacía.</i></para>
        /// </summary>
        public new bool IsInitialized => !string.IsNullOrEmpty(this.Id);

        /// <summary>
        /// Initializes as not initialized (<see cref="string.Empty"/>).
        /// <para><i>[ES] Inicializa como no inicializado (<see cref="string.Empty"/>).</i></para>
        /// </summary>
        protected C_RegisterString() : base(string.Empty) { }

        /// <summary>
        /// Initializes with a specified identifier. Null is stored as <see cref="string.Empty"/>.
        /// <para><i>[ES] Inicializa con un identificador concreto. Null se almacena como <see cref="string.Empty"/>.</i></para>
        /// </summary>
        protected C_RegisterString(string? id) : base(id ?? string.Empty) { }

        /// <summary>
        /// Rehydration constructor for loading from persistence.
        /// <para><i>[ES] Constructor de rehidratación para cargar desde persistencia.</i></para>
        /// </summary>
        protected C_RegisterString(
            string? id,
            DateTime createDate,
            DateTime? updateDate = null,
            DateTime? deleteDate = null)
            : base(id ?? string.Empty, createDate, updateDate, deleteDate) { }

        /// <summary>
        /// Copy constructor from another <see cref="I_Register{TId}"/> of <see cref="string"/>.
        /// <para><i>[ES] Constructor de copia desde otro <see cref="I_Register{TId}"/> de <see cref="string"/>.</i></para>
        /// </summary>
        protected C_RegisterString(I_Register<string>? source) : base(source) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{TId}"/> of <see cref="string"/>.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{TId}"/> de <see cref="string"/>.</i></para>
        /// </summary>
        protected C_RegisterString(I_Identifiable<string>? source)
            : base(source is null ? string.Empty : (source.Id ?? string.Empty)) { }

        /// <summary>
        /// Initializes with the specified value. Throws if already initialized or value is null/empty.
        /// <para><i>[ES] Inicializa con el valor indicado. Lanza si ya está inicializado o el valor es null/vacío.</i></para>
        /// </summary>
        public string Init(string value)
        {
            if (this.IsInitialized)
                throw new InvalidOperationException("Id is already initialized and cannot be changed.");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Id cannot be null or empty.", nameof(value));
            this.Id = value;
            return value;
        }
    }
}
