namespace ResellerRepository
{
    using DatabaseContext;

    using ResellerRepositoryTypes;

    public class UpdateResellerStart : IUpdateReseller
    {
        Task<UpdateResellerResponse> IUpdateReseller.UpdateResellerAsync(DataContext dataContext, UpdateResellerRequest request)
        {
            return Task.FromResult(new UpdateResellerResponse() { IsSuccessful = true});
        }
    }
}
