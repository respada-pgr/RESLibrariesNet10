using System.Collections.Generic;

namespace _1_LibraryClasses.Collection
{
    /// <summary>
    /// Generalized list contract that extends <see cref="IList{T}"/> with first/last access,
    /// insertion at the start, and extract/remove operations at the extremes.
    /// <para>
    /// <see cref="ICollection{T}.Add"/> appends at the <b>end</b>. Use <see cref="AddFirst"/> to insert at the beginning.
    /// <see cref="GetFirst"/> / <see cref="GetLast"/> return <c>default</c> when the list is empty.
    /// <see cref="ReplaceFirst"/> / <see cref="ReplaceLast"/> replace an existing element and throw if the list is empty.
    /// </para>
    /// <para><i>[ES] Contrato de lista que extiende <see cref="IList{T}"/> con acceso al primero/último,
    /// inserción al inicio y extracción/eliminación en los extremos.
    /// <see cref="ICollection{T}.Add"/> añade al <b>final</b>. Use <see cref="AddFirst"/> para el inicio.
    /// <see cref="GetFirst"/> / <see cref="GetLast"/> devuelven <c>default</c> si la lista está vacía.
    /// <see cref="ReplaceFirst"/> / <see cref="ReplaceLast"/> sustituyen un elemento existente y lanzan si está vacía.</i></para>
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in the list.
    /// <para><i>[ES] El tipo de elementos de la lista.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// I_List&lt;string&gt; lines = new C_List_String();
    /// lines.Add("body");
    /// lines.AddFirst("header");
    /// string? head = lines.GetFirst();
    /// lines.ReplaceFirst("HEADER");
    /// string? taken = lines.ExtractLast();
    /// </code>
    /// </example>
    public interface I_List<T> : IList<T>
    {
        /// <summary>
        /// Gets the first element, or <c>default</c> if the list is empty.
        /// <para><i>[ES] Obtiene el primer elemento, o <c>default</c> si la lista está vacía.</i></para>
        /// </summary>
        /// <returns>
        /// The element at index <c>0</c>, or <c>default(T)</c> when <see cref="ICollection{T}.Count"/> is <c>0</c>.
        /// <para><i>[ES] El elemento en el índice <c>0</c>, o <c>default(T)</c> si no hay elementos.</i></para>
        /// </returns>
        /// <example>
        /// <code>
        /// string? head = list.GetFirst();
        /// </code>
        /// </example>
        T? GetFirst();

        /// <summary>
        /// Gets the last element, or <c>default</c> if the list is empty.
        /// <para><i>[ES] Obtiene el último elemento, o <c>default</c> si la lista está vacía.</i></para>
        /// </summary>
        /// <returns>
        /// The element at index <c>Count - 1</c>, or <c>default(T)</c> when the list is empty.
        /// <para><i>[ES] El elemento en el índice <c>Count - 1</c>, o <c>default(T)</c> si está vacía.</i></para>
        /// </returns>
        /// <example>
        /// <code>
        /// string? tail = list.GetLast();
        /// </code>
        /// </example>
        T? GetLast();

        /// <summary>
        /// Replaces the first element with the specified value.
        /// <para>
        /// Does not insert: the list must already contain at least one element.
        /// </para>
        /// <para><i>[ES] Sustituye el primer elemento por el valor indicado.
        /// No inserta: la lista debe tener al menos un elemento.</i></para>
        /// </summary>
        /// <param name="value">
        /// The new value for index <c>0</c>.
        /// <para><i>[ES] Nuevo valor del índice <c>0</c>.</i></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// The list is empty.
        /// <para><i>[ES] La lista está vacía.</i></para>
        /// </exception>
        /// <example>
        /// <code>
        /// list.ReplaceFirst("HEADER");
        /// </code>
        /// </example>
        void ReplaceFirst(T value);

        /// <summary>
        /// Replaces the last element with the specified value.
        /// <para>
        /// Does not insert: the list must already contain at least one element.
        /// </para>
        /// <para><i>[ES] Sustituye el último elemento por el valor indicado.
        /// No inserta: la lista debe tener al menos un elemento.</i></para>
        /// </summary>
        /// <param name="value">
        /// The new value for the last index.
        /// <para><i>[ES] Nuevo valor del último índice.</i></para>
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// The list is empty.
        /// <para><i>[ES] La lista está vacía.</i></para>
        /// </exception>
        /// <example>
        /// <code>
        /// list.ReplaceLast("footer");
        /// </code>
        /// </example>
        void ReplaceLast(T value);

        /// <summary>
        /// Inserts an element at the beginning of the list (index <c>0</c>).
        /// <para><i>[ES] Inserta un elemento al principio de la lista (índice <c>0</c>).</i></para>
        /// </summary>
        /// <param name="item">
        /// The object to insert at the beginning.
        /// <para><i>[ES] El objeto a insertar al principio.</i></para>
        /// </param>
        /// <example>
        /// <code>
        /// list.AddFirst("header");
        /// </code>
        /// </example>
        void AddFirst(T item);

        /// <summary>
        /// Retrieves and removes the element at the specified index.
        /// <para><i>[ES] Obtiene y elimina el elemento en el índice especificado.</i></para>
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the element to extract.
        /// <para><i>[ES] Índice base cero del elemento a extraer.</i></para>
        /// </param>
        /// <returns>
        /// The element that was removed.
        /// <para><i>[ES] El elemento eliminado.</i></para>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is out of range.
        /// <para><i>[ES] <paramref name="index"/> está fuera de rango.</i></para>
        /// </exception>
        T ExtractAt(int index);

        /// <summary>
        /// Retrieves and removes the first element, or <c>default</c> if the list is empty.
        /// <para><i>[ES] Obtiene y elimina el primer elemento, o <c>default</c> si la lista está vacía.</i></para>
        /// </summary>
        T? ExtractFirst();

        /// <summary>
        /// Retrieves and removes the last element, or <c>default</c> if the list is empty.
        /// <para><i>[ES] Obtiene y elimina el último elemento, o <c>default</c> si la lista está vacía.</i></para>
        /// </summary>
        T? ExtractLast();

        /// <summary>
        /// Removes the first element. Does nothing if the list is empty.
        /// <para><i>[ES] Elimina el primer elemento. No hace nada si la lista está vacía.</i></para>
        /// </summary>
        void RemoveFirst();

        /// <summary>
        /// Removes the last element. Does nothing if the list is empty.
        /// <para><i>[ES] Elimina el último elemento. No hace nada si la lista está vacía.</i></para>
        /// </summary>
        void RemoveLast();
    }
}
