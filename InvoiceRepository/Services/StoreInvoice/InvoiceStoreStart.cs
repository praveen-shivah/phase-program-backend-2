namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreStart : IInvoiceStore
    {
        Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
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
