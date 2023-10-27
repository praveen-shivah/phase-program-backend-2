
namespace TransactionRepository
{
    using DatabaseContext;
    using System.Threading.Tasks;
    using TransactionRepositoryTypes;
    public class CreateTransactionStart : ICreateTransaction
    {
        public Task<CreateTransactionResponse> CreateTransactionAsync(DataContext dataContext, CreateTransactionRequest request)
        {
            return Task.FromResult(new CreateTransactionResponse() { IsSuccessful = true });
        }
    }
}
