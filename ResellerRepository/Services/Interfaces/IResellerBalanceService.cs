namespace ResellerRepository
{
    using ApiDTO;

    public interface IResellerBalanceService
    {
        Task<bool> UpdateBalance(ResellerBalanceDTO resellerBalance);
    }
}
