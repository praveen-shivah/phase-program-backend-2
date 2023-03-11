using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    using ApiDTO;

    using APISupport;
    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using OrganizationRepositoryTypes;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    [AuthorizePolicy]
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
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<ActionResult<List<OrganizationDto>>> GetUsers()
        {
            this.logger.Debug(LogClass.General, "GetUsers received");

            var result = await this.organizationRepository.GetOrganizations();
            return this.Ok(result);
        }

        [HttpPost("update-organization")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<IActionResult> UpdateOrganization(OrganizationDto organizationDto)
        {
            this.logger.Debug(LogClass.General, "UpdateOrganization received");

            var result = await this.organizationRepository.UpdateOrganizationRequestAsync(organizationDto);
            return this.Ok(result);
        }
    }
}
