namespace DataModelsLibrary
{
    public class Address : BaseOrganizationEntity
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public City City { get; set; }
        public StateProvince StateProvince { get; set; }
        public PostalCode PostalCode { get; set; }
    }
}
