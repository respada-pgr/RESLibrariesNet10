namespace _1_LibraryClasses
{
    /// <summary>
    /// Contract for objects that expose whether they have been initialized.
    /// <para>
    /// Initialization may happen in a constructor, via a setter, or via an optional <c>Init</c> method
    /// on a more specific contract. This interface only requires that the state can be queried.
    /// </para>
    /// <para><i>[ES] Contrato para objetos que exponen si han sido inicializados.
    /// La inicialización puede ocurrir en el constructor, con un setter o con un <c>Init</c> opcional.
    /// Esta interfaz solo exige poder consultar el estado.</i></para>
    /// </summary>
    public interface I_Initializable
    {
        /// <summary>
        /// Gets a value indicating whether the object has been initialized.
        /// <para><i>[ES] Obtiene si el objeto ha sido inicializado.</i></para>
        /// </summary>
        bool IsInitialized { get; }
    }

    /// <summary>
    /// Optional initialization with a generated or provided value of type <typeparamref name="TResult"/>.
    /// <para><i>[ES] Inicialización opcional con un valor generado o suministrado de tipo <typeparamref name="TResult"/>.</i></para>
    /// </summary>
    public interface I_Initializable<TResult> : I_Initializable
    {
        /// <summary>
        /// Initializes the object with a newly generated value and returns it.
        /// Must not be called if already initialized.
        /// <para><i>[ES] Inicializa con un valor generado y lo devuelve. No llamar si ya está inicializado.</i></para>
        /// </summary>
        TResult Init();

        /// <summary>
        /// Initializes the object with the specified value and returns it.
        /// Must not be called if already initialized.
        /// <para><i>[ES] Inicializa con el valor indicado y lo devuelve. No llamar si ya está inicializado.</i></para>
        /// </summary>
        TResult Init(TResult value);
    }
}
