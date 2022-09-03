namespace DataSeedingLibrary
{

    using AuthenticationRepositoryTypes;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    public class SeedDataAddOrganizations : ISeedData
    {
        private readonly ISeedData seedData;

        public SeedDataAddOrganizations(ISeedData seedData)
        {
            this.seedData = seedData;
        }

        async Task<SeedDataResponse> ISeedData.SeedAsync(DPContext context, SeedDataRequest seedDataRequest)
        {
            var response = await this.seedData.SeedAsync(context, seedDataRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var organization = context.Organization.SingleOrDefault(x => x.Id == 1);
            if (organization != null)
            {
                return response;
            }

            organization = new Organization()
            {
                APIKey = AuthenticationConstants.OrganizationAPIKey,
                Name = "Multi-Sweeps administration",
                Password = AuthenticationConstants.AuthenticationAdminDefaultPassword,
                UserId = AuthenticationConstants.AuthenticationAdminDefaultUserName,
                URL = string.Empty
            };

            context.Organization.Add(organization);

            return response;
        }
    }
}
