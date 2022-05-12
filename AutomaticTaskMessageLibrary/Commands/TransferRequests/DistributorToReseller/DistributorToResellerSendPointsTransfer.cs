namespace AutomaticTaskLibrary
{
    using AutomaticTaskMessageLibrary;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepository;

    using InvoiceRepositoryTypes;

    public class DistributorToResellerSendPointsTransfer : IDistributorToOperatorSendPointsTransfer
    {
        private readonly IPlaceMessageOnServiceBus placeMessageOnServiceBus;

        public DistributorToResellerSendPointsTransfer(IPlaceMessageOnServiceBus placeMessageOnServiceBus)
        {
            this.placeMessageOnServiceBus = placeMessageOnServiceBus;
        }

        async Task<DistributorToOperatorSendPointsTransferResponse> IDistributorToOperatorSendPointsTransfer.SendPointsTransfer(DistributorToResellerSendPointsTransferRequest request)
        {
            var placeMessageOnServiceBusRequest = new PlaceMessageOnServiceBusRequest(new AutomaticTaskTransferPoints() { DistributorToResellerSendPointsTransferRequest = request });
            var response = await this.placeMessageOnServiceBus.Send(placeMessageOnServiceBusRequest);
            return new DistributorToOperatorSendPointsTransferResponse() { IsSuccessful = response.IsSuccessful };
        }
    }
}
