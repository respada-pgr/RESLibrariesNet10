namespace _1_LibraryClassesNet10.Enums
{
    /// <summary>
    /// Represents a domain or process action type.
    /// <para><i>[ES] Representa un tipo de acción de dominio o de proceso.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// E_Action action = E_Action.CREATE;
    /// if (action == E_Action.UNDEFINED) { /* ... */ }
    /// </code>
    /// </example>
    public enum E_Action
    {
        /// <summary>No action specified.</summary>
        UNDEFINED = -1,

        /// <summary>Create operation.</summary>
        CREATE = 0,

        /// <summary>Update operation.</summary>
        UPDATE = 1,

        /// <summary>Delete operation.</summary>
        DELETE = 2,

        /// <summary>End / finish operation.</summary>
        END = 3,

        /// <summary>Hold / pause operation.</summary>
        HOLD = 4,

        /// <summary>Read operation.</summary>
        READ = 5,

        /// <summary>Load operation.</summary>
        LOAD = 6,

        /// <summary>Save operation.</summary>
        SAVE = 7,

        /// <summary>Enable operation.</summary>
        ENABLE = 10,

        /// <summary>Disable operation.</summary>
        DISABLE = 11,

        /// <summary>Unsubscribe operation.</summary>
        UNSUBSCRIBE = 20,

        /// <summary>Send operation.</summary>
        SEND = 30
    }
}
