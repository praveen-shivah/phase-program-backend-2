namespace VendorRepositoryTypes
{
    using DatabaseContext;

    public class UpdateVendorResponse
    {
        public bool IsSuccessful { get; set; }

        public Vendor Vendor { get; set; }
    }
}
