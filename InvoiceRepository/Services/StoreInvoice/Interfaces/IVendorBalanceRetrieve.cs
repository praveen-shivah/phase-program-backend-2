namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using InvoiceRepositoryTypes;

    public interface IVendorBalanceRetrieve
    {
        Task<VendorBalanceRetrieveResponse> GetBalance(VendorBalanceRetrieveRequest request);
    }
}
