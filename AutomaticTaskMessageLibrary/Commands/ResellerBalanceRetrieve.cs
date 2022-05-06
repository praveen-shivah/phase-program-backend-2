namespace AutomaticTaskLibrary
{
    using AutomaticTaskMessageLibrary;

    using InvoiceRepository;

    using InvoiceRepositoryTypes;

    public class ResellerBalanceRetrieve : IResellerBalanceRetrieve
    {
        private readonly IPlaceMessageOnServiceBus placeMessageOnServiceBus;

        public ResellerBalanceRetrieve(IPlaceMessageOnServiceBus placeMessageOnServiceBus)
        {
            this.placeMessageOnServiceBus = placeMessageOnServiceBus;
        }

        async Task<ResellerBalanceRetrieveResponse> IResellerBalanceRetrieve.GetBalance(ResellerBalanceRetrieveRequest vendorBalanceRetrieveRequest)
        {
            var placeMessageOnServiceBusRequest = new PlaceMessageOnServiceBusRequest(new AutomaticTaskResellerBalanceRetrieve() { VendorBalanceRetrieveRequest = vendorBalanceRetrieveRequest });
            var response = await this.placeMessageOnServiceBus.Send(placeMessageOnServiceBusRequest);
            return new ResellerBalanceRetrieveResponse() { IsSuccessful = response.IsSuccessful };
        }
    }
}
