namespace ResellerRepository;

public interface IUpdateResellerBalanceRepository
{
    Task<UpdateResellerBalanceResponse> UpdateResellerBalanceAsync(UpdateResellerBalanceRequest request);
}