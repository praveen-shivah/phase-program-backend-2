namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class Vendor : BaseEntity
    {
        public string Name { get; set; }

        public int SoftwareTypeId { get; set; }

        public SoftwareType SoftwareType { get; set; }

        public bool IsActive { get; set; }

        public List<VendorCredentialsByOrganization> VendorCredentialsByOrganizations { get; set; }
    }
}
