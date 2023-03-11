namespace ResellerRepository;

using DatabaseContext;

public class UpdateResellerBalanceProcess : IUpdateResellerBalance
{
    private IUpdateResellerBalance updateResellerBalance;

    public UpdateResellerBalanceProcess(IUpdateResellerBalance updateResellerBalance)
    {
        this.updateResellerBalance = updateResellerBalance;
    }

    async Task<UpdateResellerBalanceResponse> IUpdateResellerBalance.UpdateResellerBalanceAsync(DataContext context, UpdateResellerBalanceRequest request)
    {
        var response = await this.updateResellerBalance.UpdateResellerBalanceAsync(context, request);
        if (!response.IsSuccessful || response.Reseller == null)
        {
            return response;
        }

        response.Reseller.Balance = request.Balance;

        return response;
    }
}