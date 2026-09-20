using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Base
{
    /// <summary>
    /// Domain entity that records an action type at a point in time, with optional name and description.
    /// <para>
    /// Extends <see cref="C_EntityNamed{TId}"/> and implements <see cref="I_Descriptible"/>.
    /// <see cref="Date"/> is when the action occurred (UTC by default).
    /// </para>
    /// <para><i>[ES] Entidad de dominio que registra un tipo de acción en un instante, con nombre y descripción opcionales.
    /// Extiende <see cref="C_EntityNamed{TId}"/> e implementa <see cref="I_Descriptible"/>.
    /// <see cref="Date"/> es cuándo ocurrió la acción (UTC por defecto).</i></para>
    /// </summary>
    /// <typeparam name="TId">
    /// The type of the unique identifier.
    /// <para><i>[ES] El tipo del identificador único.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// var action = new C_ActionGuid(E_Action.CREATE, "Create order");
    /// E_Action kind = action.Action;
    /// DateTime when = action.Date;
    /// </code>
    /// </example>
    public class C_Action<TId> : C_EntityNamed<TId>, I_Descriptible
    {
        /// <summary>
        /// Gets or sets the action type.
        /// <para><i>[ES] Obtiene o establece el tipo de acción.</i></para>
        /// </summary>
        public E_Action Action { get; set; } = E_Action.UNDEFINED;

        /// <summary>
        /// Gets the UTC date and time when the action was recorded.
        /// <para><i>[ES] Obtiene la fecha y hora UTC en que se registró la acción.</i></para>
        /// </summary>
        public DateTime Date { get; protected set; }

        /// <summary>
        /// Gets or sets an optional description of the action.
        /// <para><i>[ES] Obtiene o establece una descripción opcional de la acción.</i></para>
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Initializes with default id, <see cref="E_Action.UNDEFINED"/>, and <see cref="Date"/> = UTC now.
        /// <para><i>[ES] Inicializa con id por defecto, <see cref="E_Action.UNDEFINED"/> y <see cref="Date"/> = UTC ahora.</i></para>
        /// </summary>
        public C_Action() : base()
        {
            this.Action = E_Action.UNDEFINED;
            this.Date = DateTime.UtcNow;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Initializes with identifier and action type.
        /// <para><i>[ES] Inicializa con identificador y tipo de acción.</i></para>
        /// </summary>
        public C_Action(TId? id, E_Action action = E_Action.UNDEFINED) : base(id)
        {
            this.Action = action;
            this.Date = DateTime.UtcNow;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Initializes with identifier, name, action, and optional description.
        /// <para><i>[ES] Inicializa con identificador, nombre, acción y descripción opcional.</i></para>
        /// </summary>
        public C_Action(
            TId? id,
            string? name,
            E_Action action = E_Action.UNDEFINED,
            string? description = null)
            : base(id, name)
        {
            this.Action = action;
            this.Date = DateTime.UtcNow;
            this.Description = description ?? string.Empty;
        }

        /// <summary>
        /// Copy constructor (identity, name, action, date, description).
        /// <para><i>[ES] Constructor de copia (identidad, nombre, acción, fecha, descripción).</i></para>
        /// </summary>
        public C_Action(C_Action<TId>? source) : base(source)
        {
            if (source is null)
            {
                this.Action = E_Action.UNDEFINED;
                this.Date = DateTime.UtcNow;
                this.Description = string.Empty;
            }
            else
            {
                this.Action = source.Action;
                this.Date = source.Date;
                this.Description = source.Description;
            }
        }
    }

    /// <summary>
    /// Action entity specialized for <see cref="Guid"/> identity.
    /// <para><i>[ES] Entidad de acción especializada con identidad <see cref="Guid"/>.</i></para>
    /// </summary>
    public class C_ActionGuid : C_Action<Guid>
    {
        /// <inheritdoc cref="C_EntityNamedGuid.Id"/>
        public new Guid Id
        {
            get
            {
                Guid? value = base.Id;
                return value.HasValue ? value.Value : Guid.Empty;
            }
            protected set => base.Id = value;
        }

        /// <inheritdoc cref="C_EntityNamedGuid.IsInitialized"/>
        public new bool IsInitialized => this.Id != Guid.Empty;

        /// <summary>
        /// Initializes with a new <see cref="Guid"/> and <see cref="E_Action.UNDEFINED"/>.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo y <see cref="E_Action.UNDEFINED"/>.</i></para>
        /// </summary>
        public C_ActionGuid() : base(Guid.NewGuid(), E_Action.UNDEFINED) { }

        /// <summary>
        /// Initializes with a new <see cref="Guid"/> and the specified action.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo y la acción indicada.</i></para>
        /// </summary>
        public C_ActionGuid(E_Action action) : base(Guid.NewGuid(), action) { }

        /// <summary>
        /// Initializes with a new <see cref="Guid"/>, name, action, and optional description.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> nuevo, nombre, acción y descripción opcional.</i></para>
        /// </summary>
        public C_ActionGuid(
            string? name,
            E_Action action = E_Action.UNDEFINED,
            string? description = null)
            : base(Guid.NewGuid(), name, action, description) { }

        /// <summary>
        /// Initializes with a specific <see cref="Guid"/>, name, action, and optional description.
        /// <para><i>[ES] Inicializa con un <see cref="Guid"/> concreto, nombre, acción y descripción opcional.</i></para>
        /// </summary>
        public C_ActionGuid(
            Guid id,
            string? name,
            E_Action action = E_Action.UNDEFINED,
            string? description = null)
            : base(id, name, action, description) { }

        /// <summary>
        /// Copy constructor.
        /// <para><i>[ES] Constructor de copia.</i></para>
        /// </summary>
        public C_ActionGuid(C_ActionGuid? source) : base(source) { }
    }
}
