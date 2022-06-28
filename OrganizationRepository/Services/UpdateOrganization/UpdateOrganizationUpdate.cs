namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    using OrganizationRepository;

    public class UpdateOrganizationUpdate : IUpdateOrganization
    {
        private readonly IUpdateOrganization updateOrganization;

        public UpdateOrganizationUpdate(IUpdateOrganization updateOrganization)
        {
            this.updateOrganization = updateOrganization;
        }

        async Task<UpdateOrganizationResponse> IUpdateOrganization.Update(DPContext dpContext, UpdateOrganizationRequest updateOrganizationRequest)
        {
            var response = await this.updateOrganization.Update(dpContext, updateOrganizationRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.Organization.APIKey = updateOrganizationRequest.UpdateOrganizationDto.APIKey;
            response.Organization.Name = updateOrganizationRequest.UpdateOrganizationDto.Name;

            return response;
        }
    }
}
