namespace ApiHost
{
    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult OperatorUpdated(int operatorId)
        {
            this.logger.Debug(LogClass.General, "Operator information updated");

            return this.Ok();
        }
    }
}