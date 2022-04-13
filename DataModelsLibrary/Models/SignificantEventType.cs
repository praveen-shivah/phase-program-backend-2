namespace DataModelsLibrary
{
    using System.ComponentModel.DataAnnotations;

    using DataSharedLibrary;

    public class SignificantEventType : BaseEntity
    {
        public string Description { get; set; }
    }
}
