namespace VendorRepository
{
    using DataPostgresqlLibrary;

    using VendorRepositoryTypes;

    public interface IUpdateVendor
    {
        Task<UpdateVendorResponse> UpdateVendorAsync(DPContext dpContext, UpdateVendorRequest request);
    }
}
