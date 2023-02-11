namespace OrganizationRepository
{
    using DatabaseContext;

    public class UpdateOrganizationStart : IUpdateOrganization
    {
        Task<UpdateOrganizationResponse> IUpdateOrganization.Update(DataContext dataContext, UpdateOrganizationRequest updateOrganizationRequest)
        {
            return Task.FromResult(new UpdateOrganizationResponse() { IsSuccessful = true });
        }
    }
}
