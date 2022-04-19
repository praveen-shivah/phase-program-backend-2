namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class SiteInformation : BaseEntity
    {
        public string OrganizationId { get; set; }
        public string Item_Id { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}