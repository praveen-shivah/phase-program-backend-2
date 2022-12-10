namespace ResellerRepository;

using DataPostgresqlLibrary;

public interface IUpdateResellerSite
{
    Task<UpdateResellerSiteResponse> UpdateResellerSiteAsync(DPContext context, UpdateResellerSiteRequest request);
}