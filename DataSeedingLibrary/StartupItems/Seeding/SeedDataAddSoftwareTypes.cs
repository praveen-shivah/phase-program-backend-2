namespace DataSeedingLibrary
{
    using ApiDTO;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    public class SeedDataAddSoftwareTypes : ISeedData
    {
        private readonly ISeedData seedData;

        public SeedDataAddSoftwareTypes(ISeedData seedData)
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

            var softwareTypeValues = Enum.GetValues(typeof(SoftwareTypeEnum));
            foreach (int softwareTypeValue in softwareTypeValues)
            {
                var name = Enum.GetName(typeof(SoftwareTypeEnum), softwareTypeValue);
                if (name == null)
                {
                    continue;
                }

                var softwareType = await context.SoftwareType.SingleOrDefaultAsync(x => x.Id == softwareTypeValue);
                if (softwareType != null)
                {
                    continue;
                }

                context.SoftwareType.Add(
                    new SoftwareType
                        {
                            Id = softwareTypeValue,
                            Name = name
                        });
            }

            return response;
        }
    }
}