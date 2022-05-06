namespace DataModelsLibrary
{
    public class PostalCode : BaseOrganizationEntity
    {
        public Country Country { get; set; }
        public string Code { get; set; }
    }
}
