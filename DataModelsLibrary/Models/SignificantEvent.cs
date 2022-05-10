namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class SignificantEvent : BaseEntity
    {
        public int CreatedBy { get; set; }

        public int EventTypeId { get; set; }

        public string LongDescription { get; set; }

        public string ShortDescription { get; set; }
    }
}