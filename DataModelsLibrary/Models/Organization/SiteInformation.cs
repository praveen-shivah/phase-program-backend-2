namespace DataModelsLibrary
{
    public class SiteInformation : BaseOrganizationEntity
    {
        public string Item_Id { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string AccountId { get; set; }
        public int ResellerId { get; set; }
        public Vendor Vendor { get; set; }
    }
}