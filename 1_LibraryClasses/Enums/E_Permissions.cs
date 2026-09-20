namespace _1_LibraryClassesNet10.Enums
{
    /// <summary>
    /// Represents a set of flag-based permissions that can be assigned to a user or role.
    /// <para>
    /// Do not use <c>HasFlag(E_Permissions.NONE)</c> to test for no permissions:
    /// <c>(x &amp; 0) == 0</c> is always true. Use equality: <c>value == E_Permissions.NONE</c>.
    /// </para>
    /// <para><i>[ES] Representa un conjunto de permisos basados en flags asignables a un usuario o rol.
    /// No use <c>HasFlag(E_Permissions.NONE)</c> para comprobar la ausencia de permisos:
    /// <c>(x &amp; 0) == 0</c> es siempre verdadero. Use igualdad: <c>value == E_Permissions.NONE</c>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// E_Permissions user = E_Permissions.READ | E_Permissions.WRITE;
    /// bool canWrite = user.HasFlag(E_Permissions.WRITE);
    /// bool none = user == E_Permissions.NONE;
    /// user |= E_Permissions.EXECUTE;
    /// user &amp;= ~E_Permissions.WRITE;
    /// </code>
    /// </example>
    [Flags]
    public enum E_Permissions
    {
        /// <summary>No permissions granted.</summary>
        NONE = 0,

        /// <summary>Read permission.</summary>
        READ = 1 << 0,

        /// <summary>Write permission.</summary>
        WRITE = 1 << 1,

        /// <summary>Execute permission.</summary>
        EXECUTE = 1 << 2,

        /// <summary>Delete permission.</summary>
        DELETE = 1 << 3,

        /// <summary>All defined permissions combined.</summary>
        ALL = READ | WRITE | EXECUTE | DELETE
    }
}
