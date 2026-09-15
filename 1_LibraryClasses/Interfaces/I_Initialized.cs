namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that track their initialization state.
    /// <para>
    /// Implementations expose whether the object has completed its initialization process
    /// (for example, whether an identity has been assigned).
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que rastrean su estado de inicialización.
    /// Las implementaciones exponen si el objeto ha completado su proceso de inicialización
    /// (por ejemplo, si se le ha asignado una identidad).</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Configuration : I_Initialized
    /// {
    ///     public bool IsInitialized { get; private set; }
    ///
    ///     public void Load()
    ///     {
    ///         // Load configuration logic...
    ///         IsInitialized = true;
    ///     }
    /// }
    ///
    /// var config = new C_Configuration();
    /// if (!config.IsInitialized)
    /// {
    ///     config.Load();
    /// }
    /// </code>
    /// </example>
    public interface I_Initialized
    {
        /// <summary>
        /// Gets a value indicating whether the object has been properly initialized.
        /// <para><i>[ES] Obtiene un valor que indica si el objeto ha sido inicializado correctamente.</i></para>
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the object is initialized; otherwise, <see langword="false"/>.
        /// <para><i>[ES] <see langword="true"/> si el objeto está inicializado; de lo contrario, <see langword="false"/>.</i></para>
        /// </value>
        bool IsInitialized { get; }
    }
}
