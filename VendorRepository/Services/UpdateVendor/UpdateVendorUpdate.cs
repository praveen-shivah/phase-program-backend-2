namespace VendorRepository
{
    using DataPostgresqlLibrary;

    using VendorRepositoryTypes;

    public class UpdateVendorUpdate : IUpdateVendor
    {
        private readonly IUpdateVendor updateVendor;

        public UpdateVendorUpdate(IUpdateVendor updateVendor)
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

            response.Vendor.IsActive = request.VendorDto.IsActive;
            response.Vendor.Name = request.VendorDto.Name;
            response.Vendor.SoftwareType = request.VendorDto.SoftwareType;

            return response;
        }
    }
}
