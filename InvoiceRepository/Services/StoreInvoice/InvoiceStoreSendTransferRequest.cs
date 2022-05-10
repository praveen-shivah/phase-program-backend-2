namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

    internal class InvoiceStoreSendTransferRequest : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        private readonly IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer;

        public InvoiceStoreSendTransferRequest(IInvoiceStore invoiceStore, IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer)
        {
            this.invoiceStore = invoiceStore;
            this.distributorToOperatorSendPointsTransfer = distributorToOperatorSendPointsTransfer;
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
                var organizationId = invoiceLineItem.Organization.Id;
                var siteId = int.Parse(invoiceLineItem.ItemId);
                var site = dpContext.SiteInformation.Include(x=>x.Vendor).Single(x => x.Organization.Id == organizationId && x.Id == siteId);
                var vendor = site.Vendor;

                await this.distributorToOperatorSendPointsTransfer.SendPointsTransfer(
                    new DistributorToResellerSendPointsTransferRequest()
                    {
                        SoftwareType = vendor.SoftwareType,
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
