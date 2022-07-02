namespace VendorRepository
{
    using ApiDTO;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    using VendorRepositoryTypes;

    public class VendorRepository : IVendorRepository
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        private readonly IUpdateVendor updateVendor;

        public VendorRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory, IUpdateVendor updateVendor)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.updateVendor = updateVendor;
        }

        async Task<List<VendorDto>> IVendorRepository.GetVendors()
        {
            var result = new List<VendorDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var vendors = await context.Vendor.ToListAsync();
                        foreach (var vendor in vendors)
                        {
                            result.Add(
                                new VendorDto()
                                    {
                                        Id = vendor.Id,
                                        Name = vendor.Name,
                                        IsActive = vendor.IsActive
                                    });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<VendorDto>();
        }

        async Task<UpdateVendorResponse> IVendorRepository.UpdateVendorRequestAsync(VendorDto vendorDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.updateVendor.UpdateVendorAsync(context, new UpdateVendorRequest(vendorDto));

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();

            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new UpdateVendorResponse() { IsSuccessful = false };
            }

            return new UpdateVendorResponse() { IsSuccessful = true };
        }
    }
}
