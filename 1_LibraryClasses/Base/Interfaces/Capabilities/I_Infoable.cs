namespace _1_LibraryClasses
{
    /// <summary>
    /// Defines a contract for objects that can produce a diagnostic or display summary via <see cref="Info"/>.
    /// <para>
    /// Typical implementations return a multi-line text suitable for logs, UI details panels, or debugging.
    /// </para>
    /// <para><i>[ES] Define un contrato para objetos que pueden producir un resumen diagnóstico o de visualización
    /// mediante <see cref="Info"/>. Las implementaciones suelen devolver texto multilínea útil para logs,
    /// paneles de detalle o depuración.</i></para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class C_Note : I_Infoable
    /// {
    ///     public string Title { get; set; } = "";
    ///     public string Message { get; set; } = "";
    ///
    ///     public string Info()
    ///     {
    ///         return Title + ": " + Message;
    ///     }
    /// }
    ///
    /// I_Infoable item = new C_Note { Title = "Save", Message = "OK" };
    /// Console.WriteLine(item.Info());
    /// </code>
    /// </example>
    public interface I_Infoable
    {
        /// <summary>
        /// Returns a diagnostic or display summary of the object.
        /// <para>
        /// The text may be single-line or multi-line. It should not throw for a valid instance.
        /// </para>
        /// <para><i>[ES] Devuelve un resumen diagnóstico o de visualización del objeto.
        /// Puede ser de una o varias líneas. No debería lanzar en una instancia válida.</i></para>
        /// </summary>
        /// <returns>
        /// A non-null summary string (may be empty).
        /// <para><i>[ES] Cadena de resumen no nula (puede estar vacía).</i></para>
        /// </returns>
        string Info();
    }
}
