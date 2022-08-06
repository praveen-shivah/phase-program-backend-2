namespace DataSeedingLibrary
{
    using DataPostgresqlLibrary;

    public interface ISeedData
    {
        Task<SeedDataResponse> SeedAsync(DPContext context, SeedDataRequest seedDataRequest);
    }
}
