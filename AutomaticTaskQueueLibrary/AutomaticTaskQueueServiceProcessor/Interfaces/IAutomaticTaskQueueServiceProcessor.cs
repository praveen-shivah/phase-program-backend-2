namespace AutomaticTaskQueueLibrary;

using DatabaseContext;

public interface IAutomaticTaskQueueServiceProcessor
{
    Task<AutomaticTaskQueueServiceProcessorResponse> AutomaticTaskQueueServiceProcessorAsync(DataContext context, AutomaticTaskQueueServiceProcessorRequest request);
}