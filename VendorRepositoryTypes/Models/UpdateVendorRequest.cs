namespace VendorRepositoryTypes
{
    using ApiDTO;

    public class UpdateVendorRequest
    {
        public UpdateVendorRequest(VendorDto vendorDto)
        {
            this.VendorDto = vendorDto;
        }

        public VendorDto VendorDto { get; }
    }
}