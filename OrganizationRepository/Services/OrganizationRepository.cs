namespace OrganizationRepository
{
    using ApiDTO;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using OrganizationRepositoryTypes;

    using UnitOfWorkTypesLibrary;

    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        private readonly IUpdateOrganization updateOrganization;

        public OrganizationRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IUpdateOrganization updateOrganization)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.updateOrganization = updateOrganization;
        }

        async Task<OrganizationResponse> IOrganizationRepository.GetOrganizationRequestAsync(OrganizationRequest organizationRequest)
        {
            var response = new OrganizationResponse();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var organizationRecord = await context.Organization.SingleOrDefaultAsync(x => x.Id.ToString() == organizationRequest.OrganizationId && x.Apikey == organizationRequest.APIKey);
                        if (organizationRecord == null)
                        {
                            return WorkItemResultEnum.cancelWithoutError;
                        }

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            response.IsSuccessful = result == WorkItemResultEnum.commitSuccessfullyCompleted;

            return response;
        }

        async Task<UpdateOrgResponse> IOrganizationRepository.UpdateOrganizationRequestAsync(OrganizationDto organizationDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.updateOrganization.Update(context, new UpdateOrganizationRequest(organizationDto));

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();

            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new UpdateOrgResponse() { IsSuccessful = false };
            }

            return new UpdateOrgResponse() { IsSuccessful = true };
        }


        async Task<List<OrganizationDto>> IOrganizationRepository.GetOrganizations()
        {
            var result = new List<OrganizationDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var organizations = await context.Organization.ToListAsync();
                        result.Add(new OrganizationDto() { IsPlaceHolder = true });
                        foreach (var organization in organizations)
                        {
                            result.Add(
                                new OrganizationDto()
                                    {
                                        APIKey = organization.Apikey,
                                        Id = organization.Id,
                                        Name = organization.Name,
                                        URL = organization.Url,
                                        UserId = string.Empty,
                                        Password = string.Empty
                                    });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<OrganizationDto>();
        }
    }
}
