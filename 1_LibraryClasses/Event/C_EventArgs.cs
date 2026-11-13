using System;
using _1_LibraryClasses;
using _1_LibraryClasses.Result;

namespace _1_LibraryClasses.Event
{
    /// <summary>
    /// EventArgs payload with status and message (for .NET events).
    /// <para><i>[ES] Carga de EventArgs con estado y mensaje (para eventos .NET).</i></para>
    /// </summary>
    public class C_EventArgs : EventArgs, I_Infoable
    {
        #region PRIVATE FIELDS

        private E_ResultStatus _status = E_ResultStatus.ND;
        private DateTime _createDate = DateTime.Now;
        private string _message = "";

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>Creation timestamp. / Fecha de creación.</summary>
        public DateTime CreateDate => _createDate;

        public string Message
        {
            get => _message;
            set => _message = value ?? "";
        }

        public E_ResultStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                _createDate = DateTime.Now;
            }
        }

        public bool IsOK
        {
            get => _status == E_ResultStatus.OK;
            set
            {
                _status = value ? E_ResultStatus.OK : E_ResultStatus.ND;
                _createDate = DateTime.Now;
            }
        }

        public bool IsFail
        {
            get => _status == E_ResultStatus.FAIL;
            set
            {
                _status = value ? E_ResultStatus.FAIL : E_ResultStatus.ND;
                _createDate = DateTime.Now;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public C_EventArgs()
        {
        }

        public C_EventArgs(string message, E_ResultStatus status = E_ResultStatus.ND)
        {
            _status = status;
            _message = message ?? "";
        }

        public C_EventArgs(C_Error error, string message = null)
        {
            _status = E_ResultStatus.FAIL;
            _message = message ?? error?.Message ?? "";
        }

        public C_EventArgs(Exception exception, string message = null)
        {
            _status = E_ResultStatus.FAIL;
            _message = message ?? exception?.Message ?? "";
        }

        /// <summary>
        /// Copy constructor. Preserves status, message and CreateDate.
        /// <para><i>[ES] Constructor de copia. Conserva estado, mensaje y CreateDate.</i></para>
        /// </summary>
        public C_EventArgs(C_EventArgs? source)
        {
            if (source is null)
                return;
            this._status = source._status;
            this._message = source._message;
            this._createDate = source._createDate;
        }

        #endregion

        #region PUBLIC METHODS

        public C_EventArgs Clone()
            => new C_EventArgs(this);

        public string Info()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

    