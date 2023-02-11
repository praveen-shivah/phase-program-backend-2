namespace VendorRepository
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using VendorRepositoryTypes;

    public class UpdateVendorUpdate : IUpdateVendor
    {
        private readonly IUpdateVendor updateVendor;

        public UpdateVendorUpdate(IUpdateVendor updateVendor)
        {
            this.updateVendor = updateVendor;
        }

        async Task<UpdateVendorResponse> IUpdateVendor.UpdateVendorAsync(DataContext dataContext, UpdateVendorRequest request)
        {
            var response = await this.updateVendor.UpdateVendorAsync(dataContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var softwareType = await dataContext.SoftwareType.SingleAsync(x => x.Id == (int)request.VendorDto.SoftwareType);

            response.Vendor.IsActive = request.VendorDto.IsActive;
            response.Vendor.Name = request.VendorDto.Name;
            response.Vendor.SoftwareType = softwareType;

            return response;
        }
    }
}
