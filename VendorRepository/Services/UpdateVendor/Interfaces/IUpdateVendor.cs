namespace VendorRepository
{
    using DatabaseContext;

    using VendorRepositoryTypes;

    public interface IUpdateVendor
    {
        Task<UpdateVendorResponse> UpdateVendorAsync(DataContext dataContext, UpdateVendorRequest request);
    }
}
