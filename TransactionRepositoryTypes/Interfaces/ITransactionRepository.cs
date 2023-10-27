
namespace TransactionRepositoryTypes
{
    using ApiDTO;

    public interface ITransactionRepository
    {
        Task<CreateTransactionResponse> AddTransactionRequestAsync(TransactionDto transactionDto);
        Task<List<TransactionDto>> GetTransaction(string customerId);
    }
}
