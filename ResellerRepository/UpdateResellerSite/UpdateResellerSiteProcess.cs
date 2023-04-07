namespace ResellerRepository;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

using System.Net;

using APISupportTypes;

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

        var record = request.IgnoreResellerId ?
                 await context.SiteInformation.SingleOrDefaultAsync(x => x.Id == request.Id) : 
                 await context.SiteInformation.SingleOrDefaultAsync(x => x.Id == request.Id && x.ResellerId == request.ResellerId);

        if (record == null)
        {
            response.IsSuccessful = false;
            response.ResponseTypeEnum = ResponseTypeEnum.idNotFound;
            response.ErrorMessage = $"Site information id {request.Id} for reseller id {request.ResellerId} not found.  ignoreResellerId set to {request.IgnoreResellerId}";
            response.HttpStatusCode = HttpStatusCode.BadRequest;

            return response;
        }

        record.AccountId = request.AccountId;
        record.LoginUsername = request.LoginUsername;

        if (request.LoginPassword != "******" || string.IsNullOrEmpty(request.LoginPassword))
        {
            record.LoginPassword = request.LoginPassword;
        }

        return response;
    }
}