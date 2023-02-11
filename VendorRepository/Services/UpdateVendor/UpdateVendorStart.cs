namespace VendorRepository
{
    using DatabaseContext;

    using VendorRepositoryTypes;

    public class UpdateVendorStart : IUpdateVendor
    {
        Task<UpdateVendorResponse> IUpdateVendor.UpdateVendorAsync(DataContext dataContext, UpdateVendorRequest request)
        {
            return Task.FromResult(new UpdateVendorResponse() { IsSuccessful = true});
        }
    }
}
