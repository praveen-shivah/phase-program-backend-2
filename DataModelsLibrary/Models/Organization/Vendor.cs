namespace DataModelsLibrary
{
    using AutomaticTaskSharedLibrary;

    public class Vendor : BaseOrganizationEntity
    {
        public string Name { get; set; }

        public SoftwareType SoftwareType { get; set; }
    }
}
