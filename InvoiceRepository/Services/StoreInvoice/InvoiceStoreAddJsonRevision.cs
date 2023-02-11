namespace InvoiceRepository
{
    using DatabaseContext;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreAddJsonRevision : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreAddJsonRevision(IInvoiceStore invoiceStore)
        {
            this.invoiceStore = invoiceStore;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DataContext dataContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dataContext, request);
            if (!response.IsSuccessful || response.Organization == null)
            {
                return response;
            }

            var invoiceRevision = new InvoiceRevision()
            {
                Organization = response.Organization,
                ResellerId = response.Invoice.CfResellerId,
                InvoiceId = response.InvoiceRecord.Id,
                InvoiceId1 = response.Invoice.InvoiceId,
                Json = request.JsonString
            };

            await dataContext.InvoiceRevision.AddAsync(invoiceRevision);

            return response;
        }
    }
}
