namespace ResellerRepository
{
    using DataPostgresqlLibrary;

    using ResellerRepositoryTypes;

    public interface IUpdateReseller
    {
        Task<UpdateResellerResponse> UpdateResellerAsync(DPContext dpContext, UpdateResellerRequest request);
    }
}
