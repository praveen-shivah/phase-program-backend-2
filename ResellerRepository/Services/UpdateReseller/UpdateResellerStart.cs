namespace ResellerRepository
{
    using DataPostgresqlLibrary;

    using ResellerRepositoryTypes;

    public class UpdateResellerStart : IUpdateReseller
    {
        Task<UpdateResellerResponse> IUpdateReseller.UpdateResellerAsync(DPContext dpContext, UpdateResellerRequest request)
        {
            return Task.FromResult(new UpdateResellerResponse() { IsSuccessful = true});
        }
    }
}
