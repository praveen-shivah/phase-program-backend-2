namespace DataModelsLibrary
{
    using ApiDTO;

    public class Vendor : BaseOrganizationEntity
    {
        public string Name { get; set; }

        public SoftwareType SoftwareType { get; set; }

        public bool IsActive { get; set; }
    }
}
