namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreUpdateInvoice : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreUpdateInvoice(IInvoiceStore invoiceStore)
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

            response.InvoiceRecord.Balance = response.Invoice.Balance;
            response.InvoiceRecord.BalanceFormatted = response.Invoice.BalanceFormatted;
            response.InvoiceRecord.Status = response.Invoice.Status;
            response.InvoiceRecord.StatusFormatted = response.Invoice.StatusFormatted;



            return response;
        }
    }
}
