using System;

namespace _1_LibraryClassesNet10.Enums
{
    /// <summary>
    /// Represents a set of flag-based permissions that can be assigned to a user or role.
    /// </summary>
    /// <example>
    /// <code>
    /// // Assigning multiple permissions:
    /// E_Permissions userPermissions = E_Permissions.Read | E_Permissions.Write; // Binary: 00011, Decimal: 3 (Read, Write)
    /// E_Permissions allPermissions = E_Permissions.All;                         // Binary: 01111, Decimal: 15 (All)
    /// E_Permissions noPermissions = E_Permissions.None;                         // Binary: 00000, Decimal: 0 (None)
    /// 
    /// // Assigning via binary, hex, or decimal literals (requires explicit cast):
    /// E_Permissions readAndExecute = (E_Permissions)0b00101;                   // Binary: 00101, Decimal: 5 (Read, Execute)
    /// E_Permissions readAndExecuteHex = (E_Permissions)0x05;                   // Hex: 0x05, Decimal: 5 (Read, Execute)
    /// E_Permissions readAndExecuteDec = (E_Permissions)5;                      // Decimal: 5 (Read, Execute)
    /// 
    /// // Checking for a specific permission:
    /// bool canWrite = userPermissions.HasFlag(E_Permissions.Write);        // Returns true
    /// bool canDelete = (userPermissions & E_Permissions.Delete) == E_Permissions.Delete; // Returns false (Requires parenthesis due to operator precedence)
    /// 
    /// // IMPORTANT NOTE ON HasFlag(E_Permissions.None):
    /// // Do NOT use HasFlag to check for None (0).
    /// // userPermissions.HasFlag(E_Permissions.None) will ALWAYS return true because (x & 0) == 0 is true for any value.
    /// // To check if there are no permissions assigned, use direct equality instead:
    /// bool hasNoPermissions = userPermissions == E_Permissions.None;       // Returns false
    /// 
    /// // Adding a permission (OR):
    /// userPermissions |= E_Permissions.Execute;                            // Binary: 00111, Decimal: 7 (Read, Write, Execute)
    /// 
    /// // Safely revoking a permission (AND + NOT):
    /// userPermissions &= ~E_Permissions.Write;                             // Binary: 00101, Decimal: 5 (Read, Execute)
    /// 
    /// // Toggling a permission state (XOR):
    /// userPermissions ^= E_Permissions.Execute;                            // Binary: 00001, Decimal: 1 (Read only)
    /// userPermissions ^= E_Permissions.All;                                // Binary: 01110, Decimal: 14 (Write, Execute, Delete)
    /// </code>
    /// </example>
    [Flags]
    public enum E_Permissions
    {
        None = 0,                               // 00000 (Decimal: 0)
        Read = 1 << 0,                          // 00001 (Decimal: 1)
        Write = 1 << 1,                         // 00010 (Decimal: 2)
        Execute = 1 << 2,                       // 00100 (Decimal: 4)
        Delete = 1 << 3,                        // 01000 (Decimal: 8)
        All = Read | Write | Execute | Delete   // 01111 (Decimal: 15)
    }
}