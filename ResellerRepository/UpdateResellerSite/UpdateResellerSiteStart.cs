namespace ResellerRepository;

using APISupportTypes;

using DatabaseContext;

using System.Net;

public class UpdateResellerSiteStart : IUpdateResellerSite
{
    Task<UpdateResellerSiteResponse> IUpdateResellerSite.UpdateResellerSiteAsync(DataContext context, UpdateResellerSiteRequest request)
    {
        return Task.FromResult(new UpdateResellerSiteResponse()
        {
            IsSuccessful = true,
            HttpStatusCode = HttpStatusCode.OK,
            ErrorMessage = string.Empty,
            ResponseTypeEnum = ResponseTypeEnum.success
        });
    }
}