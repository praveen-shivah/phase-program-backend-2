using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    using APISupport;
    using AuthenticationRepositoryTypes;
    using LoggingLibrary;
    using System.Threading.Tasks;
    using TransactionRepositoryTypes;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        private readonly ILogger logger;
        private readonly ITransactionRepository transactionRepository;
        public TransactionController(ILogger logger, ITransactionRepository transactionRepository)
        {
            this.logger = logger;
            this.transactionRepository = transactionRepository;
        }
        [HttpPost("get-transaction")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<IActionResult> GetTransactions(string customerId)
        {
            this.logger.Debug(LogClass.General, "GetTransactions received");

            var result = await this.transactionRepository.GetTransaction(customerId);
            return this.Ok(result);
        }
    }
}
