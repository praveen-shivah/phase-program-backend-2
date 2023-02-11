namespace AuthenticationRepository
{
    using DatabaseContext;

    using OrganizationRepository;

    public class UpdateOrganizationUpdate : IUpdateOrganization
    {
        private readonly IUpdateOrganization updateOrganization;

        public UpdateOrganizationUpdate(IUpdateOrganization updateOrganization)
        {
            this.updateOrganization = updateOrganization;
        }

        async Task<UpdateOrganizationResponse> IUpdateOrganization.Update(DataContext dataContext, UpdateOrganizationRequest updateOrganizationRequest)
        {
            var response = await this.updateOrganization.Update(dataContext, updateOrganizationRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.Organization.Apikey = updateOrganizationRequest.UpdateOrganizationDto.APIKey;
            response.Organization.Name = updateOrganizationRequest.UpdateOrganizationDto.Name;

            return response;
        }
    }
}
