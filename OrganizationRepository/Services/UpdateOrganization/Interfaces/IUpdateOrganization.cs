namespace OrganizationRepository
{
    using DataPostgresqlLibrary;

    public interface IUpdateOrganization
    {
        Task<UpdateOrganizationResponse> Update(DPContext dpContext, UpdateOrganizationRequest updateOrganizationRequest);
    }
}
