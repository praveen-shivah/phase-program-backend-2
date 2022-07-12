namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class Vendor : BaseEntity
    {
        public string Name { get; set; }

        public SoftwareType SoftwareType { get; set; }

        public bool IsActive { get; set; }
    }
}
