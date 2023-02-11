namespace InvoiceRepository
{
    using DatabaseContext;

    using InvoiceRepositoryTypes;

    public interface IInvoiceStore
    {
        Task<InvoiceStoreResponse> Store(DataContext dataContext, InvoiceStoreRequest request);
    }
}
