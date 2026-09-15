using _1_LibraryClassesNet10.Interfaces;

namespace _1_LibraryClassesNet10.Classes
{
    public class C_Item<TId, TValue> : C_Entity<TId>
    {
        public TValue Value { get; set; } = default!;

        public C_Item() { }

        public C_Item(TId? id) : base(id) { }

        public C_Item(TId? id, string name, TValue value, string description = "")
            : base(id, name, description)
        {
            Value = value;
        }

        public C_Item(string name, TValue value, string description = "")
            : base(default, name, description)
        {
            Value = value;
        }

        public C_Item(C_Item<TId, TValue> source) : base(source)
        {
            Value = source.Value;
        }
    }

    public class C_Item<TValue> : C_Item<Guid, TValue>
    {
        public C_Item() : base(Guid.NewGuid()) { }

        public C_Item(string name, TValue value, string description = "")
            : base(Guid.NewGuid(), name, value, description) { }

        public C_Item(Guid? id, string name, TValue value, string description = "")
            : base(id ?? Guid.NewGuid(), name, value, description) { }

        public C_Item(C_Item<TValue> source) : base(source) { }
    }
}
