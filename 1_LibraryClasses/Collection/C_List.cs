using System.Collections;
using System.Collections.Generic;

namespace _1_LibraryClasses.Collection
{
    /// <summary>
    /// Concrete implementation of <see cref="I_List{T}"/> backed by <see cref="List{T}"/>.
    /// <para>
    /// Exposes first/last access, extract/remove at extremes, range helpers, and the full power of
    /// <see cref="List{T}"/> (search, sort, convert, capacity).
    /// <see cref="ICollection{T}.Add"/> appends at the end; use <see cref="AddFirst"/> to insert at the start.
    /// Call <see cref="MakeReadOnly"/> to freeze the list; further mutations throw <see cref="NotSupportedException"/>.
    /// </para>
    /// <para><i>[ES] Implementación concreta de <see cref="I_List{T}"/> respaldada por <see cref="List{T}"/>.
    /// Expone acceso al primero/último, extracción/eliminación en extremos, ayudas de rango y la potencia de
    /// <see cref="List{T}"/>. <see cref="ICollection{T}.Add"/> añade al final; use <see cref="AddFirst"/> al inicio.
    /// Use <see cref="MakeReadOnly"/> para congelar; las mutaciones posteriores lanzan <see cref="NotSupportedException"/>.</i></para>
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in the list.
    /// <para><i>[ES] El tipo de elementos de la lista.</i></para>
    /// </typeparam>
    /// <example>
    /// <code>
    /// var list = new C_List&lt;string&gt;();
    /// list.Add("body");
    /// list.AddFirst("header");
    /// string? head = list.GetFirst();
    /// list.ReplaceFirst("HEADER");
    /// list.MakeReadOnly();
    /// </code>
    /// </example>
    public class C_List<T> : I_List<T>
    {
#region PRIVATE FIELDS

        private readonly List<T> _items;
        private bool _isReadOnly;

#endregion

#region PUBLIC PROPERTIES

        /// <summary>
        /// Gets or sets the total number of elements the internal buffer can hold without resizing.
        /// <para><i>[ES] Obtiene o establece el número de elementos que el buffer interno puede contener sin redimensionar.</i></para>
        /// </summary>
        public int Capacity
        {
            get => this._items.Capacity;
            set
            {
                this.EnsureWritable();
                this._items.Capacity = value;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the list.
        /// <para><i>[ES] Obtiene el número de elementos contenidos en la lista.</i></para>
        /// </summary>
        public int Count => this._items.Count;

        /// <summary>
        /// Gets a value indicating whether the list is read-only.
        /// <para>
        /// When <see langword="true"/>, mutating operations throw <see cref="NotSupportedException"/>
        /// (or return <see langword="false"/> on Try* mutators). Call <see cref="MakeReadOnly"/> to freeze.
        /// </para>
        /// <para><i>[ES] Indica si la lista es de solo lectura.
        /// Si es <see langword="true"/>, las mutaciones lanzan <see cref="NotSupportedException"/>
        /// (o devuelven <see langword="false"/> en Try*). Use <see cref="MakeReadOnly"/> para congelar.</i></para>
        /// </summary>
        public bool IsReadOnly => this._isReadOnly;

        /// <summary>
        /// Explicit implementation of <see cref="ICollection{T}.IsReadOnly"/>.
        /// <para><i>[ES] Implementación explícita de <see cref="ICollection{T}.IsReadOnly"/>.</i></para>
        /// </summary>
        bool ICollection<T>.IsReadOnly => this._isReadOnly;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// <para><i>[ES] Obtiene o establece el elemento en el índice especificado.</i></para>
        /// </summary>
        public T this[int index]
        {
            get => this._items[index];
            set
            {
                this.EnsureWritable();
                this._items[index] = value;
            }
        }

#endregion

#region CONSTRUCTORS

        /// <summary>
        /// Initializes an empty list with the default capacity.
        /// <para><i>[ES] Inicializa una lista vacía con la capacidad predeterminada.</i></para>
        /// </summary>
        public C_List() => this._items = new List<T>();

        /// <summary>
        /// Initializes an empty list with the specified capacity.
        /// <para><i>[ES] Inicializa una lista vacía con la capacidad indicada.</i></para>
        /// </summary>
        public C_List(int capacity) => this._items = new List<T>(capacity);

        /// <summary>
        /// Initializes a list with elements copied from the specified collection.
        /// <para><i>[ES] Inicializa una lista con elementos copiados de la colección indicada.</i></para>
        /// </summary>
        public C_List(IEnumerable<T> collection) => this._items = new List<T>(collection);

#endregion

#region PUBLIC METHODS — I_List / IList

        /// <inheritdoc/>
        public T? GetFirst() => this._items.Count > 0 ? this._items[0] : default;

        /// <inheritdoc/>
        public T? GetLast() => this._items.Count > 0 ? this._items[this._items.Count - 1] : default;

        /// <inheritdoc/>
        public void ReplaceFirst(T value)
        {
            this.EnsureWritable();
            if (this._items.Count == 0)
                throw new InvalidOperationException("The list is empty. / La lista está vacía.");
            this._items[0] = value;
        }

        /// <inheritdoc/>
        public void ReplaceLast(T value)
        {
            this.EnsureWritable();
            if (this._items.Count == 0)
                throw new InvalidOperationException("The list is empty. / La lista está vacía.");
            this._items[this._items.Count - 1] = value;
        }

        /// <summary>
        /// Tries to get the first element without throwing.
        /// <para><i>[ES] Intenta obtener el primer elemento sin lanzar excepción.</i></para>
        /// </summary>
        public bool TryGetFirst(out T? value)
        {
            if (this._items.Count == 0)
            {
                value = default;
                return false;
            }
            value = this._items[0];
            return true;
        }

        /// <summary>
        /// Tries to get the last element without throwing.
        /// <para><i>[ES] Intenta obtener el último elemento sin lanzar excepción.</i></para>
        /// </summary>
        public bool TryGetLast(out T? value)
        {
            if (this._items.Count == 0)
            {
                value = default;
                return false;
            }
            value = this._items[this._items.Count - 1];
            return true;
        }

        /// <summary>
        /// Tries to replace the first element. Returns <see langword="false"/> if empty or read-only.
        /// <para><i>[ES] Intenta sustituir el primer elemento. Devuelve <see langword="false"/> si está vacía o es solo lectura.</i></para>
        /// </summary>
        public bool TryReplaceFirst(T value)
        {
            if (this._isReadOnly || this._items.Count == 0)
                return false;
            this._items[0] = value;
            return true;
        }

        /// <summary>
        /// Tries to replace the last element. Returns <see langword="false"/> if empty or read-only.
        /// <para><i>[ES] Intenta sustituir el último elemento. Devuelve <see langword="false"/> si está vacía o es solo lectura.</i></para>
        /// </summary>
        public bool TryReplaceLast(T value)
        {
            if (this._isReadOnly || this._items.Count == 0)
                return false;
            this._items[this._items.Count - 1] = value;
            return true;
        }

        /// <inheritdoc/>
        public void AddFirst(T item)
        {
            this.EnsureWritable();
            this._items.Insert(0, item);
        }

        /// <inheritdoc/>
        public T ExtractAt(int index)
        {
            this.EnsureWritable();
            T item = this._items[index];
            this._items.RemoveAt(index);
            return item;
        }

        /// <inheritdoc/>
        public T? ExtractFirst()
        {
            this.EnsureWritable();
            if (this._items.Count == 0)
                return default;
            return this.ExtractAt(0);
        }

        /// <inheritdoc/>
        public T? ExtractLast()
        {
            this.EnsureWritable();
            if (this._items.Count == 0)
                return default;
            return this.ExtractAt(this._items.Count - 1);
        }

        /// <inheritdoc/>
        public void RemoveFirst()
        {
            this.EnsureWritable();
            if (this._items.Count == 0)
                return;
            this._items.RemoveAt(0);
        }

        /// <inheritdoc/>
        public void RemoveLast()
        {
            this.EnsureWritable();
            if (this._items.Count == 0)
                return;
            this._items.RemoveAt(this._items.Count - 1);
        }

        /// <inheritdoc/>
        public void Add(T item)
        {
            this.EnsureWritable();
            this._items.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.EnsureWritable();
            this._items.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(T item) => this._items.Contains(item);

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex) => this._items.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public int IndexOf(T item) => this._items.IndexOf(item);

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            this.EnsureWritable();
            this._items.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            this.EnsureWritable();
            return this._items.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            this.EnsureWritable();
            this._items.RemoveAt(index);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => this._items.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#endregion

#region PUBLIC METHODS — range insert / remove

        /// <summary>
        /// Inserts the elements of a collection at the beginning of the list.
        /// <para><i>[ES] Inserta los elementos de una colección al principio de la lista.</i></para>
        /// </summary>
        public void AddRangeFirst(IEnumerable<T> collection)
        {
            this.EnsureWritable();
            this._items.InsertRange(0, collection);
        }

        /// <summary>
        /// Appends the elements of a collection to the end of the list.
        /// <para><i>[ES] Añade los elementos de una colección al final de la lista.</i></para>
        /// </summary>
        public void AddRange(IEnumerable<T> collection)
        {
            this.EnsureWritable();
            this._items.AddRange(collection);
        }

        /// <summary>
        /// Inserts the elements of a collection at the specified index.
        /// <para><i>[ES] Inserta los elementos de una colección en el índice especificado.</i></para>
        /// </summary>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            this.EnsureWritable();
            this._items.InsertRange(index, collection);
        }

        /// <summary>
        /// Removes a range of elements starting at the specified index.
        /// <para><i>[ES] Elimina un rango de elementos a partir del índice especificado.</i></para>
        /// </summary>
        public void RemoveRange(int index, int count)
        {
            this.EnsureWritable();
            this._items.RemoveRange(index, count);
        }

        /// <summary>
        /// Removes up to <paramref name="count"/> elements from the beginning of the list.
        /// <para><i>[ES] Elimina hasta <paramref name="count"/> elementos desde el principio de la lista.</i></para>
        /// </summary>
        public void RemoveRangeFirst(int count)
        {
            this.EnsureWritable();
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int actualCount = Math.Min(count, this._items.Count);
            if (actualCount > 0)
                this._items.RemoveRange(0, actualCount);
        }

        /// <summary>
        /// Removes up to <paramref name="count"/> elements from the end of the list.
        /// <para><i>[ES] Elimina hasta <paramref name="count"/> elementos desde el final de la lista.</i></para>
        /// </summary>
        public void RemoveRangeLast(int count)
        {
            this.EnsureWritable();
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int actualCount = Math.Min(count, this._items.Count);
            if (actualCount > 0)
                this._items.RemoveRange(this._items.Count - actualCount, actualCount);
        }

        /// <summary>
        /// Removes all elements that match the conditions defined by the specified predicate.
        /// <para><i>[ES] Elimina todos los elementos que cumplen el predicado indicado.</i></para>
        /// </summary>
        public int RemoveAll(Predicate<T> match)
        {
            this.EnsureWritable();
            return this._items.RemoveAll(match);
        }

#endregion

#region PUBLIC METHODS — try remove / try extract

        /// <summary>
        /// Tries to remove a range of elements; returns whether any were removed and how many.
        /// <para><i>[ES] Intenta eliminar un rango; indica si se eliminó algo y cuántos elementos.</i></para>
        /// </summary>
        public bool TryRemoveRange(int index, int count, out int numElementsRemoved)
        {
            if (this._isReadOnly || index < 0 || count < 0 || index > this._items.Count)
            {
                numElementsRemoved = 0;
                return false;
            }
            int actualCount = Math.Min(count, this._items.Count - index);
            if (actualCount <= 0)
            {
                numElementsRemoved = 0;
                return false;
            }
            this._items.RemoveRange(index, actualCount);
            numElementsRemoved = actualCount;
            return true;
        }

        /// <summary>
        /// Tries to remove elements from the beginning.
        /// <para><i>[ES] Intenta eliminar elementos desde el principio.</i></para>
        /// </summary>
        public bool TryRemoveRangeFirst(int count, out int numElementsRemoved)
        {
            if (this._isReadOnly || count <= 0 || this._items.Count == 0)
            {
                numElementsRemoved = 0;
                return false;
            }
            int actualCount = Math.Min(count, this._items.Count);
            this._items.RemoveRange(0, actualCount);
            numElementsRemoved = actualCount;
            return true;
        }

        /// <summary>
        /// Tries to remove elements from the end.
        /// <para><i>[ES] Intenta eliminar elementos desde el final.</i></para>
        /// </summary>
        public bool TryRemoveRangeLast(int count, out int numElementsRemoved)
        {
            if (this._isReadOnly || count <= 0 || this._items.Count == 0)
            {
                numElementsRemoved = 0;
                return false;
            }
            int actualCount = Math.Min(count, this._items.Count);
            this._items.RemoveRange(this._items.Count - actualCount, actualCount);
            numElementsRemoved = actualCount;
            return true;
        }

        /// <summary>
        /// Tries to extract and remove the element at the specified index.
        /// <para><i>[ES] Intenta extraer y eliminar el elemento en el índice especificado.</i></para>
        /// </summary>
        public bool TryExtractAt(int index, out T? result)
        {
            if (this._isReadOnly || index < 0 || index >= this._items.Count)
            {
                result = default;
                return false;
            }
            result = this._items[index];
            this._items.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Tries to extract and remove the first element.
        /// <para><i>[ES] Intenta extraer y eliminar el primer elemento.</i></para>
        /// </summary>
        public bool TryExtractFirst(out T? result) => this.TryExtractAt(0, out result);

        /// <summary>
        /// Tries to extract and remove the last element.
        /// <para><i>[ES] Intenta extraer y eliminar el último elemento.</i></para>
        /// </summary>
        public bool TryExtractLast(out T? result)
        {
            if (this._isReadOnly || this._items.Count == 0)
            {
                result = default;
                return false;
            }
            return this.TryExtractAt(this._items.Count - 1, out result);
        }

#endregion

#region PUBLIC METHODS — get / extract range

        /// <summary>
        /// Returns a new list with a copy of a range of elements (does not remove them).
        /// <para><i>[ES] Devuelve una lista nueva con una copia de un rango (no los elimina).</i></para>
        /// </summary>
        public C_List<T> GetRangeAt(int index, int count) => new(this._items.GetRange(index, count));

        /// <summary>
        /// Returns a new list with a copy of a range of elements.
        /// <para><i>[ES] Devuelve una lista nueva con una copia de un rango de elementos.</i></para>
        /// </summary>
        public C_List<T> GetRange(int index, int count) => this.GetRangeAt(index, count);

        /// <summary>
        /// Returns a new list with a copy of the first <paramref name="count"/> elements.
        /// <para><i>[ES] Devuelve una lista nueva con los primeros <paramref name="count"/> elementos.</i></para>
        /// </summary>
        public C_List<T> GetRangeFirst(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int actual = Math.Min(count, this._items.Count);
            return this.GetRangeAt(0, actual);
        }

        /// <summary>
        /// Returns a new list with a copy of the last <paramref name="count"/> elements.
        /// <para><i>[ES] Devuelve una lista nueva con los últimos <paramref name="count"/> elementos.</i></para>
        /// </summary>
        public C_List<T> GetRangeLast(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int actual = Math.Min(count, this._items.Count);
            return this.GetRangeAt(this._items.Count - actual, actual);
        }

        /// <summary>
        /// Tries to get a range of elements without removing them.
        /// <para><i>[ES] Intenta obtener un rango de elementos sin eliminarlos.</i></para>
        /// </summary>
        public bool TryGetRangeAt(int index, int length, out C_List<T>? result)
        {
            if (index < 0 || length < 0 || index + length > this._items.Count)
            {
                result = null;
                return false;
            }
            result = GetRangeAt(index, length);
            return true;
        }

        /// <summary>
        /// Tries to get the first elements without removing them.
        /// <para><i>[ES] Intenta obtener los primeros elementos sin eliminarlos.</i></para>
        /// </summary>
        public bool TryGetRangeFirst(int length, out C_List<T>? result)
            => this.TryGetRangeAt(0, length, out result);

        /// <summary>
        /// Tries to get the last elements without removing them.
        /// <para><i>[ES] Intenta obtener los últimos elementos sin eliminarlos.</i></para>
        /// </summary>
        public bool TryGetRangeLast(int length, out C_List<T>? result)
        {
            if (length < 0 || length > this._items.Count)
            {
                result = null;
                return false;
            }
            return this.TryGetRangeAt(this._items.Count - length, length, out result);
        }

        /// <summary>
        /// Extracts and removes a range of elements; returns them as a new <see cref="I_List{T}"/>.
        /// <para><i>[ES] Extrae y elimina un rango de elementos; los devuelve como un nuevo <see cref="I_List{T}"/>.</i></para>
        /// </summary>
        public I_List<T> ExtractRangeAt(int index, int count)
        {
            this.EnsureWritable();
            C_List<T> range = this.GetRangeAt(index, count);
            this._items.RemoveRange(index, count);
            return range;
        }

        /// <summary>
        /// Extracts and removes the first <paramref name="count"/> elements.
        /// <para><i>[ES] Extrae y elimina los primeros <paramref name="count"/> elementos.</i></para>
        /// </summary>
        public I_List<T> ExtractRangeFirst(int count)
        {
            this.EnsureWritable();
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int actual = Math.Min(count, this._items.Count);
            return this.ExtractRangeAt(0, actual);
        }

        /// <summary>
        /// Extracts and removes the last <paramref name="count"/> elements.
        /// <para><i>[ES] Extrae y elimina los últimos <paramref name="count"/> elementos.</i></para>
        /// </summary>
        public I_List<T> ExtractRangeLast(int count)
        {
            this.EnsureWritable();
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            int actual = Math.Min(count, this._items.Count);
            return this.ExtractRangeAt(this._items.Count - actual, actual);
        }

        /// <summary>
        /// Tries to extract and remove a range of elements.
        /// <para><i>[ES] Intenta extraer y eliminar un rango de elementos.</i></para>
        /// </summary>
        public bool TryExtractRangeAt(int index, int count, out C_List<T>? result)
        {
            if (this._isReadOnly || index < 0 || count < 0 || index + count > this._items.Count)
            {
                result = null;
                return false;
            }
            result = (C_List<T>)this.ExtractRangeAt(index, count);
            return true;
        }

        /// <summary>
        /// Tries to extract and remove the first elements.
        /// <para><i>[ES] Intenta extraer y eliminar los primeros elementos.</i></para>
        /// </summary>
        public bool TryExtractRangeFirst(int count, out C_List<T>? result)
            => this.TryExtractRangeAt(0, count, out result);

        /// <summary>
        /// Tries to extract and remove the last elements.
        /// <para><i>[ES] Intenta extraer y eliminar los últimos elementos.</i></para>
        /// </summary>
        public bool TryExtractRangeLast(int count, out C_List<T>? result)
        {
            if (this._isReadOnly || count < 0 || count > this._items.Count)
            {
                result = null;
                return false;
            }
            return this.TryExtractRangeAt(this._items.Count - count, count, out result);
        }

#endregion

#region PUBLIC METHODS — List&lt;T&gt; power (search, sort, convert)

        /// <summary>Creates a shallow copy as a range slice. / Crea una copia superficial de un tramo.</summary>
        public C_List<T> Slice(int start, int length) => this.GetRangeAt(start, length);

        public int IndexOf(T item, int index) => this._items.IndexOf(item, index);
        public int IndexOf(T item, int index, int count) => this._items.IndexOf(item, index, count);
        public int LastIndexOf(T item) => this._items.LastIndexOf(item);
        public int LastIndexOf(T item, int index) => this._items.LastIndexOf(item, index);
        public int LastIndexOf(T item, int index, int count) => this._items.LastIndexOf(item, index, count);

        public int BinarySearch(T item) => this._items.BinarySearch(item);
        public int BinarySearch(T item, IComparer<T>? comparer) => this._items.BinarySearch(item, comparer);
        public int BinarySearch(int index, int count, T item, IComparer<T>? comparer)
            => this._items.BinarySearch(index, count, item, comparer);

        public bool Exists(Predicate<T> match) => this._items.Exists(match);
        public T? Find(Predicate<T> match) => this._items.Find(match);
        public C_List<T> FindAll(Predicate<T> match) => new(this._items.FindAll(match));
        public int FindIndex(Predicate<T> match) => this._items.FindIndex(match);
        public int FindIndex(int startIndex, Predicate<T> match) => this._items.FindIndex(startIndex, match);
        public int FindIndex(int startIndex, int count, Predicate<T> match)
            => this._items.FindIndex(startIndex, count, match);
        public T? FindLast(Predicate<T> match) => this._items.FindLast(match);
        public int FindLastIndex(Predicate<T> match) => this._items.FindLastIndex(match);
        public int FindLastIndex(int startIndex, Predicate<T> match)
            => this._items.FindLastIndex(startIndex, match);
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
            => this._items.FindLastIndex(startIndex, count, match);
        public bool TrueForAll(Predicate<T> match) => this._items.TrueForAll(match);

        public void Sort()
        {
            this.EnsureWritable();
            this._items.Sort();
        }

        public void Sort(IComparer<T>? comparer)
        {
            this.EnsureWritable();
            this._items.Sort(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            this.EnsureWritable();
            this._items.Sort(comparison);
        }

        public void Sort(int index, int count, IComparer<T>? comparer)
        {
            this.EnsureWritable();
            this._items.Sort(index, count, comparer);
        }

        public void Reverse()
        {
            this.EnsureWritable();
            this._items.Reverse();
        }

        public void Reverse(int index, int count)
        {
            this.EnsureWritable();
            this._items.Reverse(index, count);
        }

        public C_List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
            => new(this._items.ConvertAll(converter));

        public void CopyTo(T[] array) => this._items.CopyTo(array);
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
            => this._items.CopyTo(index, array, arrayIndex, count);

        public int EnsureCapacity(int capacity)
        {
            this.EnsureWritable();
            return this._items.EnsureCapacity(capacity);
        }

        public void ForEach(Action<T> action) => this._items.ForEach(action);
        public T[] ToArray() => this._items.ToArray();

        /// <summary>
        /// Creates a shallow copy as a standard <see cref="List{T}"/>.
        /// <para><i>[ES] Crea una copia superficial como <see cref="List{T}"/> estándar.</i></para>
        /// </summary>
        public List<T> ToList() => new(this._items);

        public void TrimExcess()
        {
            this.EnsureWritable();
            this._items.TrimExcess();
        }

#endregion

#region PUBLIC METHODS — read-only

        /// <summary>
        /// Marks the list as read-only. Further mutations throw <see cref="NotSupportedException"/>
        /// (Try* mutators return <see langword="false"/>).
        /// <para><i>[ES] Marca la lista como solo lectura. Las mutaciones posteriores lanzan
        /// <see cref="NotSupportedException"/> (los Try* mutadores devuelven <see langword="false"/>).</i></para>
        /// </summary>
        public void MakeReadOnly() => this._isReadOnly = true;

#endregion

#region PRIVATE METHODS

        /// <summary>
        /// Throws if the list has been marked read-only.
        /// <para><i>[ES] Lanza si la lista se ha marcado como solo lectura.</i></para>
        /// </summary>
        /// <exception cref="NotSupportedException">The list is read-only.</exception>
        private void EnsureWritable()
        {
            if (this._isReadOnly)
                throw new NotSupportedException("The list is read-only. / La lista es de solo lectura.");
        }

#endregion
    }
}
