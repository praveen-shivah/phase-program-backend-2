namespace InvoiceRepository
{
    using ApiDTO;

    using AutomaticTaskSharedLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

    internal class InvoiceStoreSendTransferRequest : IInvoiceStore
    {
        private readonly IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer;

        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreSendTransferRequest(IInvoiceStore invoiceStore, IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer)
        {
            this.invoiceStore = invoiceStore;
            this.distributorToOperatorSendPointsTransfer = distributorToOperatorSendPointsTransfer;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful || response.Organization == null)
            {
                return response;
            }

            foreach (var invoiceLineItem in response.InvoiceRecord.LineItems)
            {
                var organizationId = response.Organization.Id;
                var softwareType = invoiceLineItem.SoftwareType;
                var site = dpContext.SiteInformation.Include(x => x.Vendor).Single(x => x.Organization.Id == organizationId && x.Vendor.SoftwareType.Name.ToUpper() == softwareType.ToUpper());
                var vendor = site.Vendor;

                await this.distributorToOperatorSendPointsTransfer.SendPointsTransfer(
                    new DistributorToResellerSendPointsTransferRequest
                        {
                            SoftwareType = (SoftwareTypeEnum)vendor.SoftwareType.Id,
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