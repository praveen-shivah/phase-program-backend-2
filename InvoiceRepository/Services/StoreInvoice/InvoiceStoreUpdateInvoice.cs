namespace InvoiceRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

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
            if (!response.IsSuccessful || response.Organization == null)
            {
                return response;
            }

            response.InvoiceRecord.Balance = response.Invoice.Balance;
            response.InvoiceRecord.BalanceFormatted = response.Invoice.BalanceFormatted;
            response.InvoiceRecord.Status = response.Invoice.Status;
            response.InvoiceRecord.StatusFormatted = response.Invoice.StatusFormatted;

            var lineItems = await dpContext.InvoiceLineItem.Where(x => x.InvoiceId == response.InvoiceRecord.Id).ToListAsync();
            dpContext.InvoiceLineItem.RemoveRange(lineItems);
            await dpContext.SaveChangesAsync();

            foreach (var item in response.Invoice.LineItems)
            {
                var invoiceLineItemRecord = new InvoiceLineItem()
                {
                    Organization = response.Organization,
                    InvoiceId = response.InvoiceRecord.Id,
                    ItemId = item.ItemId,
                    Description = item.Description,
                    Quantity = item.Quantity
                };

                await dpContext.InvoiceLineItem.AddAsync(invoiceLineItemRecord);
            }

            return response;
        }
    }
}
