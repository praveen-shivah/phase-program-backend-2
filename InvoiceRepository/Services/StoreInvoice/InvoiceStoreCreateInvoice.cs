namespace InvoiceRepository
{
    using System.Security.Cryptography.X509Certificates;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

    internal class InvoiceStoreCreateInvoice : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreCreateInvoice(IInvoiceStore invoiceStore)
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

            var invoiceRecord = await dpContext.Invoice.SingleOrDefaultAsync(x => x.InvoiceId == response.Invoice.InvoiceId);
            if (invoiceRecord == null)
            {
                invoiceRecord = new Invoice
                {
                    Id = 0,
                    Balance = response.Invoice.Balance,
                    BalanceFormatted = response.Invoice.BalanceFormatted,
                    CfCustomerType = response.Invoice.CfCustomerType,
                    CfCustomerTypeUnformatted = response.Invoice.CfCustomerTypeUnformatted,
                    CfSiteNumber = response.Invoice.CfSiteNumber,
                    CfSiteNumberUnformatted = response.Invoice.CfSiteNumberUnformatted,
                    CreatedDate = response.Invoice.CreatedDate,
                    CreatedDateFormatted = response.Invoice.CreatedDateFormatted,
                    CreatedTime = response.Invoice.CreatedTime,
                    CustomerId = response.Invoice.CustomerId,
                    CustomerName = response.Invoice.CustomerName,
                    InvoiceId = response.Invoice.InvoiceId,
                    InvoiceNumber = response.Invoice.InvoiceNumber,
                    InvoiceUrl = response.Invoice.InvoiceUrl,
                    Status = response.Invoice.Status,
                    StatusFormatted = response.Invoice.StatusFormatted,
                };

                dpContext.Invoice.Add(invoiceRecord);
                await dpContext.SaveChangesAsync();
            }

            response.InvoiceRecord = invoiceRecord;

            return response;
        }
    }
}
