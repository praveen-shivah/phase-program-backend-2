namespace ResellerRepository;

using CommonServices;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

using ResellerRepositoryTypes;

public class UpdateResellerAddSitesForEachVendor : IUpdateReseller
{
    private IUpdateReseller updateReseller;

    private readonly IDateTimeService dateTimeService;

    public UpdateResellerAddSitesForEachVendor(IUpdateReseller updateReseller, IDateTimeService dateTimeService)
    {
        this.updateReseller = updateReseller;
        this.dateTimeService = dateTimeService;
    }

    async Task<UpdateResellerResponse> IUpdateReseller.UpdateResellerAsync(DataContext context, UpdateResellerRequest request)
    {
        var response = await this.updateReseller.UpdateResellerAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }
        var vendors = await context.Vendor.Where(x => x.IsActive).ToListAsync();
        foreach (var vendor in vendors)
        {
            var siteInformation = await context.SiteInformation.SingleOrDefaultAsync(x => x.OrganizationId == response.Reseller.OrganizationId && x.VendorId == vendor.Id);
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
                OrganizationId = response.Reseller.OrganizationId,
                ResellerId = response.Reseller.Id,
                VendorId = vendor.Id,
                Url = string.Empty
            };

            context.SiteInformation.Add(siteInformation);
        }

        return response;
    }
}