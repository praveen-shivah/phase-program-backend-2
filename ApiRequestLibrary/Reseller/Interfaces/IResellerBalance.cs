namespace ApiRequestLibrary
{
    using ApiDTO;

    public interface IResellerBalance
    {
        Task<bool> UpdateBalance(ResellerBalanceDTO resellerBalance);
    }
}
