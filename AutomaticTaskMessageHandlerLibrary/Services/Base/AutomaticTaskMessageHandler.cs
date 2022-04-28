namespace AutomaticTaskMessageHandlerHost
{
    using AutomaticTaskBrowserCommandProcessingLibrary;

    using AutomaticTaskLibrary;

    using NServiceBus;

    public class AutomaticTaskMessageHandler : IHandleMessages<IAutomaticTask>
    {
        private readonly Dictionary<AutomaticTaskType, IAutomaticTaskMessageHandler> automaticTaskMessageHandlerDictionary = new();

        public AutomaticTaskMessageHandler(IAutomaticTaskMessageHandler[] automaticTaskMessageHandlers)
        {
            foreach(var automaticTaskMessageHandler in automaticTaskMessageHandlers)
            {
                this.automaticTaskMessageHandlerDictionary.Add(automaticTaskMessageHandler.AutomaticTaskType, automaticTaskMessageHandler);
            }
        }

        public async Task Handle(IAutomaticTask message, IMessageHandlerContext context)
        {
            if (this.automaticTaskMessageHandlerDictionary.TryGetValue(message.AutomaticTaskType, out var automaticTaskMessageHandler))
            {
                await automaticTaskMessageHandler.Execute(message);
            }
        }
    }
}
