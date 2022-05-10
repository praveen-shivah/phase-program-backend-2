namespace ResellerRepository
{
    using MobileRequestApiDTO;

    public interface IResellerBalanceService
    {
        Task<bool> UpdateBalance(ResellerBalanceDTO resellerBalance);
    }
}
