namespace ResellerRepository;

using DatabaseContext;

public class ResellerSiteInformationPopulateProcess : IResellerSiteInformationPopulate
{
    private IResellerSiteInformationPopulate resellerSiteInformationPopulate;

    public ResellerSiteInformationPopulateProcess(IResellerSiteInformationPopulate resellerSiteInformationPopulate)
    {
        this.resellerSiteInformationPopulate = resellerSiteInformationPopulate;
    }

    async Task<ResellerSiteInformationPopulateResponse> IResellerSiteInformationPopulate.ResellerSiteInformationPopulateAsync(
        DataContext context,
        ResellerSiteInformationPopulateRequest request)
    {
        var response = await this.resellerSiteInformationPopulate.ResellerSiteInformationPopulateAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        return response;
    }
}