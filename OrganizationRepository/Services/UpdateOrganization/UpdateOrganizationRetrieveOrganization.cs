namespace OrganizationRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    public class UpdateOrganizationRetrieveOrganization : IUpdateOrganization
    {
        private readonly IUpdateOrganization updateOrganization;

        public UpdateOrganizationRetrieveOrganization(IUpdateOrganization updateOrganization)
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

            var organization = await dpContext.Organization.SingleOrDefaultAsync(x => x.Id == updateOrganizationRequest.UpdateOrganizationDto.Id);
            if (organization == null)
            {
                organization = new Organization()
                {
                    Id = updateOrganizationRequest.UpdateOrganizationDto.Id,
                    APIKey = updateOrganizationRequest.UpdateOrganizationDto.APIKey,
                    Name = updateOrganizationRequest.UpdateOrganizationDto.Name,
                    URL = updateOrganizationRequest.UpdateOrganizationDto.URL,
                    UserId = updateOrganizationRequest.UpdateOrganizationDto.UserId,
                    Password = updateOrganizationRequest.UpdateOrganizationDto.Password
                };

                dpContext.Organization.Add(organization);
            }

            response.Organization = organization;
            return response;
        }
    }
}
