namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

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
                var organizationId = invoiceLineItem.OrganizationId.Trim();
                var siteId = int.Parse(invoiceLineItem.ItemId);
                var site = dpContext.SiteInformation.Include(x=>x.Vendor).Single(x => x.OrganizationId == organizationId && x.Id == siteId);
                var vendor = site.Vendor;

                await this.vendorToOperatorSendPointsTransfer.SendPointsTransfer(
                    new VendorToOperatorSendPointsTransferRequest()
                    {
                        SoftwareType = vendor.SoftwareType,
                        SiteUrl = site.URL,
                        UserId = site.UserName,
                        Password = site.Password,
                        AccountId = invoiceLineItem.Description.Trim(),
                        Points = invoiceLineItem.Quantity,
                    });
            }

            return response;
        }
    }
}
