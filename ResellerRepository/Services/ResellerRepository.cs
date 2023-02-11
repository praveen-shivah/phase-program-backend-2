namespace ResellerRepository
{
    using ApiDTO;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using ResellerRepositoryTypes;

    using UnitOfWorkTypesLibrary;

    public class ResellerRepository : IResellerRepository
    {
        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        private readonly IUpdateReseller updateReseller;

        public ResellerRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IUpdateReseller updateReseller)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.updateReseller = updateReseller;
        }

        async Task<List<ResellerDto>> IResellerRepository.GetResellers(int organizationId)
        {
            var result = new List<ResellerDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var Resellers = await context.Reseller.ToListAsync();
                        result.Add(new ResellerDto() { IsPlaceHolder = true });
                        foreach (var Reseller in Resellers)
                        {
                            result.Add(
                                new ResellerDto()
                                {
                                    Id = Reseller.Id,
                                    Name = Reseller.Name,
                                });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<ResellerDto>();
        }

        async Task<List<SiteInformationDto>> IResellerRepository.GetResellerSites(int organizationId, int resellerId)
        {
            var result = new List<SiteInformationDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var siteInformationList = await context.SiteInformation.Include("Vendor").Where(x => x.ResellerId == resellerId).ToListAsync();
                        result.Add(new SiteInformationDto() { IsPlaceHolder = true });
                        foreach (var siteInformation in siteInformationList)
                        {
                            result.Add(
                                new SiteInformationDto()
                                {
                                    Id = siteInformation.Id,
                                    Description = siteInformation.Description,
                                    Item_Id = siteInformation.ItemId,
                                    Url = siteInformation.Url,
                                    AccountId = siteInformation.AccountId,
                                    VendorId = siteInformation.Vendor.Id.ToString(),
                                    Balance = siteInformation.Balance
                                });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<SiteInformationDto>();
        }

        async Task<UpdateResellerResponse> IResellerRepository.UpdateResellerRequestAsync(int organizationId, ResellerDto ResellerDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.updateReseller.UpdateResellerAsync(context, new UpdateResellerRequest(organizationId, ResellerDto));

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();

            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new UpdateResellerResponse() { IsSuccessful = false };
            }

            return new UpdateResellerResponse() { IsSuccessful = true };
        }
    }
}
