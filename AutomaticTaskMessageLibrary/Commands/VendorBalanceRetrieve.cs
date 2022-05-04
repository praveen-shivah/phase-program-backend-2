namespace AutomaticTaskLibrary
{
    using AutomaticTaskMessageLibrary;

    using InvoiceRepository;

    using InvoiceRepositoryTypes;

    public class VendorBalanceRetrieve : IVendorBalanceRetrieve
    {
        private readonly IPlaceMessageOnServiceBus placeMessageOnServiceBus;

        public VendorBalanceRetrieve(IPlaceMessageOnServiceBus placeMessageOnServiceBus)
        {
            this.placeMessageOnServiceBus = placeMessageOnServiceBus;
        }

        async Task<VendorBalanceRetrieveResponse> IVendorBalanceRetrieve.GetBalance(VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest)
        {
            var placeMessageOnServiceBusRequest = new PlaceMessageOnServiceBusRequest(new AutomaticTaskVendorBalanceRetrieve() { VendorBalanceRetrieveRequest = vendorBalanceRetrieveRequest });
            var response = await this.placeMessageOnServiceBus.Send(placeMessageOnServiceBusRequest);
            return new VendorBalanceRetrieveResponse() { IsSuccessful = response.IsSuccessful };
        }
    }
}
