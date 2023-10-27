
namespace TransactionRepository
{
    using DatabaseContext;
    using TransactionRepositoryTypes;
    public interface ICreateTransaction
    {
        Task<CreateTransactionResponse> CreateTransactionAsync(DataContext dataContext, CreateTransactionRequest request);
    }
}
