namespace AutomaticTaskQueueLibrary;

using DataPostgresqlLibrary;

public interface IAutomaticTaskQueueServiceProcessor
{
    Task<AutomaticTaskQueueServiceProcessorResponse> AutomaticTaskQueueServiceProcessorAsync(DPContext context, AutomaticTaskQueueServiceProcessorRequest request);
}