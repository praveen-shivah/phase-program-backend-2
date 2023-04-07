namespace ResellerRepository;

using DatabaseContext;

using System.Net;

using APISupportTypes;

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