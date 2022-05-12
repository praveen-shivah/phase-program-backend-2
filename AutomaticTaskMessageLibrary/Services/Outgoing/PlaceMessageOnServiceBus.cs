namespace AutomaticTaskMessageLibrary
{
    using AutomaticTaskSharedLibrary;

    using LoggingLibrary;

    using NServiceBus;

    public class PlaceMessageOnServiceBus : IPlaceMessageOnServiceBus
    {
        private readonly ILogger logger;

        private readonly IEndpointConfigurationFactory endpointConfigurationFactory;

        public PlaceMessageOnServiceBus(ILogger logger, IEndpointConfigurationFactory endpointConfigurationFactory)
        {
            this.logger = logger;
            this.endpointConfigurationFactory = endpointConfigurationFactory;
        }

        async Task<PlaceMessageOnServiceBusResponse> IPlaceMessageOnServiceBus.Send(PlaceMessageOnServiceBusRequest placeMessageOnServiceBusRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(placeMessageOnServiceBusRequest.CallBackInformationRequest.APIKey) || string.IsNullOrEmpty(placeMessageOnServiceBusRequest.CallBackInformationRequest.OrganizationId))
                {
                    throw new InvalidDataException("Organization Id and/or api key not supplied for request.");
                }

                var endpointConfiguration = this.endpointConfigurationFactory.CreateEndpointConfiguration(EndpointConfigurationConstants.QueueEndpoint, EndpointConfigurationConstants.HandlerEndpoint);
                var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
                await endpointInstance.Send(placeMessageOnServiceBusRequest.CallBackInformationRequest).ConfigureAwait(false);

                return new PlaceMessageOnServiceBusResponse() { IsSuccessful = true };
            }
            catch (Exception exception)
            {
                this.logger.Error(LogClass.CommRest, "PlaceMessageOnServiceBus", "Send", $"Error: {exception.Message}{exception.StackTrace}", exception);
            }

            return new PlaceMessageOnServiceBusResponse() { IsSuccessful = false };
        }
    }
}
