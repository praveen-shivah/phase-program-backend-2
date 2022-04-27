namespace AutomaticTaskMessageHandlerLibrary
{
    using AutomaticTaskLibrary;

    using NServiceBus;

    public class AutomaticTaskMessageHandler : IHandleMessages<IAutomaticTask>
    {
        public Task Handle(IAutomaticTask message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
