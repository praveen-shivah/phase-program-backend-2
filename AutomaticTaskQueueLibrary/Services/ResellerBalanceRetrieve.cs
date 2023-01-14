namespace AutomaticTaskQueueLibrary;

using AutomaticTaskSharedLibrary;

using InvoiceRepository;

using InvoiceRepositoryTypes;

using RestServicesSupportTypes;

public class ResellerBalanceRetrieve : IResellerBalanceRetrieve
{
    private readonly ISiteProcessorUrls siteProcessorUrls;

    private readonly IRestServicesFactory<ResellerBalanceRetrieveRequestDto, ResellerBalanceRetrieveResponseDto> restFactory;

    public ResellerBalanceRetrieve(ISiteProcessorUrls siteProcessorUrls, IRestServicesFactory<ResellerBalanceRetrieveRequestDto, ResellerBalanceRetrieveResponseDto> restFactory)
    {
        this.siteProcessorUrls = siteProcessorUrls;
        this.restFactory = restFactory;
    }

    async Task<ResellerBalanceRetrieveResponseDto> IResellerBalanceRetrieve.GetBalance(ResellerBalanceRetrieveRequestDto requestDto)
    {
        var restClient = this.restFactory.Create(RestServicesEnum.external);
        var response = await restClient.Post(this.siteProcessorUrls.GetTransferPointsUrl(), requestDto);
        return response;
    }
}