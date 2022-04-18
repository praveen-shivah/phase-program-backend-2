namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal interface IInvoiceStore
    {
        Task<InvoiceStoreResponse> Store(DPContext dpContext, InvoiceStoreRequest request);
    }
}
