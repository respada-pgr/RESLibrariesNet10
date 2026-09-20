using _1_LibraryClassesNet10.Enums;

namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for managing a set of flag-based permissions using <see cref="E_Permissions"/>.
    /// <para><i>[ES] Define un contrato para gestionar un conjunto de permisos basados en flags usando <see cref="E_Permissions"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// I_Permissions perms = new C_Permissions(E_Permissions.READ);
    /// perms.Add(E_Permissions.WRITE);
    /// if (perms.Has(E_Permissions.READ))
    /// {
    ///     // authorized work
    /// }
    /// </code>
    /// </example>
    public interface I_Permissions
    {
        /// <summary>
        /// Gets the current set of assigned permission flags.
        /// <para><i>[ES] Obtiene el conjunto actual de flags de permiso asignados.</i></para>
        /// </summary>
        E_Permissions Value { get; }

        /// <summary>
        /// Gets a value indicating whether no permissions are granted (<see cref="E_Permissions.NONE"/>).
        /// <para><i>[ES] Indica si no hay permisos concedidos (<see cref="E_Permissions.NONE"/>).</i></para>
        /// </summary>
        bool HasNone { get; }

        /// <summary>
        /// Gets a value indicating whether at least one permission is granted.
        /// <para><i>[ES] Indica si hay al menos un permiso concedido.</i></para>
        /// </summary>
        bool HasAny { get; }

        /// <summary>
        /// Gets a value indicating whether all defined permissions (<see cref="E_Permissions.ALL"/>) are granted.
        /// <para><i>[ES] Indica si están concedidos todos los permisos definidos (<see cref="E_Permissions.ALL"/>).</i></para>
        /// </summary>
        bool HasAll { get; }

        /// <summary>
        /// Grants one or more permissions (bitwise OR).
        /// <para><i>[ES] Concede uno o más permisos (OR bit a bit).</i></para>
        /// </summary>
        /// <param name="permission">The permission flag(s) to grant.</param>
        void Add(E_Permissions permission);

        /// <summary>
        /// Revokes one or more permissions (bitwise AND NOT).
        /// <para><i>[ES] Revoca uno o más permisos (AND NOT bit a bit).</i></para>
        /// </summary>
        /// <param name="permission">The permission flag(s) to revoke.</param>
        void Remove(E_Permissions permission);

        /// <summary>
        /// Toggles one or more permissions (bitwise XOR).
        /// <para><i>[ES] Alterna uno o más permisos (XOR bit a bit).</i></para>
        /// </summary>
        /// <param name="permission">The permission flag(s) to toggle.</param>
        void Toggle(E_Permissions permission);

        /// <summary>
        /// Determines whether the specified permission flag(s) are granted.
        /// <para>
        /// For <see cref="E_Permissions.NONE"/>, returns the same as <see cref="HasNone"/>.
        /// For other values, uses <see cref="Enum.HasFlag"/> (all bits in <paramref name="permission"/> must be set).
        /// </para>
        /// <para><i>[ES] Determina si el/los flag(s) indicados están concedidos.
        /// Para <see cref="E_Permissions.NONE"/> equivale a <see cref="HasNone"/>.
        /// En otro caso usa <see cref="Enum.HasFlag"/> (deben estar todos los bits de <paramref name="permission"/>).</i></para>
        /// </summary>
        /// <param name="permission">The permission flag(s) to check.</param>
        /// <returns><see langword="true"/> if granted; otherwise, <see langword="false"/>.</returns>
        bool Has(E_Permissions permission);

        /// <summary>
        /// Determines whether at least one of the specified permission bits is granted.
        /// <para><i>[ES] Determina si al menos uno de los bits de permiso indicados está concedido.</i></para>
        /// </summary>
        /// <param name="permissions">The permission flags to test against.</param>
        /// <returns><see langword="true"/> if any overlapping bit is set; otherwise, <see langword="false"/>.</returns>
        bool HasAnyPermission(E_Permissions permissions);

        /// <summary>
        /// Determines whether all of the specified permission bits are granted.
        /// <para><i>[ES] Determina si todos los bits de permiso indicados están concedidos.</i></para>
        /// </summary>
        /// <param name="permissions">The permission flags that must all be present.</param>
        /// <returns><see langword="true"/> if all specified bits are set; otherwise, <see langword="false"/>.</returns>
        bool HasAllPermissions(E_Permissions permissions);

        /// <summary>
        /// Resets all permissions to <see cref="E_Permissions.NONE"/>.
        /// <para><i>[ES] Restablece todos los permisos a <see cref="E_Permissions.NONE"/>.</i></para>
        /// </summary>
        void Clear();
    }
}
