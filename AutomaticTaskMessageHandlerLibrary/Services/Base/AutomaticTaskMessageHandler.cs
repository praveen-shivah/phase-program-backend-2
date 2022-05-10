namespace AutomaticTaskMessageHandlerHost
{
    using AutomaticTaskBrowserCommandProcessingLibrary;

    using AutomaticTaskLibrary;

    using NServiceBus;

    public class AutomaticTaskMessageHandler : IHandleMessages<IAutomaticTask>
    {
        private readonly Func<AutomaticTaskType, IAutomaticTaskMessageHandler?> handlerProvider;

        public AutomaticTaskMessageHandler(Func<AutomaticTaskType, IAutomaticTaskMessageHandler?> handlerProvider)
        {
            this.handlerProvider = handlerProvider;
        }

        public async Task Handle(IAutomaticTask automaticTask, IMessageHandlerContext context)
        {
            var automaticTaskMessageHandler = this.handlerProvider(automaticTask.AutomaticTaskType);
            if (automaticTaskMessageHandler == null) return;
            await automaticTaskMessageHandler.Execute(automaticTask);
        }
    }
}
