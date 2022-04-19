namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    public interface IInvoiceStore
    {
        Task<InvoiceStoreResponse> Store(DPContext dpContext, InvoiceStoreRequest request);
    }
}
