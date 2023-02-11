namespace ResellerRepository;

using DatabaseContext;

public class UpdateResellerSiteStart : IUpdateResellerSite
{
    Task<UpdateResellerSiteResponse> IUpdateResellerSite.UpdateResellerSiteAsync(DataContext context, UpdateResellerSiteRequest request)
    {
        return Task.FromResult(new UpdateResellerSiteResponse() { IsSuccessful = true });
    }
}