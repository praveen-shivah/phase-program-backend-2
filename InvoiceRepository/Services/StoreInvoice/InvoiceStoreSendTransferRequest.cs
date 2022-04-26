namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    internal class InvoiceStoreSendTransferRequest : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        private readonly IVendorToOperatorSendPointsTransfer vendorToOperatorSendPointsTransfer;

        public InvoiceStoreSendTransferRequest(IInvoiceStore invoiceStore, IVendorToOperatorSendPointsTransfer vendorToOperatorSendPointsTransfer)
        {
            this.invoiceStore = invoiceStore;
            this.vendorToOperatorSendPointsTransfer = vendorToOperatorSendPointsTransfer;
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
                this.vendorToOperatorSendPointsTransfer.SendPointsTransfer(
                    new VendorToOperatorSendPointsTransferRequest()
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
