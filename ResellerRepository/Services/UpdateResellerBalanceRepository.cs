namespace ResellerRepository;

using DatabaseContext;

using UnitOfWorkTypesLibrary;

public class UpdateResellerBalanceRepository : IUpdateResellerBalanceRepository
{
    private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

    private readonly IUpdateResellerBalance updateResellerBalance;

    public UpdateResellerBalanceRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IUpdateResellerBalance updateResellerBalance)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.updateResellerBalance = updateResellerBalance;
    }

    async Task<UpdateResellerBalanceResponse> IUpdateResellerBalanceRepository.UpdateResellerBalanceAsync(UpdateResellerBalanceRequest request)
    {
        var result = new UpdateResellerBalanceResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.updateResellerBalance.UpdateResellerBalanceAsync(context, request);
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