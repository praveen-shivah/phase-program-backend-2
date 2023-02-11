namespace ResellerRepository
{
    using ApiDTO;

    using CommonServices;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class ResellerBalanceService : IResellerBalanceService
    {
        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        private readonly IDateTimeService dateTimeService;

        public ResellerBalanceService(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IDateTimeService dateTimeService)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.dateTimeService = dateTimeService;
        }

        async Task<bool> IResellerBalanceService.UpdateBalance(ResellerBalanceDTO resellerBalance)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var record = await context.SiteInformation.SingleAsync(x => x.Reseller.Id == resellerBalance.ResellerId && x.Id == resellerBalance.SiteInformationId);
                        record.Balance = int.Parse(resellerBalance.Balance);

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            return result == WorkItemResultEnum.commitSuccessfullyCompleted;
        }

        async Task<bool> IResellerBalanceService.TransferPointsCompleted(ResellerTransferPointsCompletedDto resellerTransferPointsCompletedDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var record = await context.TransferPointsQueue.SingleOrDefaultAsync(x => x.InvoiceLineItemId == resellerTransferPointsCompletedDto.InvoiceLineItemId);
                        if (record != null)
                        {
                            record.DateTimeSent = this.dateTimeService.UtcNow;

                            var invoiceLineItemRecord = await context.InvoiceLineItem.SingleOrDefaultAsync(x => x.ItemId == record.ItemId);
                            if (invoiceLineItemRecord != null)
                            {
                                invoiceLineItemRecord.DateTimeSent = this.dateTimeService.UtcNow;
                            }
                        }

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            return result == WorkItemResultEnum.commitSuccessfullyCompleted;
        }
    }
}
