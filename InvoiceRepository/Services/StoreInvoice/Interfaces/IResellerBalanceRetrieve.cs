namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using InvoiceRepositoryTypes;

    public interface IResellerBalanceRetrieve
    {
        Task<ResellerBalanceRetrieveResponse> GetBalance(ResellerBalanceRetrieveRequest request);
    }
}
