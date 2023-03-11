namespace ResellerRepository;

using System.Net;

using DatabaseContext;

using RestServicesSupportTypes;

public class UpdateResellerBalanceStart : IUpdateResellerBalance
{
    Task<UpdateResellerBalanceResponse> IUpdateResellerBalance.UpdateResellerBalanceAsync(DataContext context, UpdateResellerBalanceRequest request)
    {
        return Task.FromResult(new UpdateResellerBalanceResponse()
        {
            IsSuccessful = true,
            HttpStatusCode = HttpStatusCode.OK,
            ResponseTypeEnum = ResponseTypeEnum.success,
            ErrorMessage = string.Empty
        });
    }
}