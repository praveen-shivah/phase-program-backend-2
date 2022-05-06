namespace DataModelsLibrary
{
    public class Contact : BaseOrganizationEntity
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string MiddleName { get; set; }
        public List<Address> AddressList { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
