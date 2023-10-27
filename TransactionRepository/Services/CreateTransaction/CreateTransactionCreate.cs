
namespace TransactionRepository
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using TransactionRepositoryTypes;
    public class CreateTransactionCreate : ICreateTransaction
    {
        private readonly ICreateTransaction createTransaction;
        public CreateTransactionCreate(ICreateTransaction createTransaction)
        {
            this.createTransaction = createTransaction;
        }
        async Task<CreateTransactionResponse> ICreateTransaction.CreateTransactionAsync(DataContext dataContext, CreateTransactionRequest request)
        {
            try
            {
                var response = await this.createTransaction.CreateTransactionAsync(dataContext, request);
                if (!response.IsSuccessful)
                {
                    return response;
                }
                //Check if Transaction exist
                var existingTransaction = await dataContext.Transaction
                    .SingleOrDefaultAsync(p => p.CustomerID == request.transactionDto.CustomerID &&
                p.Time == request.transactionDto.Time &&
                p.Station == request.transactionDto.Station &&
                p.Comps == request.transactionDto.Comps &&
                p.Amount == request.transactionDto.Amount &&
                p.Free == request.transactionDto.Free &&
                p.Type == request.transactionDto.Type);

                if (existingTransaction != null)
                {
                    return new CreateTransactionResponse { IsSuccessful = true };
                }
                // Add a new Transaction
                var transaction = new Transaction()
                {
                    CustomerID = request.transactionDto.CustomerID,
                    Time = request.transactionDto.Time,
                    Station = request.transactionDto.Station,
                    Comps = request.transactionDto.Comps,
                    Amount = request.transactionDto.Amount,
                    Free = request.transactionDto.Free,
                    Type = request.transactionDto.Type,
                    OrganizationId = request.transactionDto.OrganizationId,
                    ResellerId = request.transactionDto.ResellerId,
                    VendorId = request.transactionDto.VendorId,
                    LoginUsername = request.transactionDto.LoginUsername,
                    LoginPassword = request.transactionDto.LoginPassword,
                };
                await dataContext.Transaction.AddAsync(transaction);
                await dataContext.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
