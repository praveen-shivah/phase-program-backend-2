namespace ResellerRepository;

using DatabaseContext;

public interface IUpdateResellerSite
{
    Task<UpdateResellerSiteResponse> UpdateResellerSiteAsync(DataContext context, UpdateResellerSiteRequest request);
}