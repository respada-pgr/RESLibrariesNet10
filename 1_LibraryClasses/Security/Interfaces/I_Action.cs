using _1_LibraryClasses;

using _1_LibraryClasses.Security;

namespace _1_LibraryClasses.Security
{
    /// <summary>
    /// Contract for a recorded domain action (type + when). Under review with <see cref="C_User"/>.
    /// <para><i>[ES] Contrato de una acción de dominio registrada (tipo + cuándo). En revisión junto con <see cref="C_User"/>.</i></para>
    /// </summary>
    public interface I_Action<TId> : I_Entity<TId>
    {
        E_Action Action { get; }
        DateTime Date { get; }
    }
}
