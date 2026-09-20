namespace _1_LibraryClassesNet10.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that can be initialized and expose their initialization state.
    /// <para>
    /// Extends <see cref="I_Initialized"/>. Initialization is allowed only once; subsequent calls should fail.
    /// </para>
    /// <para>
    /// <b>Parameterless <see cref="Init()"/>:</b> intended for types that can generate an identity value
    /// (e.g. <see cref="Guid"/> via <c>C_IdGuid</c> / <c>C_EntityGuid</c>).
    /// Types whose identity comes from outside (e.g. <c>int</c>, <c>long</c>, <c>string</c> from a database)
    /// typically expose only <c>Init(TResult value)</c> on the concrete class and do not implement this interface.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que pueden ser inicializados y exponen su estado de inicialización.
    /// Extiende <see cref="I_Initialized"/>. La inicialización solo se permite una vez; las llamadas posteriores deben fallar.</i></para>
    /// <para><i>[ES]
    /// <b><see cref="Init()"/> sin parámetros:</b> pensado para tipos que pueden generar una identidad
    /// (p. ej. <see cref="Guid"/> con <c>C_IdGuid</c> / <c>C_EntityGuid</c>).
    /// Tipos cuya identidad viene de fuera (p. ej. <c>int</c>, <c>long</c>, <c>string</c> desde una base de datos)
    /// suelen exponer solo <c>Init(TResult value)</c> en la clase concreta y no implementan esta interfaz.
    /// </i></para>
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the value used and returned by the <see cref="Init()"/> methods
    /// (for identity types, typically the identifier type itself).
    /// <para><i>[ES] El tipo del valor usado y devuelto por los métodos <see cref="Init()"/>
    /// (en tipos de identidad, normalmente el propio tipo del identificador).</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// // Guid specialization implements I_Initializable&lt;Guid&gt;
    /// C_IdGuid id = new(Guid.Empty);
    /// Guid value = id.Init();
    ///
    /// // int specialization: Init(int) on the class only (does not implement I_Initializable&lt;int&gt;)
    /// C_IdInt n = new();
    /// n.Init(42);
    /// </code>
    /// </example>
    public interface I_Initializable<TResult> : I_Initialized
    {
        /// <summary>
        /// Initializes the object with a newly generated value and returns it.
        /// <para>
        /// Must not be called if the object is already initialized.
        /// Meaningful when the type can generate a value (e.g. <see cref="Guid"/>).
        /// </para>
        /// <para><i>[ES] Inicializa el objeto con un valor recién generado y lo devuelve.
        /// No debe llamarse si el objeto ya está inicializado.
        /// Tiene sentido cuando el tipo puede generar un valor (p. ej. <see cref="Guid"/>).</i></para>
        /// </summary>
        /// <returns>
        /// The result of the initialization process.
        /// <para><i>[ES] El resultado del proceso de inicialización.</i></para>
        /// </returns>
        TResult Init();

        /// <summary>
        /// Initializes the object with the specified value and returns it.
        /// <para>
        /// Must not be called if the object is already initialized.
        /// </para>
        /// <para><i>[ES] Inicializa el objeto con el valor especificado y lo devuelve.
        /// No debe llamarse si el objeto ya está inicializado.</i></para>
        /// </summary>
        /// <param name="value">
        /// The value to assign during initialization.
        /// <para><i>[ES] El valor a asignar durante la inicialización.</i></para>
        /// </param>
        /// <returns>
        /// The value assigned during initialization.
        /// <para><i>[ES] El valor asignado durante la inicialización.</i></para>
        /// </returns>
        TResult Init(TResult value);
    }
}