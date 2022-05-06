namespace DataModelsLibrary
{
    public class ResellerVendorBalance : BaseOrganizationEntity
    {
        public int Balance { get; set; }

        public DateTime LastUpdated { get; set; }

        public Vendor Vendor { get; set; }
    }
}
