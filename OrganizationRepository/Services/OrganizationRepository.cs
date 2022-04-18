namespace OrganizationRepository
{
    using DataPostgresqlLibrary;

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
                context =>
                    {
                        var organizationRecord = context.Organization.SingleOrDefault(x => x.UserId == organizationRequest.OrganizationId && x.APIKey == organizationRequest.APIKey);
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
