namespace DataSeedingLibrary
{
    using DatabaseContext;

    public interface ISeedData
    {
        Task<SeedDataResponse> SeedAsync(DataContext context, SeedDataRequest seedDataRequest);
    }
}
