namespace AutomaticTaskQueueLibrary
{
    using AutomaticTaskSharedLibrary;

    using InvoiceRepository;

    using RestServicesSupportTypes;

    public class DistributorToOperatorSendPointsTransfer : IDistributorToOperatorSendPointsTransfer
    {
        private readonly ISiteProcessorUrls siteProcessorUrls;

        private readonly IRestServicesFactory<DistributorToResellerSendPointsTransferRequestDto, DistributorToOperatorSendPointsTransferResponseDto> restFactory;

        public DistributorToOperatorSendPointsTransfer(ISiteProcessorUrls siteProcessorUrls, IRestServicesFactory<DistributorToResellerSendPointsTransferRequestDto, DistributorToOperatorSendPointsTransferResponseDto> restFactory)
        {
            this.siteProcessorUrls = siteProcessorUrls;
            this.restFactory = restFactory;
        }

        public async Task<DistributorToOperatorSendPointsTransferResponseDto> SendPointsTransfer(DistributorToResellerSendPointsTransferRequestDto requestDto)
        {
            var restClient = this.restFactory.Create(RestServicesEnum.external);
            var response = await restClient.Post(this.siteProcessorUrls.GetTransferPointsUrl(), requestDto);
            return response;
        }
    }
}