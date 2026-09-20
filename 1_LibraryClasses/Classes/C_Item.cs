using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes
{
    /// <summary>
    /// Abstract base for domain entities that carry a typed value in addition to an identifier.
    /// <para>
    /// Extends <see cref="C_Entity{TId}"/>. Use for measurements, readings, settings, or any entity
    /// whose primary payload is a single <typeparamref name="TValue"/>.
    /// </para>
    /// <para><i>[ES] Base abstracta para entidades de dominio que llevan un valor tipado además del identificador.
    /// Extiende <see cref="C_Entity{TId}"/>. Útil para medidas, lecturas, ajustes o cualquier entidad
    /// cuyo contenido principal sea un único <typeparamref name="TValue"/>.</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The underlying type of the unique identifier.
    /// <para><i>[ES] El tipo subyacente del identificador único.</i></para>
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of the value stored by the item.
    /// <para><i>[ES] El tipo del valor almacenado por el ítem.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// public class C_TemperatureReading : C_ItemGuid&lt;decimal&gt;
    /// {
    ///     public C_TemperatureReading(decimal value) : base(value) { }
    /// }
    /// </code>
    /// </example>
    public abstract class C_Item<TId, TValue> : C_Entity<TId>
    {
        /// <summary>
        /// Gets or sets the value associated with this item.
        /// <para><i>[ES] Obtiene o establece el valor asociado a este ítem.</i></para>
        /// </summary>
        public TValue Value { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance with an unassigned identifier and default value.
        /// <para><i>[ES] Inicializa una nueva instancia sin identificador y valor por defecto.</i></para>
        /// </summary>
        protected C_Item() : base()
        {
            this.Value = default!;
        }

        /// <summary>
        /// Initializes a new instance with a specific identifier and value.
        /// <para><i>[ES] Inicializa una nueva instancia con identificador y valor concretos.</i></para>
        /// </summary>
        protected C_Item(TId? id, TValue value = default!) : base(id)
        {
            this.Value = value;
        }

        /// <summary>
        /// Copy constructor from an identifiable source; value is set to default.
        /// <para><i>[ES] Constructor de copia desde una fuente identificable; el valor queda en default.</i></para>
        /// </summary>
        protected C_Item(I_Identifiable<TId>? source) : base(source)
        {
            this.Value = default!;
        }

        /// <summary>
        /// Copy constructor from another item (identity + value).
        /// <para><i>[ES] Constructor de copia desde otro ítem (identidad + valor).</i></para>
        /// </summary>
        protected C_Item(C_Item<TId, TValue>? source) : base(source)
        {
            this.Value = source is null ? default! : source.Value;
        }
    }

    /// <summary>
    /// Item entity specialized for <see cref="Guid"/> identity.
    /// <para><i>[ES] Ítem especializado con identidad <see cref="Guid"/>.</i></para>
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value stored by the item.
    /// <para><i>[ES] El tipo del valor almacenado por el ítem.</i></para>
    /// </typeparam>
    public abstract class C_ItemGuid<TValue> : C_Item<Guid, TValue>
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
        /// Initializes with a newly generated <see cref="Guid"/> and the given value.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo y el valor indicado.</i></para>
        /// </summary>
        protected C_ItemGuid(TValue value = default!) : base(Guid.NewGuid(), value) { }

        /// <summary>
        /// Initializes with a specified <see cref="Guid"/> and value.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> concreto y un valor.</i></para>
        /// </summary>
        protected C_ItemGuid(Guid id, TValue value = default!) : base(id, value) { }

        /// <summary>
        /// Constructor from an <see cref="I_Identifiable{Guid}"/> source; value is default.
        /// <para><i>[ES] Constructor a partir de <see cref="I_Identifiable{Guid}"/>; valor por defecto.</i></para>
        /// </summary>
        protected C_ItemGuid(I_Identifiable<Guid>? source)
            : base(source?.Id is Guid id && id != Guid.Empty ? id : Guid.Empty, default!) { }

        /// <summary>
        /// Copy constructor from another Guid-based item.
        /// <para><i>[ES] Constructor de copia desde otro ítem basado en Guid.</i></para>
        /// </summary>
        protected C_ItemGuid(C_ItemGuid<TValue>? source)
            : base(source is null ? Guid.Empty : source.Id, source is null ? default! : source.Value) { }
    }

    /// <summary>
    /// Item entity specialized for <see cref="int"/> identity. <c>0</c> means not initialized.
    /// <para><i>[ES] Ítem con identidad <see cref="int"/>. <c>0</c> = no inicializado.</i></para>
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value stored by the item.
    /// <para><i>[ES] El tipo del valor almacenado por el ítem.</i></para>
    /// </typeparam>
    public abstract class C_ItemInt<TValue> : C_Item<int, TValue>
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

        protected C_ItemInt(TValue value = default!) : base(0, value) { }

        protected C_ItemInt(int id, TValue value = default!) : base(id, value) { }

        protected C_ItemInt(I_Identifiable<int>? source)
            : base(source?.Id is int id && id != 0 ? id : 0, default!) { }

        protected C_ItemInt(C_ItemInt<TValue>? source)
            : base(source is null ? 0 : source.Id, source is null ? default! : source.Value) { }
    }

    /// <summary>
    /// Item entity specialized for <see cref="long"/> identity. <c>0L</c> means not initialized.
    /// <para><i>[ES] Ítem con identidad <see cref="long"/>. <c>0L</c> = no inicializado.</i></para>
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value stored by the item.
    /// <para><i>[ES] El tipo del valor almacenado por el ítem.</i></para>
    /// </typeparam>
    public abstract class C_ItemLong<TValue> : C_Item<long, TValue>
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

        protected C_ItemLong(TValue value = default!) : base(0L, value) { }

        protected C_ItemLong(long id, TValue value = default!) : base(id, value) { }

        protected C_ItemLong(I_Identifiable<long>? source)
            : base(source?.Id is long id && id != 0L ? id : 0L, default!) { }

        protected C_ItemLong(C_ItemLong<TValue>? source)
            : base(source is null ? 0L : source.Id, source is null ? default! : source.Value) { }
    }

    /// <summary>
    /// Item entity specialized for <see cref="string"/> identity. <see cref="string.Empty"/> means not initialized.
    /// <para><i>[ES] Ítem con identidad <see cref="string"/>. <see cref="string.Empty"/> = no inicializado.</i></para>
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value stored by the item.
    /// <para><i>[ES] El tipo del valor almacenado por el ítem.</i></para>
    /// </typeparam>
    public abstract class C_ItemString<TValue> : C_Item<string, TValue>
    {
        public new string Id
        {
            get => base.Id ?? string.Empty;
            protected set => base.Id = value ?? string.Empty;
        }

        public new bool IsInitialized => !string.IsNullOrEmpty(this.Id);

        protected C_ItemString(TValue value = default!) : base(string.Empty, value) { }

        protected C_ItemString(string? id, TValue value = default!) : base(id ?? string.Empty, value) { }

        protected C_ItemString(I_Identifiable<string>? source)
            : base(source is null ? string.Empty : (source.Id ?? string.Empty), default!) { }

        protected C_ItemString(C_ItemString<TValue>? source)
            : base(source is null ? string.Empty : source.Id, source is null ? default! : source.Value) { }
    }
}
