namespace DataSeedingLibrary
{
    using DataPostgresqlLibrary;

    public class SeedDataStart : ISeedData
    {
        Task<SeedDataResponse> ISeedData.SeedAsync(DPContext context, SeedDataRequest seedDataRequest)
        {
            return Task.FromResult(new SeedDataResponse() {IsSuccessful = true});
        }
    }
}
