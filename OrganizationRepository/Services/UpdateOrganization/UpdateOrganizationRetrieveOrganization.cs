﻿namespace OrganizationRepository
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    public class UpdateOrganizationRetrieveOrganization : IUpdateOrganization
    {
        private readonly IUpdateOrganization updateOrganization;

        public UpdateOrganizationRetrieveOrganization(IUpdateOrganization updateOrganization)
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

            var organization = await dataContext.Organization.SingleOrDefaultAsync(x => x.Id == updateOrganizationRequest.UpdateOrganizationDto.Id);
            if (organization == null)
            {
                organization = new Organization()
                {
                    Id = updateOrganizationRequest.UpdateOrganizationDto.Id,
                    Apikey = updateOrganizationRequest.UpdateOrganizationDto.APIKey,
                    Name = updateOrganizationRequest.UpdateOrganizationDto.Name,
                    Url = updateOrganizationRequest.UpdateOrganizationDto.URL,
                    UserId = updateOrganizationRequest.UpdateOrganizationDto.UserId,
                    Password = updateOrganizationRequest.UpdateOrganizationDto.Password
                };

                dataContext.Organization.Add(organization);
            }

            response.Organization = organization;
            return response;
        }
    }
}
