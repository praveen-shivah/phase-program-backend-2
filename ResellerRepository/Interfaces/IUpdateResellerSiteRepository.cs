namespace ResellerRepository;

public interface IUpdateResellerSiteRepository
{
    Task<UpdateResellerSiteResponse> UpdateResellerSiteAsync(UpdateResellerSiteRequest request);
}