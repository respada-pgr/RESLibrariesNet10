using System;
using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes
{
    /// <summary>
    /// Encapsulates and manages flag-based permission logic for <see cref="E_Permissions"/>.
    /// </summary>
    /// <remarks xml:lang="es">
    /// Encapsula y gestiona la lógica de permisos basados en banderas para <see cref="E_Permissions"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// // 1. Initialize user permissions with Read access
    /// // 1. Inicializar los permisos del usuario con acceso de lectura
    /// C_Permissions userPermissions = new(E_Permissions.Read);
    /// 
    /// // 2. Add Write access using overloaded operator +
    /// // 2. Agregar acceso de escritura usando el operador sobrecargado +
    /// userPermissions += E_Permissions.Write;
    /// 
    /// // 3. Verify permissions using logical checks
    /// // 3. Verificar permisos mediante comprobaciones lógicas
    /// if (userPermissions.HasAllPermissions(E_Permissions.Read | E_Permissions.Write))
    /// {
    ///     Console.WriteLine("User has full read/write access.");
    ///     // El usuario tiene acceso completo de lectura y escritura.
    /// }
    /// 
    /// // 4. Revoke Read permission using operator -
    /// // 4. Revocar permiso de lectura usando el operador -
    /// userPermissions -= E_Permissions.Read;
    /// 
    /// // 5. Implicit conversion to enum for bitwise equality comparison
    /// // 5. Conversión implícita a enum para comparación de igualdad
    /// if (userPermissions == E_Permissions.Write)
    /// {
    ///     Console.WriteLine("User only retains Write permission.");
    ///     // El usuario solo conserva el permiso de escritura.
    /// }
    /// </code>
    /// </example>
    public class C_Permissions : I_Permissions, IEquatable<C_Permissions>, IEquatable<E_Permissions>
    {
        /// <inheritdoc />
        public E_Permissions Value { get; private set; }

        /// <inheritdoc />
        public bool HasNone => Value == E_Permissions.None;

        /// <inheritdoc />
        public bool HasAny => Value != E_Permissions.None;

        /// <inheritdoc />
        public bool HasAll => (Value & E_Permissions.All) == E_Permissions.All;

        /// <summary>Initializes a new instance of the <see cref="C_Permissions"/> class.</summary>
        /// <remarks xml:lang="es">Inicializa una nueva instancia de la clase <see cref="C_Permissions"/>.</remarks>
        /// <param name="initialPermissions">Initial permissions to assign. Defaults to <see cref="E_Permissions.None"/>.</param>
        /// <example>
        /// <code>
        /// // Create an empty instance (None)
        /// // Crear una instancia vacía (Sin permisos)
        /// var emptyPerms = new C_Permissions();
        /// 
        /// // Create an instance with combined initial flags
        /// // Crear una instancia con banderas iniciales combinadas
        /// var customPerms = new C_Permissions(E_Permissions.Read | E_Permissions.Write);
        /// </code>
        /// </example>
        public C_Permissions(E_Permissions initialPermissions = E_Permissions.None)
        {
            Value = initialPermissions;
        }

        /// <inheritdoc />
        public void Add(E_Permissions permission) => Value |= permission;

        /// <inheritdoc />
        public void Remove(E_Permissions permission) => Value &= ~permission;

        /// <inheritdoc />
        public void Toggle(E_Permissions permission) => Value ^= permission;

        /// <inheritdoc />
        public bool Has(E_Permissions permission)
        {
            if (permission == E_Permissions.None)
            {
                return HasNone;
            }

            return Value.HasFlag(permission);
        }

        /// <inheritdoc />
        public bool HasAnyPermission(E_Permissions permissions)
        {
            return (Value & permissions) != E_Permissions.None;
        }

        /// <inheritdoc />
        public bool HasAllPermissions(E_Permissions permissions)
        {
            return (Value & permissions) == permissions;
        }

        /// <inheritdoc />
        public void Clear() => Value = E_Permissions.None;

        /// <summary>Returns a string representation of the current permissions.</summary>
        /// <remarks xml:lang="es">Devuelve una representación en texto de los permisos actuales.</remarks>
        public override string ToString() => Value.ToString();

        #region Conversions and Operators

        /// <summary>Implicitly converts a <see cref="C_Permissions"/> instance to an <see cref="E_Permissions"/> enum value.</summary>
        /// <remarks xml:lang="es">Convierte implícitamente una instancia de <see cref="C_Permissions"/> a un valor del enum <see cref="E_Permissions"/>.</remarks>
        /// <example>
        /// <code>
        /// C_Permissions perms = new(E_Permissions.Read);
        /// 
        /// // Implicitly cast class instance to enum value
        /// // Conversión implícita de la instancia al valor del enum
        /// E_Permissions enumVal = perms;
        /// </code>
        /// </example>
        public static implicit operator E_Permissions(C_Permissions permissions) => permissions?.Value ?? E_Permissions.None;

        /// <summary>Implicitly converts an <see cref="E_Permissions"/> enum value to a <see cref="C_Permissions"/> instance.</summary>
        /// <remarks xml:lang="es">Convierte implícitamente un valor del enum <see cref="E_Permissions"/> a una instancia de <see cref="C_Permissions"/>.</remarks>
        /// <example>
        /// <code>
        /// // Create class instance directly from enum flag
        /// // Crear instancia de clase directamente desde un flag del enum
        /// C_Permissions perms = E_Permissions.Admin;
        /// </code>
        /// </example>
        public static implicit operator C_Permissions(E_Permissions enumValue) => new(enumValue);

        /// <summary>Adds a permission to the target instance.</summary>
        /// <remarks xml:lang="es">Agrega un permiso a la instancia de destino.</remarks>
        /// <example>
        /// <code>
        /// // Add Write permission to existing instance
        /// // Agregar el permiso Write a la instancia existente
        /// perms += E_Permissions.Write;
        /// </code>
        /// </example>
        public static C_Permissions operator +(C_Permissions target, E_Permissions permission)
        {
            target?.Add(permission);
            return target;
        }

        /// <summary>Removes a permission from the target instance.</summary>
        /// <remarks xml:lang="es">Revoca un permiso de la instancia de destino.</remarks>
        /// <example>
        /// <code>
        /// // Revoke Write permission from instance
        /// // Revocar el permiso Write de la instancia
        /// perms -= E_Permissions.Write;
        /// </code>
        /// </example>
        public static C_Permissions operator -(C_Permissions target, E_Permissions permission)
        {
            target?.Remove(permission);
            return target;
        }

        /// <summary>Combines permissions using bitwise OR.</summary>
        /// <remarks xml:lang="es">Combina permisos mediante una operación OR bit a bit.</remarks>
        public static E_Permissions operator |(C_Permissions left, E_Permissions right) => left?.Value | right ?? right;

        /// <summary>Intersects permissions using bitwise AND.</summary>
        /// <remarks xml:lang="es">Interseca permisos mediante una operación AND bit a bit.</remarks>
        public static E_Permissions operator &(C_Permissions left, E_Permissions right) => left?.Value & right ?? E_Permissions.None;

        /// <summary>
        /// Indicates whether the current object is equal to another <see cref="C_Permissions"/> instance. 
        /// Returns <c>true</c> if equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Indica si el objeto actual es igual a otra instancia de <see cref="C_Permissions"/>.
        /// Devuelve <c>true</c> si son iguales; en caso contrario, <c>false</c>.
        /// </remarks>
        public bool Equals(C_Permissions other) => other is not null && Value == other.Value;

        /// <summary>
        /// Indicates whether the current object is equal to an <see cref="E_Permissions"/> enum value. 
        /// Returns <c>true</c> if equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Indica si el objeto actual es igual a un valor del enum <see cref="E_Permissions"/>.
        /// Devuelve <c>true</c> si son iguales; en caso contrario, <c>false</c>.
        /// </remarks>
        public bool Equals(E_Permissions other) => Value == other;

        /// <summary>
        /// Determines whether the specified object is equal to the current object. 
        /// Returns <c>true</c> if equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Determina si el objeto especificado es igual al objeto actual.
        /// Devuelve <c>true</c> si son iguales; en caso contrario, <c>false</c>.
        /// </remarks>
        public override bool Equals(object obj) => obj switch
        {
            C_Permissions c => Equals(c),
            E_Permissions e => Equals(e),
            _ => false
        };

        /// <summary>Serves as the default hash function.</summary>
        /// <remarks xml:lang="es">Sirve como la función hash predeterminada.</remarks>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Determines whether two <see cref="C_Permissions"/> instances are equal. 
        /// Returns <c>true</c> if equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Determina si dos instancias de <see cref="C_Permissions"/> son iguales.
        /// Devuelve <c>true</c> si son iguales; en caso contrario, <c>false</c>.
        /// </remarks>
        public static bool operator ==(C_Permissions left, C_Permissions right) => Equals(left, right);

        /// <summary>
        /// Determines whether two <see cref="C_Permissions"/> instances are not equal. 
        /// Returns <c>true</c> if not equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Determina si dos instancias de <see cref="C_Permissions"/> son diferentes.
        /// Devuelve <c>true</c> si son diferentes; en caso contrario, <c>false</c>.
        /// </remarks>
        public static bool operator !=(C_Permissions left, C_Permissions right) => !Equals(left, right);

        /// <summary>
        /// Determines whether a <see cref="C_Permissions"/> instance is equal to an <see cref="E_Permissions"/> enum value. 
        /// Returns <c>true</c> if equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Determina si una instancia de <see cref="C_Permissions"/> es igual a un valor del enum <see cref="E_Permissions"/>.
        /// Devuelve <c>true</c> si son iguales; en caso contrario, <c>false</c>.
        /// </remarks>
        public static bool operator ==(C_Permissions left, E_Permissions right) => left?.Value == right;

        /// <summary>
        /// Determines whether a <see cref="C_Permissions"/> instance is not equal to an <see cref="E_Permissions"/> enum value. 
        /// Returns <c>true</c> if not equal; otherwise, <c>false</c>.
        /// </summary>
        /// <remarks xml:lang="es">
        /// Determina si una instancia de <see cref="C_Permissions"/> es diferente a un valor del enum <see cref="E_Permissions"/>.
        /// Devuelve <c>true</c> si son diferentes; en caso contrario, <c>false</c>.
        /// </remarks>
        public static bool operator !=(C_Permissions left, E_Permissions right) => left?.Value != right;

        #endregion
    }
}