namespace OrganizationRepository
{
    using DatabaseContext;

    public interface IUpdateOrganization
    {
        Task<UpdateOrganizationResponse> Update(DataContext dataContext, UpdateOrganizationRequest updateOrganizationRequest);
    }
}
