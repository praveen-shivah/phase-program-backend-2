namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreSendTransferRequest : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        private readonly ISendPointsTransfer sendPointsTransfer;

        public InvoiceStoreSendTransferRequest(IInvoiceStore invoiceStore, ISendPointsTransfer sendPointsTransfer)
        {
            this.invoiceStore = invoiceStore;
            this.sendPointsTransfer = sendPointsTransfer;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            foreach (var invoiceLineItem in response.InvoiceRecord.LineItems)
            {
                this.sendPointsTransfer.SendPointsTransfer(
                    new SendPointsTransferRequest()
                        {
                            AccountId = int.Parse(invoiceLineItem.Description),
                            Points = invoiceLineItem.Quantity,
                            SiteId = int.Parse(invoiceLineItem.ItemId)
                        });
            }

            return response;
        }
    }
}
