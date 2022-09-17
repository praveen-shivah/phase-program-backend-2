namespace AutomaticTaskQueueLibrary;

using DataPostgresqlLibrary;

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


        response.QueueRecord = context.TransferPointsQueue.SingleOrDefault(x => x.DateTimeSent == null && x.DateTimeProcessStarted == null);
        return response;
    }
}