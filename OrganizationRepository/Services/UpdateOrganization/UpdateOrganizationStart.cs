namespace OrganizationRepository
{
    using DataPostgresqlLibrary;

    public class UpdateOrganizationStart : IUpdateOrganization
    {
        Task<UpdateOrganizationResponse> IUpdateOrganization.Update(DPContext dpContext, UpdateOrganizationRequest updateOrganizationRequest)
        {
            return Task.FromResult(new UpdateOrganizationResponse() { IsSuccessful = true });
        }
    }
}
