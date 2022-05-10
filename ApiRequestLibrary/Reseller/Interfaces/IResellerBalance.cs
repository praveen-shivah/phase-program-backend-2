namespace ApiRequestLibrary
{
    using MobileRequestApiDTO;

    public interface IResellerBalance
    {
        Task<bool> UpdateBalance(ResellerBalanceDTO resellerBalance);
    }
}
