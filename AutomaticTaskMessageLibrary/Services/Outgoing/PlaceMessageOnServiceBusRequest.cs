namespace AutomaticTaskMessageLibrary
{
    using AutomaticTaskSharedLibrary;

    public class PlaceMessageOnServiceBusRequest
    {
        public PlaceMessageOnServiceBusRequest(CallBackInformationRequest callBackInformationRequest)
        {
            this.CallBackInformationRequest = callBackInformationRequest;
        }

        public CallBackInformationRequest CallBackInformationRequest { get; }
    }
}