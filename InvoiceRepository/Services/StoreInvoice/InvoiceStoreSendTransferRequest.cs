namespace InvoiceRepository
{
    using ApiDTO;

    using AutomaticTaskSharedLibrary;

    using CommonServices;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

    internal class InvoiceStoreSendTransferRequest : IInvoiceStore
    {
        private readonly IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer;

        private readonly IDateTimeService dateTimeService;

        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreSendTransferRequest(IInvoiceStore invoiceStore, IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer, IDateTimeService dateTimeService)
        {
            this.invoiceStore = invoiceStore;
            this.distributorToOperatorSendPointsTransfer = distributorToOperatorSendPointsTransfer;
            this.dateTimeService = dateTimeService;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful || response.Organization == null || response.Invoice.Balance > 0.00 || response.InvoiceRecord.LineItems == null)
            {
                return response;
            }

            foreach (var invoiceLineItem in response.InvoiceRecord.LineItems)
            {
                var organizationId = response.Organization.Id;
                var softwareType = invoiceLineItem.SoftwareType;
                var site = await dpContext.SiteInformation.Include(x => x.Vendor).ThenInclude(x => x.SoftwareType).SingleAsync(x =>
                                                                                          x.Organization.Id == organizationId &&
                                                                                          x.ResellerId == response.Invoice.CfResellerId &&
                                                                                          x.Vendor.SoftwareType.Name.ToUpper() == softwareType.ToUpper());
                var vendor = site.Vendor;

                var queueRecord = new TransferPointsQueue()
                {
                    InvoiceLineItemId = invoiceLineItem.Id,
                    Organization = response.Organization,
                    APIKey = response.Organization.APIKey,
                    SoftwareType = (SoftwareTypeEnum)vendor.SoftwareType.Id,
                    UserId = site.Vendor.UserName,
                    Password = site.Vendor.Password,
                    AccountId = site.UserName,
                    Points = invoiceLineItem.Quantity
                };

                await dpContext.TransferPointsQueue.AddAsync(queueRecord);
            }

            return response;
        }
    }
}