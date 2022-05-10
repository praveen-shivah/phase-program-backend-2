namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public interface IAutomaticTaskMessageHandler
    {
        Task<bool> Execute(IAutomaticTask message);

        public AutomaticTaskType AutomaticTaskType { get; }
    }
}
