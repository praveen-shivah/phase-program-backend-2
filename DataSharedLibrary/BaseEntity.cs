namespace DataSharedLibrary
{
    public abstract class BaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public int Id { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}