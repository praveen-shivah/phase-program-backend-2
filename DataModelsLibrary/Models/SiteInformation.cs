namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class SiteInformation : BaseEntity
    {
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
