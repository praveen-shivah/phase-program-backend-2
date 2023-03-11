namespace ResellerRepository;

using System.Net;

using DatabaseContext;

using RestServicesSupportTypes;

public class UpdateResellerSiteValidateNewInformation : IUpdateResellerSite
{
    private IUpdateResellerSite updateResellerSite;

    public UpdateResellerSiteValidateNewInformation(IUpdateResellerSite updateResellerSite)
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

        if (request.Id < 0)
        {
            response.IsSuccessful = false;
            response.ResponseTypeEnum = ResponseTypeEnum.outOfRangeError;
            response.HttpStatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessage = "Site Id must be > 0";
            return response;
        }

        if (string.IsNullOrWhiteSpace(request.LoginPassword) || string.IsNullOrWhiteSpace(request.LoginUsername))
        {
            response.IsSuccessful = false;
            response.ResponseTypeEnum = ResponseTypeEnum.outOfRangeError;
            response.HttpStatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessage = "Neither LoginUsername nor LoginPassword may be blank";
            return response;
        }

        return response;
    }
}