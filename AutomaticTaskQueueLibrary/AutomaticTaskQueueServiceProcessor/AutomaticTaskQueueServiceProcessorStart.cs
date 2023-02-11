namespace AutomaticTaskQueueLibrary;

using DatabaseContext;

public class AutomaticTaskQueueServiceProcessorStart : IAutomaticTaskQueueServiceProcessor
{
    Task<AutomaticTaskQueueServiceProcessorResponse> IAutomaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(
        DataContext context,
        AutomaticTaskQueueServiceProcessorRequest request)
    {
        return Task.FromResult(new AutomaticTaskQueueServiceProcessorResponse() { IsSuccessful = true });
    }
}