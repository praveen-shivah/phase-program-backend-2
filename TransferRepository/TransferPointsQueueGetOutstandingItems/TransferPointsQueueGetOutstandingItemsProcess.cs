namespace TransferRepository;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

public class TransferPointsQueueGetOutstandingItemsProcess : ITransferPointsQueueGetOutstandingItems
{
    private ITransferPointsQueueGetOutstandingItems transferPointsQueueGetOutstandingItems;

    public TransferPointsQueueGetOutstandingItemsProcess(ITransferPointsQueueGetOutstandingItems transferPointsQueueGetOutstandingItems)
    {
        this.transferPointsQueueGetOutstandingItems = transferPointsQueueGetOutstandingItems;
    }

    async Task<TransferPointsQueueGetOutstandingItemsResponse> ITransferPointsQueueGetOutstandingItems.TransferPointsQueueGetOutstandingItemsAsync(
        DataContext context,
        TransferPointsQueueGetOutstandingItemsRequest request)
    {
        var response = await this.transferPointsQueueGetOutstandingItems.TransferPointsQueueGetOutstandingItemsAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        response.Items = await context.TransferPointsQueue.Where(x=>x.OrganizationId == request.OrganizationId && x.DateTimeSent == null).ToListAsync();

        return response;
    }
}