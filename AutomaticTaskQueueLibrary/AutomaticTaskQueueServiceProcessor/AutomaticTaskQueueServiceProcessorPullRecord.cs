namespace AutomaticTaskQueueLibrary;

using DataPostgresqlLibrary;

using Microsoft.EntityFrameworkCore;

public class AutomaticTaskQueueServiceProcessorPullRecord : IAutomaticTaskQueueServiceProcessor
{
    private IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor;

    public AutomaticTaskQueueServiceProcessorPullRecord(IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor)
    {
        this.automaticTaskQueueServiceProcessor = automaticTaskQueueServiceProcessor;
    }

    async Task<AutomaticTaskQueueServiceProcessorResponse> IAutomaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(
        DPContext context,
        AutomaticTaskQueueServiceProcessorRequest request)
    {
        var response = await this.automaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }


        response.QueueRecord = await context.TransferPointsQueue.Include(x => x.Organization).FirstOrDefaultAsync(x => x.DateTimeSent == null && x.DateTimeProcessStarted == null && !string.IsNullOrWhiteSpace(x.AccountId));
        if (response.QueueRecord == null)
        {
            return response;
        }

        response.InvoiceLineItemRecord = await context.InvoiceLineItem.SingleOrDefaultAsync(x => x.ItemId == response.QueueRecord.ItemId);

        return response;
    }
}