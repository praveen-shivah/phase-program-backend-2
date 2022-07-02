namespace VendorRepository
{
    using DataPostgresqlLibrary;

    using VendorRepositoryTypes;

    public class UpdateVendorStart : IUpdateVendor
    {
        Task<UpdateVendorResponse> IUpdateVendor.UpdateVendorAsync(DPContext dpContext, UpdateVendorRequest request)
        {
            return Task.FromResult(new UpdateVendorResponse() { IsSuccessful = true});
        }
    }
}
