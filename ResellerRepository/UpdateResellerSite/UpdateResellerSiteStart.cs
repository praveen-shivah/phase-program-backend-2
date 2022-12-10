namespace ResellerRepository;

using DataPostgresqlLibrary;

public class UpdateResellerSiteStart : IUpdateResellerSite
{
    Task<UpdateResellerSiteResponse> IUpdateResellerSite.UpdateResellerSiteAsync(DPContext context, UpdateResellerSiteRequest request)
    {
        return Task.FromResult(new UpdateResellerSiteResponse() { IsSuccessful = true });
    }
}