namespace ResellerRepository;

using DatabaseContext;

using UnitOfWorkTypesLibrary;

public class ResellerSiteInformationPopulateRepository : IResellerSiteInformationPopulateRepository
{
    private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

    private readonly IResellerSiteInformationPopulate resellerSiteInformationPopulate;

    public ResellerSiteInformationPopulateRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IResellerSiteInformationPopulate resellerSiteInformationPopulate)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.resellerSiteInformationPopulate = resellerSiteInformationPopulate;
    }

    async Task<ResellerSiteInformationPopulateResponse> IResellerSiteInformationPopulateRepository.ResellerSiteInformationPopulateAsync(ResellerSiteInformationPopulateRequest request)
    {
        var result = new ResellerSiteInformationPopulateResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.resellerSiteInformationPopulate.ResellerSiteInformationPopulateAsync(context, request);
                    result.IsSuccessful = response.IsSuccessful;

                    if (response.IsSuccessful)
                    {
                        return WorkItemResultEnum.commitSuccessfullyCompleted;
                    }
                    else
                    {
                        return WorkItemResultEnum.rollbackExit;
                    }
                });
        var uowResult = await uow.ExecuteAsync();
        if (uowResult != WorkItemResultEnum.commitSuccessfullyCompleted)
        {
            result.IsSuccessful = false;
        }

        return result;
    }
}