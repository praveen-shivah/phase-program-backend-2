namespace ResellerRepository;

using APISupportTypes;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

using System.Net;

public class UpdateResellerBalanceRetrieveReseller : IUpdateResellerBalance
{
    private IUpdateResellerBalance updateResellerBalance;

    public UpdateResellerBalanceRetrieveReseller(IUpdateResellerBalance updateResellerBalance)
    {
        this.updateResellerBalance = updateResellerBalance;
    }

    async Task<UpdateResellerBalanceResponse> IUpdateResellerBalance.UpdateResellerBalanceAsync(DataContext context, UpdateResellerBalanceRequest request)
    {
        var response = await this.updateResellerBalance.UpdateResellerBalanceAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        response.Reseller = await context.Reseller.SingleOrDefaultAsync(x => x.OrganizationId == request.OrganizationId && x.Id == request.ResellerId);
        if (response.Reseller != null)
        {
            return response;
        }

        response.ResponseTypeEnum = ResponseTypeEnum.idNotFound;
        response.ErrorMessage = $"ResellerId {request.ResellerId} not found";
        response.HttpStatusCode = HttpStatusCode.BadRequest;
        response.IsSuccessful = false;

        return response;
    }
}