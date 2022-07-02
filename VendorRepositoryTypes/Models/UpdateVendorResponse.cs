namespace VendorRepositoryTypes
{
    using DataModelsLibrary;

    public class UpdateVendorResponse
    {
        public bool IsSuccessful { get; set; }

        public Vendor Vendor { get; set; }
    }
}
