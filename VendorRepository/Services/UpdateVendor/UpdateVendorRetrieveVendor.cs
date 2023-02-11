namespace VendorRepository
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using VendorRepositoryTypes;

    public class UpdateVendorRetrieveVendor : IUpdateVendor
    {
        private readonly IUpdateVendor updateVendor;

        public UpdateVendorRetrieveVendor(IUpdateVendor updateVendor)
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

            var vendor = await dataContext.Vendor.SingleOrDefaultAsync(x => x.Id == request.VendorDto.Id);
            if (vendor == null)
            {
                var softwareType = await dataContext.SoftwareType.SingleAsync(x => x.Id == (int)request.VendorDto.SoftwareType);

                vendor = new Vendor()
                {
                    IsActive = request.VendorDto.IsActive,
                    Name = request.VendorDto.Name,
                    SoftwareType = softwareType
                };
                dataContext.Vendor.Add(vendor);
                await dataContext.SaveChangesAsync();
            }

            response.Vendor = vendor;

            return response;
        }
    }
}
