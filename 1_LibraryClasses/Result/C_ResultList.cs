using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _1_LibraryClasses.Event;

namespace _1_LibraryClasses.Result
{
    /// <summary>
    /// Growable list of immutable <see cref="I_Result"/> (add-only).
    /// Counters are updated on <see cref="Add"/> (items do not change after insert).
    /// <para><i>[ES] Lista ampliable de <see cref="I_Result"/> inmutables (solo add).
    /// Los contadores se actualizan en <see cref="Add"/>.</i></para>
    /// </summary>
    public class C_ResultList : IReadOnlyList<I_Result>
    {
#region PRIVATE FIELDS

        private readonly List<I_Result> _items;
        private int _numOKs;
        private int _numWarnings;
        private int _numFails;
        private int _numErrors;
        private int _numExceptions;

#endregion

#region PUBLIC PROPERTIES

        public I_Result this[int index] => this._items[index];

        public int Count => this._items.Count;

        /// <summary>True when every item is OK or WARNING. / Todos OK o WARNING.</summary>
        public bool IsAllOk => this.Count == 0 || this._numFails == 0;

        public bool HasOKs => this._numOKs > 0;
        public bool HasWarnings => this._numWarnings > 0;
        public bool HasFails => this._numFails > 0;
        public bool HasErrors => this._numErrors > 0;
        public bool HasExceptions => this._numExceptions > 0;

        public int NumOKs => this._numOKs;
        public int NumWarnings => this._numWarnings;
        public int NumFails => this._numFails;
        public int NumErrors => this._numErrors;
        public int NumExceptions => this._numExceptions;

        /// <summary>OK + WARNING. / OK + WARNING.</summary>
        public IReadOnlyList<I_Result> OKs
            => this._items.Where(r => r.IsOK).ToList();

        public IReadOnlyList<I_Result> Warnings
            => this._items.Where(r => r.IsWarning).ToList();

        /// <summary>FAIL + ERROR + EXCEPTION. / FAIL + ERROR + EXCEPTION.</summary>
        public IReadOnlyList<I_Result> Fails
            => this._items.Where(r => r.IsFail).ToList();

        /// <summary>ERROR + EXCEPTION. / ERROR + EXCEPTION.</summary>
        public IReadOnlyList<I_Result> Errors
            => this._items.Where(r => r.IsError || r.HasException).ToList();

        public IReadOnlyList<I_Result> Exceptions
            => this._items.Where(r => r.HasException).ToList();

#endregion

#region CONSTRUCTORS

        public C_ResultList()
        {
            this._items = new List<I_Result>();
        }

        public C_ResultList(int capacity)
        {
            this._items = new List<I_Result>(capacity);
        }

        public C_ResultList(IEnumerable<I_Result> collection)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            this._items = new List<I_Result>();
            foreach (I_Result item in collection)
                this.Add(item);
        }

        public C_ResultList(params I_Result[] items)
            : this((IEnumerable<I_Result>)(items ?? Array.Empty<I_Result>()))
        {
        }

        public C_ResultList(C_Error error)
            : this()
        {
            if (error is not null)
                this.Add(C_Result.Fail(error));
        }

        public C_ResultList(Exception exception)
            : this()
        {
            if (exception is not null)
                this.Add(C_Result.Fail(exception));
        }

        /// <summary>
        /// Copy constructor: adds the same items (counters recalculated via Add).
        /// <para><i>[ES] Constructor de copia: añade los mismos ítems (contadores vía Add).</i></para>
        /// </summary>
        public C_ResultList(C_ResultList? source)
            : this(source is null ? 0 : source.Count)
        {
            if (source is null)
                return;
            foreach (I_Result item in source)
                this.Add(item);
        }

#endregion

#region PUBLIC METHODS

        public void Add(I_Result item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            this._items.Add(item);

            // Counters (items are immutable after add) — aligned with C_Result flags
            if (item.IsOK)
                this._numOKs++;
            if (item.IsWarning)
                this._numWarnings++;
            if (item.IsFail)
                this._numFails++;
            if (item.IsError || item.HasException)
                this._numErrors++;
            if (item.HasException)
                this._numExceptions++;
        }

        public void Add(C_Error error)
        {
            if (error is not null)
                this.Add(C_Result.Fail(error));
        }

        public void Add(C_Error error, string message)
        {
            if (error is not null)
                this.Add(C_Result.Fail(error, message));
        }

        public void Add(C_Exception exception)
        {
            if (exception is not null)
                this.Add(C_Result.Fail(exception));
        }

        public void Add(C_Exception exception, string message)
        {
            if (exception is not null)
                this.Add(C_Result.Fail(exception, message));
        }

        public void Add(Exception exception)
        {
            if (exception is not null)
                this.Add(C_Result.Fail(exception));
        }

        public void Add(Exception exception, string message)
        {
            if (exception is not null)
                this.Add(C_Result.Fail(exception, message));
        }

        public void Add(C_Warning warning)
        {
            if (warning is not null)
                this.Add(C_Result.Ok(warning));
        }

        public void AddRange(IEnumerable<I_Result> collection)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            foreach (I_Result item in collection)
                this.Add(item);
        }

        public bool Contains(I_Result item)
            => this._items.Contains(item);

        public int IndexOf(I_Result item)
            => this._items.IndexOf(item);

        public void CopyTo(I_Result[] array, int arrayIndex)
            => this._items.CopyTo(array, arrayIndex);

        public I_Result[] ToArray()
            => this._items.ToArray();

        public List<I_Result> ToList()
            => new List<I_Result>(this._items);

        public IEnumerator<I_Result> GetEnumerator()
            => this._items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public C_ResultList Clone()
            => new C_ResultList(this);

        public override string ToString()
            => $"C_ResultList Count={this.Count}, OKs={this.NumOKs}, Warnings={this.NumWarnings}, Fails={this.NumFails}, Errors={this.NumErrors}, Exceptions={this.NumExceptions}";

#endregion

    }
}
