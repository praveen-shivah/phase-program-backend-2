namespace VendorRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using VendorRepositoryTypes;

    public class UpdateVendorRetrieveVendor : IUpdateVendor
    {
        private readonly IUpdateVendor updateVendor;

        public UpdateVendorRetrieveVendor(IUpdateVendor updateVendor)
        {
            this.updateVendor = updateVendor;
        }

        async Task<UpdateVendorResponse> IUpdateVendor.UpdateVendorAsync(DPContext dpContext, UpdateVendorRequest request)
        {
            var response = await this.updateVendor.UpdateVendorAsync(dpContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var vendor = await dpContext.Vendor.SingleOrDefaultAsync(x => x.Id == request.VendorDto.Id);
            if (vendor == null)
            {
                var organization = await dpContext.Organization.SingleAsync(o => o.Id == request.VendorDto.OrganizationId);

                vendor = new Vendor()
                {
                    IsActive = request.VendorDto.IsActive,
                    Name = request.VendorDto.Name,
                    SoftwareType = request.VendorDto.SoftwareType,
                    Organization = organization
                };
                dpContext.Vendor.Add(vendor);
            }

            response.Vendor = vendor;

            return response;
        }
    }
}
