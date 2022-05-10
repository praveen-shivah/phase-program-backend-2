namespace DataModelsLibrary
{
    using AutomaticTaskLibrary;

    public class Vendor : BaseOrganizationEntity
    {
        public string Name { get; set; }

        public SoftwareType SoftwareType { get; set; }
    }
}
