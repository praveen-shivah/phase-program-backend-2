namespace InvoiceRepository
{
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

            response.Organization = await dpContext.Organization.SingleOrDefaultAsync(x => x.Id == request.OrganizationId);
            if (response.Organization == null)
            {
                response.IsSuccessful = false;
                response.InvoiceStoreResponseType = InvoiceStoreResponseType.invalidOrganizationId;
                return response;
            }

            response.Reseller = await dpContext.Reseller.SingleOrDefaultAsync(x => x.Id == request.OrganizationId && x.Id == response.Invoice.CfResellerId);
            if (response.Reseller == null)
            {
                response.IsSuccessful = false;
                response.InvoiceStoreResponseType = InvoiceStoreResponseType.invalidResellerId;
                return response;
            }

            var invoiceRecord = await dpContext.Invoice.SingleOrDefaultAsync(x => x.InvoiceId == response.Invoice.InvoiceId);
            if (invoiceRecord == null)
            {
                invoiceRecord = new Invoice
                {
                    Id = 0,
                    Organization = response.Organization,
                    Balance = response.Invoice.Balance,
                    BalanceFormatted = response.Invoice.BalanceFormatted,
                    Reseller = response.Reseller,
                    CreatedDate = response.Invoice.CreatedDate,
                    CreatedDateFormatted = response.Invoice.CreatedDateFormatted,
                    CreatedTime = response.Invoice.CreatedTime.ToUniversalTime(),
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
