namespace ResellerRepositoryTypes
{
    using ApiDTO;

    public interface IResellerRepository
    {
        Task<List<ResellerDto>> GetResellers();

        Task<UpdateResellerResponse> UpdateResellerRequestAsync(int organizationId, ResellerDto resellerDto);
    }
}
