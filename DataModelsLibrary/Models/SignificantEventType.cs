namespace DataModelsLibrary
{
    using System.ComponentModel.DataAnnotations;

    using DataSharedLibrary;

    public class SignificantEventType : BaseEntity
    {
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
