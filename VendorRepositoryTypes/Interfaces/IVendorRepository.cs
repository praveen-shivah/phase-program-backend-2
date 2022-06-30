namespace VendorRepositoryTypes
{
    using ApiDTO;

    public interface IVendorRepository
    {
        Task<List<VendorDto>> GetVendors();

        Task<UpdateVendorResponse> UpdateVendorRequestAsync(VendorDto vendorDto);
    }
}
