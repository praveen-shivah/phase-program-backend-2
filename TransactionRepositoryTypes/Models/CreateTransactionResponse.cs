
namespace TransactionRepositoryTypes
{
    using DatabaseContext;
    public class CreateTransactionResponse
    {
        public bool IsSuccessful { get; set; }

        public Transaction Transaction { get; set; }
    }
}
