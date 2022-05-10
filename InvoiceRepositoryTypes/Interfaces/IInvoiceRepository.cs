namespace InvoiceRepositoryTypes
{
    public interface IInvoiceRepository
    {
        Task<InvoiceStoreResponse> Store(InvoiceStoreRequest request);
    }
}
