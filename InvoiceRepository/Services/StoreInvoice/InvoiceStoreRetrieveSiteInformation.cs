namespace InvoiceRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

    internal class InvoiceStoreRetrieveSiteInformation : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreRetrieveSiteInformation(IInvoiceStore invoiceStore)
        {
            this.invoiceStore = invoiceStore;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful || response.Organization == null || response.InvoiceRecord == null || response.InvoiceRecord.LineItems == null)
            {
                return response;
            }

            foreach (var invoiceLineItem in response.InvoiceRecord.LineItems)
            {
                var organizationId = response.Organization.Id;
                var softwareType = invoiceLineItem.SoftwareType;
                var site = await dpContext.SiteInformation.Include(x => x.Vendor).SingleOrDefaultAsync(
                                    x => x.Organization.Id == organizationId &&
                                    x.ResellerId == response.Invoice.CfResellerId &&
                                    x.Vendor.Name.ToUpper().Trim() == softwareType.ToUpper());

                if (site != null)
                {
                    continue;
                }

                var vendor = await dpContext.Vendor.SingleOrDefaultAsync(x => x.Name.ToUpper() == invoiceLineItem.SoftwareType.ToUpper());
                if (vendor == null)
                {
                    continue;
                }

                var siteInformation = new SiteInformation
                {
                    Organization = response.Organization,
                    Description = invoiceLineItem.SoftwareType,
                    Item_Id = invoiceLineItem.ItemId,
                    URL = string.Empty,
                    Vendor = vendor,
                    AccountId = string.Empty,
                    ResellerId = response.Invoice.CfResellerId
                };

                await dpContext.SiteInformation.AddAsync(siteInformation);
            }

            await dpContext.SaveChangesAsync();

            return response;
        }
    }
}