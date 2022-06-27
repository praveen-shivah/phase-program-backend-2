using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiDTO;

    using ApiHost.Middleware;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Cors;

    using OrganizationRepositoryTypes;

    [Authorize]
    [ApiController]
    [Route("api/organization")]
    public class OrganizationController : Controller
    {
        private readonly ILogger logger;

        private readonly IOrganizationRepository organizationRepository;

        public OrganizationController(ILogger logger, IOrganizationRepository organizationRepository)
        {
            this.logger = logger;
            this.organizationRepository = organizationRepository;
        }

        [HttpGet("get-organizations")]
        public async Task<ActionResult<List<OrganizationDto>>> GetUsers()
        {
            this.logger.Debug(LogClass.General, "GetUsers received");

            var result = await this.organizationRepository.GetOrganizations();
            return this.Ok(result);
        }
    }
}
