namespace AutomaticTaskQueueLibrary;

using DataPostgresqlLibrary;

using LoggingLibrary;

using Microsoft.EntityFrameworkCore;

public class AutomaticTaskQueueServiceProcessorPullRecord : IAutomaticTaskQueueServiceProcessor
{
    private readonly IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor;

    private readonly ILogger logger;

    public AutomaticTaskQueueServiceProcessorPullRecord(IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor, ILogger logger)
    {
        this.automaticTaskQueueServiceProcessor = automaticTaskQueueServiceProcessor;
        this.logger = logger;
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
        this.logger.Info(LogClass.CommRest, $"AutomaticTaskQueueServiceProcessorPullRecord pulled record Id: {response.QueueRecord.Id} ItemId: {response.QueueRecord.ItemId}");

        return response;
    }
}