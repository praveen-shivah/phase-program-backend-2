namespace ResellerRepository;

using DatabaseContext;

public interface IResellerSiteInformationPopulate
{
    Task<ResellerSiteInformationPopulateResponse> ResellerSiteInformationPopulateAsync(DataContext context, ResellerSiteInformationPopulateRequest request);
}