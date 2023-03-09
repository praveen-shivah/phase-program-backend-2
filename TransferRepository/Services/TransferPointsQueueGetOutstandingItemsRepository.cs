namespace TransferRepository;

using DatabaseContext;

using UnitOfWorkTypesLibrary;

public class TransferPointsQueueGetOutstandingItemsRepository : ITransferPointsQueueGetOutstandingItemsRepository
{
    private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

    private readonly ITransferPointsQueueGetOutstandingItems transferPointsQueueGetOutstandingItems;

    public TransferPointsQueueGetOutstandingItemsRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, ITransferPointsQueueGetOutstandingItems transferPointsQueueGetOutstandingItems)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.transferPointsQueueGetOutstandingItems = transferPointsQueueGetOutstandingItems;
    }

    async Task<TransferPointsQueueGetOutstandingItemsResponse> ITransferPointsQueueGetOutstandingItemsRepository.TransferPointsQueueGetOutstandingItemsAsync(
        TransferPointsQueueGetOutstandingItemsRequest request)
    {
        var result = new TransferPointsQueueGetOutstandingItemsResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.transferPointsQueueGetOutstandingItems.TransferPointsQueueGetOutstandingItemsAsync(context, request);
                    result.IsSuccessful = response.IsSuccessful;
                    result.Items = response.Items;

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