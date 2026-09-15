using System;

namespace _1_LibraryClassesNet10.Database
{
    public class C_RegBBDD_Int : C_RegBBDD_Nullable_Int
    {
        public new int Id { get { return base.Id ?? -1; } set { base.Id = value; } }

        public C_RegBBDD_Int() : base()
        {
        }

        public C_RegBBDD_Int(int id, string name, string description, DateTime insertDate, DateTime? updateDate, DateTime? deleteDate)
            : base(id, name, description, insertDate, updateDate, deleteDate)
        {

        }

        public C_RegBBDD_Int(C_RegBBDD_Int r) : base(r)
        {

        }

        public void Set(C_RegBBDD_Int r)
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
