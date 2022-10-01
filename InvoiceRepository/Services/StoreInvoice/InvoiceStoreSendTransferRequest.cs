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
                var site = await dpContext.SiteInformation.Include(x => x.Vendor).ThenInclude(x => x.SoftwareType).Include(v => v.Vendor.VendorCredentialsByOrganizations).SingleAsync(x =>
                                                                                            x.Organization.Id == organizationId &&
                                                                                            x.ResellerId == response.Invoice.CfResellerId &&
                                                                                            x.Vendor.SoftwareType.Name.ToUpper() == softwareType.ToUpper());
                var vendor = site.Vendor;
                var vendorCredentials = dpContext.VendorCredentialsByOrganizations.FirstOrDefault(x => x.Vendor.Id == vendor.Id && x.Organization.Id == request.OrganizationId);

                if (site == null || site.AccountId == null || vendor == null)
                {
                    continue;
                }

                // before we try ensure we have all of the information 
                // zq - information should be added to the record
                if (vendorCredentials == null)
                {
                    vendorCredentials = new VendorCredentialsByOrganization()
                                            {
                                                Organization = site.Organization,
                                                Vendor = vendor,
                                                UserName = string.Empty,
                                                Password = string.Empty
                                            };
                    await dpContext.VendorCredentialsByOrganizations.AddAsync(vendorCredentials);
                    continue;
                }

                // If this ItemId has already been added but has not yet been processed, then
                // remove it, otherwise it cannot be added
                var listToBeDeleted = await dpContext.TransferPointsQueue.Where(x => x.ItemId == invoiceLineItem.ItemId).ToListAsync();
                if (listToBeDeleted.Any(x => x.DateTimeProcessStarted != null || x.DateTimeSent != null))
                {
                    continue;
                }

                dpContext.RemoveRange(listToBeDeleted);

                var queueRecord = new TransferPointsQueue()
                {
                    InvoiceLineItemId = invoiceLineItem.Id,
                    ItemId = invoiceLineItem.ItemId,
                    Organization = response.Organization,
                    APIKey = response.Organization.APIKey,
                    SoftwareType = (SoftwareTypeEnum)vendor.SoftwareType.Id,
                    UserId = vendorCredentials.UserName,   // login credentials for this organization to this vendor
                    Password = vendorCredentials.Password,
                    AccountId = site.AccountId,            // account to transfer to for the selected reseller
                    Points = invoiceLineItem.Quantity
                };

                await dpContext.TransferPointsQueue.AddAsync(queueRecord);
                await dpContext.SaveChangesAsync();
            }

            return response;
        }
    }
}