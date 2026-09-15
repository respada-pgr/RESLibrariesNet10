using System;

namespace _1_LibraryClassesNet10.Database
{
    public class C_Pagination
    {
        public enum E_Order { ND = -1, ASC = 0, DESC = 1 }

        private E_Order _order = E_Order.ND;
        private string _columnNameOrder;
        private int _currentPage = 0;          // Número de página que quieres mostrar
        private int _pageSize = 1;             // Número de registros por página        
        private int _totalRecords = 0;

        public E_Order Order { get => this._order; set => this._order = value; }
        public string ColumnNameOrder { get => this._columnNameOrder ?? ""; set => this._columnNameOrder = value; }
        public int PageSize { get => this._pageSize >= -1 ? _pageSize : -1; set => this._pageSize = value >= -1 ? value : -1; }
        public int TotalRecords
        {
            get => this._totalRecords > 0 ? this._totalRecords : 0;
            set
            {
                this._totalRecords = value > 0 ? value : 0;
                if (this._totalRecords > 0 && this._currentPage < 1)
                    this._currentPage = 1;

            }
        }
        public int TotalPages
        {
            get
            {
                int pages = (int)Math.Ceiling((double)_totalRecords / _pageSize);
                return pages > 0 ? pages : 0;
            }
        }
        public int CurrentPage
        {
            get => _currentPage > 0 ? _currentPage : 0;
            set
            {
                if (value > 0)
                {
                    this._currentPage = value <= this.TotalPages ? value : this.TotalPages;
                }
                else
                {
                    this._currentPage = this.TotalPages > 0 ? 1 : 0;
                }
            }
        }

        public C_Pagination()
        {
        }

        public C_Pagination(int pageSize)
        {
            this.PageSize = pageSize;
        }

        public C_Pagination(C_Pagination pagination)
        {
            this.PageSize = pagination.PageSize;
            this.TotalRecords = pagination.TotalRecords;
            this.CurrentPage = pagination.CurrentPage;
            this._order = pagination.Order;
            this._columnNameOrder = pagination.ColumnNameOrder;
        }

        public int GetNumRecordsToSkip()
        {
            return (this.CurrentPage - 1) * this.PageSize;
        }

        public bool Equals(C_Pagination obj)
        {
            return this.CurrentPage == obj.CurrentPage
                && this.TotalRecords == obj.TotalRecords
                && this.ColumnNameOrder == obj.ColumnNameOrder
                && this.Order == obj.Order
                && this.PageSize == obj.PageSize;
        }
    }
}
