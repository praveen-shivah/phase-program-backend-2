namespace ApiHost
{
    using System;

    using ApiHost.Middleware;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/log")]
    public class LogController : ApiControllerBase
    {
        private readonly ILogger logger;

        public LogController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            this.logger.Debug(LogClass.General, $"GET: test of a log message userId: {this.UserId} organizationId: {this.OrganizationId} ");
            this.logger.Error(LogClass.General, "LogController", "Index", "Test of an error message", new Exception("Test of an error exception"));

            return this.Ok();
        }

        [HttpPost("log")]
        public IActionResult Log()
        {
            this.logger.Debug(LogClass.General, $"POST: test of a log message userId: {this.UserId} organizationId: {this.OrganizationId} ");
            this.logger.Error(LogClass.General, "LogController", "Index", "Test of an error message", new Exception("Test of an error exception"));

            return this.Ok();
        }
    }
}