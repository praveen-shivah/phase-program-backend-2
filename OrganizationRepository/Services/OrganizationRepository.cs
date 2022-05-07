﻿namespace OrganizationRepository
{
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
    }
}