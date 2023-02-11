namespace InvoiceRepository
{
    using DatabaseContext;

    using InvoiceRepositoryTypes;

    using Microsoft.EntityFrameworkCore;

    internal class InvoiceStoreRetrieveSiteInformation : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreRetrieveSiteInformation(IInvoiceStore invoiceStore)
        {
            this.invoiceStore = invoiceStore;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DataContext dataContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dataContext, request);
            if (!response.IsSuccessful || response.Organization == null || response.InvoiceRecord == null || !response.InvoiceRecord.InvoiceLineItem.Any())
            {
                return response;
            }

            foreach (var invoiceLineItem in response.InvoiceRecord.InvoiceLineItem)
            {
                var organizationId = response.Organization.Id;
                var softwareType = invoiceLineItem.SoftwareType;
                var site = await dataContext.SiteInformation.Include(x => x.Vendor).SingleOrDefaultAsync(
                                    x => x.Organization.Id == organizationId &&
                                    x.ResellerId == response.Invoice.CfResellerId &&
                                    x.Vendor.Name.ToUpper().Trim() == softwareType.ToUpper());

                if (site != null)
                {
                    continue;
                }

                var vendor = await dataContext.Vendor.SingleOrDefaultAsync(x => x.Name.ToUpper() == invoiceLineItem.SoftwareType.ToUpper());
                if (vendor == null)
                {
                    continue;
                }

                var siteInformation = new SiteInformation
                {
                    Organization = response.Organization,
                    Description = invoiceLineItem.SoftwareType,
                    ItemId = invoiceLineItem.ItemId,
                    Url = string.Empty,
                    Vendor = vendor,
                    AccountId = string.Empty,
                    ResellerId = response.Invoice.CfResellerId
                };

                await dataContext.SiteInformation.AddAsync(siteInformation);
            }

            await dataContext.SaveChangesAsync();

            return response;
        }
    }
}