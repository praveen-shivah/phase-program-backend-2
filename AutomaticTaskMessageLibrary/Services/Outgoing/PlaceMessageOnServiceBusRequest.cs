namespace AutomaticTaskMessageLibrary
{
    using NServiceBus;

    public class PlaceMessageOnServiceBusRequest
    {
        public PlaceMessageOnServiceBusRequest(ICommand command)
        {
            this.Command = command;
        }

        public ICommand Command { get; }
    }
}