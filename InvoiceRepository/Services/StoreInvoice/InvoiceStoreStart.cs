namespace InvoiceRepository
{
    using DatabaseContext;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreStart : IInvoiceStore
    {
        Task<InvoiceStoreResponse> IInvoiceStore.Store(DataContext dataContext, InvoiceStoreRequest request)
        {
            return Task.FromResult(
                new InvoiceStoreResponse()
                {
                    IsSuccessful = true,
                    InvoiceStoreResponseType = InvoiceStoreResponseType.success
                });
        }
    }
}
