namespace ResellerRepository
{
    using ApiDTO;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    using ResellerRepositoryTypes;

    public class ResellerRepository : IResellerRepository
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        private readonly IUpdateReseller updateReseller;

        public ResellerRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory, IUpdateReseller updateReseller)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.updateReseller = updateReseller;
        }

        async Task<List<ResellerDto>> IResellerRepository.GetResellers()
        {
            var result = new List<ResellerDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var Resellers = await context.Reseller.ToListAsync();
                        result.Add(new ResellerDto() { IsPlaceHolder = true });
                        foreach (var Reseller in Resellers)
                        {
                            result.Add(
                                new ResellerDto()
                                {
                                    Id = Reseller.Id,
                                    Name = Reseller.Name,
                                });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<ResellerDto>();
        }

        async Task<UpdateResellerResponse> IResellerRepository.UpdateResellerRequestAsync(int organizationId, ResellerDto ResellerDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.updateReseller.UpdateResellerAsync(context, new UpdateResellerRequest(organizationId, ResellerDto));

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();

            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new UpdateResellerResponse() { IsSuccessful = false };
            }

            return new UpdateResellerResponse() { IsSuccessful = true };
        }
    }
}
