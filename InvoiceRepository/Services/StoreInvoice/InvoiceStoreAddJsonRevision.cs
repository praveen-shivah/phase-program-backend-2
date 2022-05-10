namespace InvoiceRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreAddJsonRevision : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreAddJsonRevision(IInvoiceStore invoiceStore)
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

            var invoiceRevision = new InvoiceRevision()
            {
                InvoiceId = response.InvoiceRecord.Id,
                Invoice_Id = response.Invoice.InvoiceId,
                Json = request.JsonString
            };

            await dpContext.InvoiceRevision.AddAsync(invoiceRevision);

            return response;
        }
    }
}
