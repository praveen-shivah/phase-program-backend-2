namespace DataSeedingLibrary
{
    using DatabaseContext;

    public class SeedDataStart : ISeedData
    {
        Task<SeedDataResponse> ISeedData.SeedAsync(DataContext context, SeedDataRequest seedDataRequest)
        {
            return Task.FromResult(new SeedDataResponse() {IsSuccessful = true});
        }
    }
}
