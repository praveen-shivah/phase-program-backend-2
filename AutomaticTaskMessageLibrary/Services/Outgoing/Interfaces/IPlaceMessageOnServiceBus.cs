namespace AutomaticTaskMessageLibrary
{
    public interface IPlaceMessageOnServiceBus
    {
        public Task<PlaceMessageOnServiceBusResponse> Send(PlaceMessageOnServiceBusRequest placeMessageOnServiceBusRequest);
    }
}
