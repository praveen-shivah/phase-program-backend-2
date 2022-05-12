namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    public interface IAutomaticTaskMessageHandler
    {
        Task<bool> Execute(IAutomaticTask message);

        public AutomaticTaskType AutomaticTaskType { get; }
    }
}
