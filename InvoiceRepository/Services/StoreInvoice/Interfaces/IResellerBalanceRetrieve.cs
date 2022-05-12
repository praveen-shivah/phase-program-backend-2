namespace InvoiceRepository
{
    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    public interface IResellerBalanceRetrieve
    {
        Task<ResellerBalanceRetrieveResponse> GetBalance(ResellerBalanceRetrieveRequest request);
    }
}
