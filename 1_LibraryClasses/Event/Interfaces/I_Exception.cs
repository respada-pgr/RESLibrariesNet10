namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// Contract for an exception-backed error notice. Implementable by throwable
    /// <see cref="C_Exception"/> without inheriting <see cref="C_Error"/>.
    /// <para><i>[ES] Contrato de aviso de error basado en excepción. Implementable por
    /// <see cref="C_Exception"/> lanzable sin heredar de <see cref="C_Error"/>.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// I_Error err = new C_Exception(ex);
    /// I_Exception iex = (I_Exception)err;
    /// Console.WriteLine(iex.EventType); // EXCEPTION
    /// </code>
    /// </example>
    public interface I_Exception : I_Error
    {
        /// <summary>
        /// Original runtime exception when this notice wraps one; otherwise <see langword="null"/>.
        /// <para><i>[ES] Excepción de runtime original si este aviso la envuelve; si no, <see langword="null"/>.</i></para>
        /// </summary>
        Exception? OriginalException { get; }
    }
}
