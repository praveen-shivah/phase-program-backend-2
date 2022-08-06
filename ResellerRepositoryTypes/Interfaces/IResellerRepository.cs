namespace ResellerRepositoryTypes
{
    using ApiDTO;

    public interface IResellerRepository
    {
        Task<List<ResellerDto>> GetResellers(int organizationId);

        Task<UpdateResellerResponse> UpdateResellerRequestAsync(int organizationId, ResellerDto resellerDto);

        Task<List<SiteInformationDto>> GetResellerSites(int organizationId, int resellerId);
    }
}
