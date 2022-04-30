namespace AutomaticTaskMessageLibrary
{
    using AutomaticTaskLibrary;

    public class PlaceMessageOnServiceBusRequest
    {
        public PlaceMessageOnServiceBusRequest(IAutomaticTask automaticTask)
        {
            this.AutomaticTask = automaticTask;
        }

        public IAutomaticTask AutomaticTask { get; }
    }
}