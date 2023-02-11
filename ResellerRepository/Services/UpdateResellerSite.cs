namespace ResellerRepository;

using DatabaseContext;

using UnitOfWorkTypesLibrary;

public class UpdateResellerSiteRepository : IUpdateResellerSiteRepository
{
    private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

    private readonly IUpdateResellerSite updateResellerSite;

    public UpdateResellerSiteRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IUpdateResellerSite updateResellerSite)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.updateResellerSite = updateResellerSite;
    }

    async Task<UpdateResellerSiteResponse> IUpdateResellerSiteRepository.UpdateResellerSiteAsync(UpdateResellerSiteRequest request)
    {
        var result = new UpdateResellerSiteResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.updateResellerSite.UpdateResellerSiteAsync(context, request);
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