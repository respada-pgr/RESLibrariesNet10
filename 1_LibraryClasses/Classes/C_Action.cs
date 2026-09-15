using System;
using _1_LibraryClassesNet10.Enums;

namespace _1_LibraryClassesNet10.Classes
{
    public class C_Action<TId> : C_Entity<TId>
    {
        public E_Action Action { get; set; } = E_Action.UNDEFINED;
        public DateTime Date { get; init; } = DateTime.UtcNow;

        public C_Action() { }

        public C_Action(TId? id, E_Action action = E_Action.UNDEFINED) : base(id)
        {
            Action = action;
        }

        public C_Action(string name, E_Action action = E_Action.UNDEFINED, string description = "")
            : base(default, name, description)
        {
            Action = action;
        }

        public C_Action(TId? id, string name, E_Action action = E_Action.UNDEFINED, string description = "")
            : base(id, name, description)
        {
            Action = action;
        }

        public C_Action(C_Action<TId> source) : base(source)
        {
            Action = source.Action;
            Date = source.Date;
        }
    }

    public class C_Action : C_Action<Guid>
    {
        public C_Action() : base(Guid.NewGuid()) { }

        public C_Action(E_Action action) : base(Guid.NewGuid(), action) { }

        public C_Action(string name, E_Action action = E_Action.UNDEFINED, string description = "")
            : base(Guid.NewGuid(), name, action, description) { }

        public C_Action(Guid? id, string name, E_Action action = E_Action.UNDEFINED, string description = "")
            : base(id ?? Guid.NewGuid(), name, action, description) { }

        public C_Action(C_Action source) : base(source) { }
    }
}