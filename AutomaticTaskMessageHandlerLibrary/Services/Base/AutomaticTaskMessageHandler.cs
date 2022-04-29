namespace AutomaticTaskMessageHandlerHost
{
    using AutomaticTaskBrowserCommandProcessingLibrary;

    using AutomaticTaskLibrary;

    using NServiceBus;

    public class AutomaticTaskMessageHandler : IHandleMessages<IAutomaticTask>
    {
        //private readonly Dictionary<AutomaticTaskType, IAutomaticTaskMessageHandler> automaticTaskMessageHandlerDictionary = new();

        //public AutomaticTaskMessageHandler(IAutomaticTaskMessageHandler[] automaticTaskMessageHandlers)
        //{
        //    foreach(var automaticTaskMessageHandler in automaticTaskMessageHandlers)
        //    {
        //        this.automaticTaskMessageHandlerDictionary.Add(automaticTaskMessageHandler.AutomaticTaskType, automaticTaskMessageHandler);
        //    }
        //}

        public Task Handle(IAutomaticTask automaticTask, IMessageHandlerContext context)
        {
            return Task.CompletedTask;

            //if (this.automaticTaskMessageHandlerDictionary.TryGetValue(automaticTask.AutomaticTaskType, out var automaticTaskMessageHandler))
            //{
            //    await automaticTaskMessageHandler.Execute(automaticTask);
            //}
        }
    }
}
