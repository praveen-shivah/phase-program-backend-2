﻿namespace InvoiceRepository
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
            if (!response.IsSuccessful || response.Organization == null || response.Reseller == null || response.InvoiceRecord.DateTimeSent != null)
            {
                return response;
            }

            response.InvoiceRecord.Balance = response.Invoice.Balance;
            response.InvoiceRecord.BalanceFormatted = response.Invoice.BalanceFormatted;
            response.InvoiceRecord.Status = response.Invoice.Status;
            response.InvoiceRecord.StatusFormatted = response.Invoice.StatusFormatted;
            response.InvoiceRecord.Organization = response.Organization;
            response.InvoiceRecord.Reseller = response.Reseller;
            response.InvoiceRecord.CreatedDate = response.Invoice.CreatedDate;
            response.InvoiceRecord.CreatedDateFormatted = response.Invoice.CreatedDateFormatted;
            response.InvoiceRecord.CreatedTime = response.Invoice.CreatedTime.ToUniversalTime();
            response.InvoiceRecord.CustomerId = response.Invoice.CustomerId;
            response.InvoiceRecord.CustomerName = response.Invoice.CustomerName;
            response.InvoiceRecord.InvoiceId = response.Invoice.InvoiceId;
            response.InvoiceRecord.InvoiceNumber = response.Invoice.InvoiceNumber;
            response.InvoiceRecord.InvoiceUrl = response.Invoice.InvoiceUrl ?? string.Empty;

            var lineItems = await dpContext.InvoiceLineItem.Where(x => x.InvoiceId == response.InvoiceRecord.Id).ToListAsync();
            dpContext.InvoiceLineItem.RemoveRange(lineItems);
            await dpContext.SaveChangesAsync();

            foreach (var item in response.Invoice.LineItems)
            {
                var softwareTypeField = item.ItemCustomFields.SingleOrDefault(x => x.Placeholder.ToUpper() == "CF_SOFTWARE_TYPE");
                if (softwareTypeField == null || softwareTypeField.Value.ToUpper() == "NONE")
                {
                    continue;
                }

                var invoiceLineItemRecord = new InvoiceLineItem()
                {
                    Organization = response.Organization,
                    InvoiceId = response.InvoiceRecord.Id,
                    ItemId = item.ItemId,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    SoftwareType = softwareTypeField.Value
                };

                await dpContext.InvoiceLineItem.AddAsync(invoiceLineItemRecord);
            }

            return response;
        }
    }
}
