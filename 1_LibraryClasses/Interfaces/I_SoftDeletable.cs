namespace _1_LibraryClassesNet10.Interfaces
{
    public interface I_SoftDeletable
    {
        bool IsDeleted { get; }
        DateTime? DeletedAt { get; }
    }
}
