namespace AutomaticTaskLibrary
{
    using AutomaticTaskMessageLibrary;

    using InvoiceRepository;

    using InvoiceRepositoryTypes;

    public class VendorToOperatorSendPointsTransfer : IVendorToOperatorSendPointsTransfer
    {
        private readonly IPlaceMessageOnServiceBus placeMessageOnServiceBus;

        public VendorToOperatorSendPointsTransfer(IPlaceMessageOnServiceBus placeMessageOnServiceBus)
        {
            this.placeMessageOnServiceBus = placeMessageOnServiceBus;
        }

        async Task<VendorToOperatorSendPointsTransferResponse> IVendorToOperatorSendPointsTransfer.SendPointsTransfer(VendorToOperatorSendPointsTransferRequest request)
        {
            var placeMessageOnServiceBusRequest = new PlaceMessageOnServiceBusRequest(new AutomaticTaskTransferPoints() { VendorToOperatorSendPointsTransferRequest = request });
            var response = await this.placeMessageOnServiceBus.Send(placeMessageOnServiceBusRequest);
            return new VendorToOperatorSendPointsTransferResponse() { IsSuccessful = response.IsSuccessful };
        }
    }
}
