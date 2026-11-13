using _1_LibraryClasses;
using _1_LibraryClasses.Register;

namespace _1_LibraryClasses.Security
{
    /// <summary>
    /// Minimal user placeholder for security/audit scenarios. Shape under review
    /// (may move to a dedicated library later).
    /// <para><i>[ES] Usuario mínimo provisional para seguridad/auditoría. Forma en revisión
    /// (puede moverse a una librería propia más adelante).</i></para>
    /// </summary>
    public class C_User : C_RegisterNamedGuid
    {
        public C_User() : base() { }

        public C_User(Guid id, string name = "") : base(id, name) { }

        public C_User(string name) : base(Guid.NewGuid(), name) { }

        /// <summary>
        /// Copy constructor. Preserves audit dates from <paramref name="source"/> (via register copy).
        /// <para><i>[ES] Constructor de copia. Conserva fechas de auditoría del origen.</i></para>
        /// </summary>
        public C_User(C_User? source) : base(source)
        {
        }

        public C_User Clone()
            => new C_User(this);
    }
}
