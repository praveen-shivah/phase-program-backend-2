namespace DataModelsLibrary
{
    public class Reseller : BaseOrganizationEntity
    {
        public string Name { get; set; }

        public List<Vendor> Vendor { get; set; }

        public List<Invoice> Invoice { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
