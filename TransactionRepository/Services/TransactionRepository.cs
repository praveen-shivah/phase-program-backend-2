
namespace TransactionRepository
{
    using ApiDTO;

    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;
    using TransactionRepositoryTypes;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class TransactionRepository : ITransactionRepository
    {
        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        private readonly ICreateTransaction createTransaction;

        private readonly ILogger logger;

        public TransactionRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, ICreateTransaction createTransaction, ILogger logger)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.createTransaction = createTransaction;
            this.logger = logger;
        }

        async Task<CreateTransactionResponse> ITransactionRepository.AddTransactionRequestAsync(TransactionDto transactionDto)
        {
            try
            {
                var uow = this.unitOfWorkFactory.Create(
                    async context =>
                    {
                        var saveTransactionResponse = await this.createTransaction.CreateTransactionAsync(context, new CreateTransactionRequest(transactionDto));
                        if (saveTransactionResponse.IsSuccessful)
                        {
                            return WorkItemResultEnum.doneContinue;
                        }
                        this.logger.Error(
                                LogClass.General,
                                "TransactionRepository",
                                "AddTransactionRequestAsync",
                                $"Error storing players information - reason: {saveTransactionResponse}",
                                new Exception($"Error storing players information - reason: {saveTransactionResponse}"));

                        return WorkItemResultEnum.rollbackExit;
                    });
                var result = await uow.ExecuteAsync();

                if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
                {
                    return new CreateTransactionResponse() { IsSuccessful = false };
                }
                return new CreateTransactionResponse() { IsSuccessful = true };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<TransactionDto>> ITransactionRepository.GetTransaction(string customerId)
        {
            var result = new List<TransactionDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                {
                    var transactions = await context.Transaction.Where(p => p.CustomerID == customerId).ToListAsync();
                    result.Add(new TransactionDto() { IsPlaceHolder = true });
                    foreach (var transaction in transactions)
                    {
                        result.Add(
                            new TransactionDto()
                            {
                                CustomerID = transaction.CustomerID,
                                Time = transaction.Time,
                                Amount = transaction.Amount,
                                Comps = transaction.Comps,
                                Free = transaction.Free,
                                Type = transaction.Type,
                                Station = transaction.Station,
                                OrganizationId = transaction.OrganizationId,
                                ResellerId = transaction.ResellerId,
                                VendorId = transaction.VendorId
                            });
                    }

                    return WorkItemResultEnum.doneContinue;
                });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<TransactionDto>();
        }
    }
}
