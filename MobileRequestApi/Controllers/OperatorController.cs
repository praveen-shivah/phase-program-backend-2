namespace ApiHost
{
    using APISupport;
    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/operator")]
    public class OperatorController : ApiControllerBase
    {
        private readonly ILogger logger;

        public OperatorController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpPost("operator-updated")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public IActionResult OperatorUpdated(int operatorId)
        {
            this.logger.Debug(LogClass.General, "Operator information updated");

            return this.Ok();
        }
    }
}