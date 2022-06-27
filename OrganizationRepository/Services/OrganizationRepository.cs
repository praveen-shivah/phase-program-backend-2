namespace OrganizationRepository
{
    using ApiDTO;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using OrganizationRepositoryTypes;

    using UnitOfWorkTypesLibrary;

    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public OrganizationRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        async Task<OrganizationResponse> IOrganizationRepository.GetOrganizationRequestAsync(OrganizationRequest organizationRequest)
        {
            var response = new OrganizationResponse();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var organizationRecord = await context.Organization.SingleOrDefaultAsync(x => x.UserId == organizationRequest.OrganizationId && x.APIKey == organizationRequest.APIKey);
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

        async Task<List<OrganizationDto>> IOrganizationRepository.GetOrganizations()
        {
            var result = new List<OrganizationDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var organizations = await context.Organization.ToListAsync();
                        foreach (var organization in organizations)
                        {
                            result.Add(
                                new OrganizationDto()
                                    {
                                        APIKey = organization.APIKey,
                                        Id = organization.Id,
                                        Name = organization.Name,
                                        URL = organization.URL
                                    });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<OrganizationDto>();
        }
    }
}
