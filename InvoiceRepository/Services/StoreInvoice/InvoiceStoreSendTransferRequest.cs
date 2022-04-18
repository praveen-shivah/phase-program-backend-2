namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreSendTransferRequest : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreSendTransferRequest(IInvoiceStore invoiceStore)
        {
            this.invoiceStore = invoiceStore;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            return response;
        }
    }
}
