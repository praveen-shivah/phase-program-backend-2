namespace ResellerRepository;

using DatabaseContext;

public class ResellerSiteInformationPopulateStart : IResellerSiteInformationPopulate
{
    Task<ResellerSiteInformationPopulateResponse> IResellerSiteInformationPopulate.ResellerSiteInformationPopulateAsync(
        DataContext context,
        ResellerSiteInformationPopulateRequest request)
    {
        return Task.FromResult(new ResellerSiteInformationPopulateResponse() { IsSuccessful = true });
    }
}