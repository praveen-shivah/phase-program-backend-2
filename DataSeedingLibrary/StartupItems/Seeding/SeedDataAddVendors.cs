﻿namespace DataSeedingLibrary
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    public class SeedDataAddVendors : ISeedData
    {
        private readonly ISeedData seedData;

        public SeedDataAddVendors(ISeedData seedData)
        {
            this.seedData = seedData;
        }

        async Task<SeedDataResponse> ISeedData.SeedAsync(DataContext context, SeedDataRequest seedDataRequest)
        {
            var response = await this.seedData.SeedAsync(context, seedDataRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var softwareTypes = context.SoftwareType.ToList();
            foreach (var softwareType in softwareTypes)
            {
                var vendor = await context.Vendor.SingleOrDefaultAsync(x => x.SoftwareTypeId == softwareType.Id);
                if (vendor != null)
                {
                    continue;
                }

                context.Vendor.Add(
                    new Vendor
                        {
                            Id = softwareType.Id,
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow,
                            IsActive = true,
                            Name = softwareType.Name,
                            SoftwareTypeId = softwareType.Id
                        });
            }

            return response;
        }
    }
}