namespace DataModelsLibrary
{
    using System.ComponentModel.DataAnnotations;

    using DataSharedLibrary;

    public class SignificantEvent : BaseEntity
    {
        public int EventTypeId { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
