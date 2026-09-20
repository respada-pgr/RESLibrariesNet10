using _1_LibraryClassesNet10.Enums;
using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes
{
    /// <summary>
    /// Encapsulates and manages flag-based permission state for <see cref="E_Permissions"/>.
    /// <para><i>[ES] Encapsula y gestiona el estado de permisos basados en flags para <see cref="E_Permissions"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// C_Permissions user = new(E_Permissions.READ);
    /// user += E_Permissions.WRITE;
    /// if (user.HasAllPermissions(E_Permissions.READ | E_Permissions.WRITE))
    /// {
    ///     // full read/write
    /// }
    /// user -= E_Permissions.READ;
    /// if (user == E_Permissions.WRITE)
    /// {
    ///     // write only
    /// }
    /// </code>
    /// </example>
    public class C_Permissions : I_Permissions, IEquatable<C_Permissions>, IEquatable<E_Permissions>
    {
        /// <inheritdoc />
        public E_Permissions Value { get; private set; }

        /// <inheritdoc />
        public bool HasNone => this.Value == E_Permissions.NONE;

        /// <inheritdoc />
        public bool HasAny => this.Value != E_Permissions.NONE;

        /// <inheritdoc />
        public bool HasAll => (this.Value & E_Permissions.ALL) == E_Permissions.ALL;

        /// <summary>
        /// Initializes a new instance with the specified permission flags.
        /// <para><i>[ES] Inicializa una nueva instancia con los flags de permiso indicados.</i></para>
        /// </summary>
        /// <param name="initialPermissions">
        /// Initial permissions. Defaults to <see cref="E_Permissions.NONE"/>.
        /// <para><i>[ES] Permisos iniciales. Por defecto <see cref="E_Permissions.NONE"/>.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// var empty = new C_Permissions();
        /// var rw = new C_Permissions(E_Permissions.READ | E_Permissions.WRITE);
        /// </code>
        /// </example>
        public C_Permissions(E_Permissions initialPermissions = E_Permissions.NONE)
        {
            this.Value = initialPermissions;
        }

        /// <inheritdoc />
        public void Add(E_Permissions permission) => this.Value |= permission;

        /// <inheritdoc />
        public void Remove(E_Permissions permission) => this.Value &= ~permission;

        /// <inheritdoc />
        public void Toggle(E_Permissions permission) => this.Value ^= permission;

        /// <inheritdoc />
        public bool Has(E_Permissions permission)
        {
            if (permission == E_Permissions.NONE)
                return this.HasNone;
            return this.Value.HasFlag(permission);
        }

        /// <inheritdoc />
        public bool HasAnyPermission(E_Permissions permissions)
            => (this.Value & permissions) != E_Permissions.NONE;

        /// <inheritdoc />
        public bool HasAllPermissions(E_Permissions permissions)
            => (this.Value & permissions) == permissions;

        /// <inheritdoc />
        public void Clear() => this.Value = E_Permissions.NONE;

        /// <summary>
        /// Returns a string representation of the current permissions.
        /// <para><i>[ES] Devuelve una representación en texto de los permisos actuales.</i></para>
        /// </summary>
        public override string ToString() => this.Value.ToString();

        /// <summary>
        /// Implicitly converts a <see cref="C_Permissions"/> instance to <see cref="E_Permissions"/>.
        /// <para><i>[ES] Convierte implícitamente una instancia de <see cref="C_Permissions"/> a <see cref="E_Permissions"/>.</i></para>
        /// </summary>
        public static implicit operator E_Permissions(C_Permissions? permissions)
            => permissions?.Value ?? E_Permissions.NONE;

        /// <summary>
        /// Implicitly converts an <see cref="E_Permissions"/> value to a <see cref="C_Permissions"/> instance.
        /// <para><i>[ES] Convierte implícitamente un valor de <see cref="E_Permissions"/> a una instancia de <see cref="C_Permissions"/>.</i></para>
        /// </summary>
        public static implicit operator C_Permissions(E_Permissions enumValue) => new(enumValue);

        /// <summary>
        /// Grants a permission on the target instance (mutates and returns <paramref name="target"/>).
        /// <para><i>[ES] Concede un permiso en la instancia de destino (muta y devuelve <paramref name="target"/>).</i></para>
        /// </summary>
        public static C_Permissions operator +(C_Permissions target, E_Permissions permission)
        {
            ArgumentNullException.ThrowIfNull(target);
            target.Add(permission);
            return target;
        }

        /// <summary>
        /// Revokes a permission from the target instance (mutates and returns <paramref name="target"/>).
        /// <para><i>[ES] Revoca un permiso de la instancia de destino (muta y devuelve <paramref name="target"/>).</i></para>
        /// </summary>
        public static C_Permissions operator -(C_Permissions target, E_Permissions permission)
        {
            ArgumentNullException.ThrowIfNull(target);
            target.Remove(permission);
            return target;
        }

        /// <summary>
        /// Combines permissions using bitwise OR.
        /// <para><i>[ES] Combina permisos con OR bit a bit.</i></para>
        /// </summary>
        public static E_Permissions operator |(C_Permissions? left, E_Permissions right)
            => (left?.Value ?? E_Permissions.NONE) | right;

        /// <summary>
        /// Intersects permissions using bitwise AND.
        /// <para><i>[ES] Interseca permisos con AND bit a bit.</i></para>
        /// </summary>
        public static E_Permissions operator &(C_Permissions? left, E_Permissions right)
            => (left?.Value ?? E_Permissions.NONE) & right;

        /// <inheritdoc />
        public bool Equals(C_Permissions? other)
            => other is not null && this.Value == other.Value;

        /// <inheritdoc />
        public bool Equals(E_Permissions other) => this.Value == other;

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj switch
        {
            C_Permissions c => this.Equals(c),
            E_Permissions e => this.Equals(e),
            _ => false
        };

        /// <inheritdoc />
        public override int GetHashCode() => this.Value.GetHashCode();

        /// <summary>
        /// Determines whether two <see cref="C_Permissions"/> instances are equal.
        /// <para><i>[ES] Determina si dos instancias de <see cref="C_Permissions"/> son iguales.</i></para>
        /// </summary>
        public static bool operator ==(C_Permissions? left, C_Permissions? right)
            => Equals(left, right);

        /// <summary>
        /// Determines whether two <see cref="C_Permissions"/> instances are not equal.
        /// <para><i>[ES] Determina si dos instancias de <see cref="C_Permissions"/> son distintas.</i></para>
        /// </summary>
        public static bool operator !=(C_Permissions? left, C_Permissions? right)
            => !Equals(left, right);

        /// <summary>
        /// Determines whether a <see cref="C_Permissions"/> instance equals an <see cref="E_Permissions"/> value.
        /// <para><i>[ES] Determina si una instancia de <see cref="C_Permissions"/> es igual a un valor de <see cref="E_Permissions"/>.</i></para>
        /// </summary>
        public static bool operator ==(C_Permissions? left, E_Permissions right)
            => left?.Value == right;

        /// <summary>
        /// Determines whether a <see cref="C_Permissions"/> instance differs from an <see cref="E_Permissions"/> value.
        /// <para><i>[ES] Determina si una instancia de <see cref="C_Permissions"/> difiere de un valor de <see cref="E_Permissions"/>.</i></para>
        /// </summary>
        public static bool operator !=(C_Permissions? left, E_Permissions right)
            => left?.Value != right;
    }
}
