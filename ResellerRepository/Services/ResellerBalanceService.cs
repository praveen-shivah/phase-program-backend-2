namespace ResellerRepository
{
    using ApiDTO;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class ResellerBalanceService : IResellerBalanceService
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public ResellerBalanceService(IUnitOfWorkFactory<DPContext> unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        async Task<bool> IResellerBalanceService.UpdateBalance(ResellerBalanceDTO resellerBalance)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var record = await context.ResellerVendorBalance.SingleAsync(x => x.Reseller.Id == resellerBalance.ResellerId);
                        record.Balance = int.Parse(resellerBalance.Balance);

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            return result == WorkItemResultEnum.commitSuccessfullyCompleted;
        }
    }
}
