using System;

namespace _1_LibraryClassesNet10.Database
{
    public class C_RegBBDD_Nullable_Long
    {
        private long? _id = null;
        private string _name = "";
        private string _description = "";
        private DateTime _insertDate = new DateTime();
        private DateTime? _updateDate = null;
        private DateTime? _deleteDate = null;

        public long? Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public DateTime InsertDate { get { return _insertDate; } set { _insertDate = value; } }
        public DateTime? UpdateDate { get { return _updateDate; } set { _updateDate = value; } }
        public DateTime? DeleteDate { get { return _deleteDate; } set { _deleteDate = value; } }

        public bool Loaded { get { return this._name != "" && this._id != null; } }

        public C_RegBBDD_Nullable_Long()
        {
        }

        public C_RegBBDD_Nullable_Long(long? id, string name, string description, DateTime insertDate, DateTime? updateDate, DateTime? deleteDate)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.InsertDate = insertDate;
            this.UpdateDate = updateDate;
            this.DeleteDate = deleteDate;
        }

        public C_RegBBDD_Nullable_Long(C_RegBBDD_Int r)
        {
            this.Set(r);
        }

        public C_RegBBDD_Nullable_Long(C_RegBBDD_Long r)
        {
            this.Set(r);
        }

        public void Set(C_RegBBDD_Nullable_Int r)
        {
            if (r != null)
            {
                this.Id = r.Id;
                this.Name = r.Name;
                this.Description = r.Description;
                this.InsertDate = r.InsertDate;
                this.UpdateDate = r.UpdateDate;
                this.DeleteDate = r.DeleteDate;
            }
        }

        public void Set(C_RegBBDD_Nullable_Long r)
        {
            if (r != null)
            {
                this.Id = r.Id;
                this.Name = r.Name;
                this.Description = r.Description;
                this.InsertDate = r.InsertDate;
                this.UpdateDate = r.UpdateDate;
                this.DeleteDate = r.DeleteDate;
            }
        }
    }
}
