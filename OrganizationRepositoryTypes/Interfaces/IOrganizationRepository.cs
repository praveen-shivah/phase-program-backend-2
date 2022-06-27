namespace OrganizationRepositoryTypes
{
    using ApiDTO;

    public interface IOrganizationRepository
    {
        Task<OrganizationResponse> GetOrganizationRequestAsync(OrganizationRequest organizationRequest);

        Task<List<OrganizationDto>> GetOrganizations();
    }
}
