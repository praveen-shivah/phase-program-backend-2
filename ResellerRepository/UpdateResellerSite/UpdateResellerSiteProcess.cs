namespace ResellerRepository;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

public class UpdateResellerSiteProcess : IUpdateResellerSite
{
    private readonly IUpdateResellerSite updateResellerSite;

    public UpdateResellerSiteProcess(IUpdateResellerSite updateResellerSite)
    {
        this.updateResellerSite = updateResellerSite;
    }

    async Task<UpdateResellerSiteResponse> IUpdateResellerSite.UpdateResellerSiteAsync(DataContext context, UpdateResellerSiteRequest request)
    {
        var response = await this.updateResellerSite.UpdateResellerSiteAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        var record = await context.SiteInformation.SingleOrDefaultAsync(x => x.Id == request.Id);
        if (record == null)
        {
            response.IsSuccessful = false;
            return response;
        }

        record.AccountId = request.AccountId;

        return response;
    }
}