using System;
using _1_LibraryClassesNet10.Enums;

namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines the contract for managing permission states using <see cref="E_Permissions"/>.
    /// </summary>
    /// <remarks xml:lang="es">
    /// Define el contrato para la gestión de estados de permisos utilizando <see cref="E_Permissions"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create an instance initialized with Read permission
    /// // Crear una instancia inicializada con el permiso Read
    /// I_Permissions perms = new C_Permissions(E_Permissions.Read);
    /// 
    /// // Grant Write permission
    /// // Conceder el permiso Write
    /// perms.Add(E_Permissions.Write);
    /// 
    /// // Check if Read permission is granted
    /// // Verificar si el permiso Read está concedido
    /// if (perms.Has(E_Permissions.Read))
    /// {
    ///     // Perform authorized operation / Realizar operación autorizada
    /// }
    /// </code>
    /// </example>
    public interface I_Permissions
    {
        /// <summary>Gets the current set of assigned permissions.</summary>
        /// <remarks xml:lang="es">Obtiene el conjunto actual de permisos asignados.</remarks>
        E_Permissions Value { get; }

        /// <summary>
        /// Gets a value indicating whether no permissions are granted (<see cref="E_Permissions.None"/>). 
        /// Returns <c>true</c> if no permissions are granted; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Obtiene un valor que indica el estado de permisos. 
        /// Devuelve <c>true</c> si no hay permisos concedidos; en caso contrario, <c>false</c>.
        /// </remarks>
        bool HasNone { get; }

        /// <summary>
        /// Gets a value indicating whether at least one permission is granted. 
        /// Returns <c>true</c> if any permission is granted; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Obtiene un valor que indica si existe algún permiso asignado.
        /// Devuelve <c>true</c> si al menos un permiso está concedido; en caso contrario, <c>false</c>.
        /// </remarks>
        bool HasAny { get; }

        /// <summary>
        /// Gets a value indicating whether all defined permissions (<see cref="E_Permissions.All"/>) are granted. 
        /// Returns <c>true</c> if all permissions are granted; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Obtiene un valor que indica la presencia de la totalidad de permisos.
        /// Devuelve <c>true</c> si todos los permisos están concedidos; en caso contrario, <c>false</c>.
        /// </remarks>
        bool HasAll { get; }

        /// <summary>Grants one or more permissions.</summary>
        /// <remarks xml:lang="es">Concede uno o más permisos.</remarks>
        /// <param name="permission">The permission(s) to grant.</param>
        /// <example>
        /// <code>
        /// // Add Read and Write permissions simultaneously
        /// // Agregar permisos de Read y Write simultáneamente
        /// userPermissions.Add(E_Permissions.Read | E_Permissions.Write);
        /// </code>
        /// </example>
        void Add(E_Permissions permission);

        /// <summary>Safely revokes one or more permissions.</summary>
        /// <remarks xml:lang="es">Revoca de forma segura uno o más permisos.</remarks>
        /// <param name="permission">The permission(s) to revoke.</param>
        /// <example>
        /// <code>
        /// // Remove Write permission safely
        /// // Revocar el permiso Write de forma segura
        /// userPermissions.Remove(E_Permissions.Write);
        /// </code>
        /// </example>
        void Remove(E_Permissions permission);

        /// <summary>Toggles the state of one or more permissions.</summary>
        /// <remarks xml:lang="es">Alterna el estado de uno o más permisos.</remarks>
        /// <param name="permission">The permission(s) to toggle.</param>
        /// <example>
        /// <code>
        /// // Invert the state of Execute permission (grants if missing, revokes if present)
        /// // Invertir el estado del permiso Execute (concede si falta, revoca si existe)
        /// userPermissions.Toggle(E_Permissions.Execute);
        /// </code>
        /// </example>
        void Toggle(E_Permissions permission);

        /// <summary>Determines whether a specific permission is granted.</summary>
        /// <remarks xml:lang="es">Determina si un permiso específico está concedido.</remarks>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c> if the permission is granted; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// // Check if user has Write access
        /// // Comprobar si el usuario tiene acceso de escritura
        /// bool canWrite = perms.Has(E_Permissions.Write);
        /// </code>
        /// </example>
        bool Has(E_Permissions permission);

        /// <summary>Determines whether at least one of the specified permissions is granted.</summary>
        /// <remarks xml:lang="es">Determina si al menos uno de los permisos especificados está concedido.</remarks>
        /// <param name="permissions">The combined permissions to check.</param>
        /// <returns><c>true</c> if any specified permission is present; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// // Verify if the user has either Read OR Execute permission
        /// // Verificar si el usuario tiene permiso de Read O de Execute
        /// bool canAccess = perms.HasAnyPermission(E_Permissions.Read | E_Permissions.Execute);
        /// </code>
        /// </example>
        bool HasAnyPermission(E_Permissions permissions);

        /// <summary>Determines whether all specified permissions are granted.</summary>
        /// <remarks xml:lang="es">Determina si todos los permisos especificados están concedidos.</remarks>
        /// <param name="permissions">The combined permissions to check.</param>
        /// <returns><c>true</c> if all specified permissions are present; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// // Verify if the user has both Read AND Write permissions
        /// // Verificar si el usuario tiene permisos de Read Y Write al mismo tiempo
        /// bool isFullControl = perms.HasAllPermissions(E_Permissions.Read | E_Permissions.Write);
        /// </code>
        /// </example>
        bool HasAllPermissions(E_Permissions permissions);

        /// <summary>Resets all assigned permissions to <see cref="E_Permissions.None"/>.</summary>
        /// <remarks xml:lang="es">Restablece todos los permisos asignados a <see cref="E_Permissions.None"/>.</remarks>
        void Clear();
    }
}