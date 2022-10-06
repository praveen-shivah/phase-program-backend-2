namespace AutomaticTaskQueueLibrary;

using DataPostgresqlLibrary;

public class AutomaticTaskQueueServiceProcessorStart : IAutomaticTaskQueueServiceProcessor
{
    Task<AutomaticTaskQueueServiceProcessorResponse> IAutomaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(
        DPContext context,
        AutomaticTaskQueueServiceProcessorRequest request)
    {
        return Task.FromResult(new AutomaticTaskQueueServiceProcessorResponse() { IsSuccessful = true });
    }
}