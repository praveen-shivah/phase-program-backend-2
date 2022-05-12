namespace AutomaticTaskLibrary
{
    using AutomaticTaskMessageLibrary;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepository;

    using InvoiceRepositoryTypes;

    public class ResellerBalanceRetrieve : IResellerBalanceRetrieve
    {
        private readonly IPlaceMessageOnServiceBus placeMessageOnServiceBus;

        public ResellerBalanceRetrieve(IPlaceMessageOnServiceBus placeMessageOnServiceBus)
        {
            this.placeMessageOnServiceBus = placeMessageOnServiceBus;
        }

        async Task<ResellerBalanceRetrieveResponse> IResellerBalanceRetrieve.GetBalance(ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var placeMessageOnServiceBusRequest = new PlaceMessageOnServiceBusRequest(new AutomaticTaskResellerBalanceRetrieve
                                                                                          {
                                                                                              ResellerBalanceRetrieveRequest = resellerBalanceRetrieveRequest,
                                                                                              OrganizationId = resellerBalanceRetrieveRequest.OrganizationId,
                                                                                              APIKey = resellerBalanceRetrieveRequest.ApiKey
                                                                                          });

            var response = await this.placeMessageOnServiceBus.Send(placeMessageOnServiceBusRequest);
            return new ResellerBalanceRetrieveResponse { IsSuccessful = response.IsSuccessful };
        }
    }
}