namespace TransferRepository;

using DatabaseContext;

public class TransferPointsQueueGetOutstandingItemsStart : ITransferPointsQueueGetOutstandingItems
{
    Task<TransferPointsQueueGetOutstandingItemsResponse> ITransferPointsQueueGetOutstandingItems.TransferPointsQueueGetOutstandingItemsAsync(
        DataContext context,
        TransferPointsQueueGetOutstandingItemsRequest request)
    {
        return Task.FromResult(new TransferPointsQueueGetOutstandingItemsResponse() { IsSuccessful = true });
    }
}