namespace DataSeedingLibrary
{
    using CommonServices;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    // On startup, loops through all resellers and then through each site and
    // creates a record for the reseller/site combination
    public class SeedDataAddMissingSitesToResellers : ISeedData
    {
        private readonly ISeedData seedData;

        private readonly IDateTimeService dateTimeService;

        public SeedDataAddMissingSitesToResellers(ISeedData seedData, IDateTimeService dateTimeService)
        {
            this.seedData = seedData;
            this.dateTimeService = dateTimeService;
        }

        async Task<SeedDataResponse> ISeedData.SeedAsync(DataContext context, SeedDataRequest seedDataRequest)
        {
            var response = await this.seedData.SeedAsync(context, seedDataRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            //var softwareType = invoiceLineItem.SoftwareType;
            //var site = await dataContext.SiteInformation.Include(x => x.Vendor).SingleOrDefaultAsync(
            //               x => x.Organization.Id == organizationId &&
            //                    x.ResellerId == response.Invoice.CfResellerId &&
            //                    x.Vendor.Name.ToUpper().Trim() == softwareType.ToUpper());

            var vendors = await context.Vendor.ToListAsync();
            var resellers = await context.Reseller.ToListAsync();

            foreach (var reseller in resellers)
            {
                foreach (var vendor in vendors)
                {
                    var siteInformation = await context.SiteInformation.SingleOrDefaultAsync(x => x.OrganizationId == reseller.OrganizationId && x.VendorId == vendor.Id);
                    if (siteInformation != null)
                    {
                        continue;
                    }

                    siteInformation = new SiteInformation()
                                          {
                                              AccountId = string.Empty,
                                              Balance = 0,
                                              CreatedOn = this.dateTimeService.UtcNow,
                                              Description = vendor.Name,
                                              ItemId = string.Empty,
                                              OrganizationId = reseller.OrganizationId,
                                              ResellerId = reseller.Id,
                                              VendorId = vendor.Id,
                                              Url = string.Empty
                                          };
                    context.SiteInformation.Add(siteInformation);
                }
            }


            return response;
        }
    }
}