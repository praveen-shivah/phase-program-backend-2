namespace ResellerRepository
{
    using DatabaseContext;

    using ResellerRepositoryTypes;

    public interface IUpdateReseller
    {
        Task<UpdateResellerResponse> UpdateResellerAsync(DataContext dataContext, UpdateResellerRequest request);
    }
}
