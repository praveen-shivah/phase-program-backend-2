namespace ResellerRepository;

using DatabaseContext;

public interface IUpdateResellerBalance
{
    Task<UpdateResellerBalanceResponse> UpdateResellerBalanceAsync(DataContext context, UpdateResellerBalanceRequest request);
}