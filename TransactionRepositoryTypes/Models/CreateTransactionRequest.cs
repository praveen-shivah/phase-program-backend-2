
namespace TransactionRepositoryTypes
{
    using ApiDTO;
    public class CreateTransactionRequest
    {
        public CreateTransactionRequest(TransactionDto transactionDto)
        {
            this.transactionDto = transactionDto;
        }

        public TransactionDto transactionDto { get; }
    }
}
