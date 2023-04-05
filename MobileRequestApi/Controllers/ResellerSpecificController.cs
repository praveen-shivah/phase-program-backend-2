namespace ApiHost
{
    using ApiDTO;

    using APISupport;

    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using ResellerRepository;

    using ResellerRepositoryTypes;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [AuthorizePolicy]
    [Route("api/resellerspecific")]
    public class ResellerSpecificController : ApiControllerBase
    {
        private readonly ILogger logger;

        private readonly IResellerBalanceService resellerBalanceService;

        private readonly IResellerRepository resellerRepository;

        private readonly IUpdateResellerBalanceRepository updateResellerBalanceRepository;

        private readonly IUpdateResellerSiteRepository updateResellerSiteRepository;

        public ResellerSpecificController(ILogger logger,
                                          IResellerBalanceService resellerBalanceService,
                                          IResellerRepository resellerRepository,
                                          IUpdateResellerBalanceRepository updateResellerBalanceRepository,
                                          IUpdateResellerSiteRepository updateResellerSiteRepository)
        {
            this.logger = logger;
            this.resellerBalanceService = resellerBalanceService;
            this.resellerRepository = resellerRepository;
            this.updateResellerBalanceRepository = updateResellerBalanceRepository;
            this.updateResellerSiteRepository = updateResellerSiteRepository;
        }


        [HttpGet("get-reseller-sites")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_RESELLER)]
        public async Task<ActionResult<List<SiteInformationDto>>> GetResellerSites()
        {
            this.logger.Debug(LogClass.General, "GetResellerSites received");
            var resellerId = this.ResellerId;

            var result = await this.resellerRepository.GetResellerSites(this.OrganizationId, resellerId);
            return this.Ok(result);
        }

        [HttpPost("update-reseller")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_RESELLER)]
        public async Task<IActionResult> UpdateReseller(ResellerDto resellerDto)
        {
            this.logger.Debug(LogClass.General, "UpdateReseller received");
            resellerDto.Id = this.ResellerId;

            var result = await this.resellerRepository.UpdateResellerRequestAsync(this.OrganizationId, resellerDto);
            return this.Ok(result);
        }

        [HttpPost("update-reseller-site")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_RESELLER)]
        public async Task<IActionResult> UpdateResellerSite(UpdateResellerSiteRequestDto updateResellerSiteRequestDto)
        {
            this.logger.Debug(LogClass.General, "UpdateResellerSite received");
            var result = await this.updateResellerSiteRepository.UpdateResellerSiteAsync(
                new UpdateResellerSiteRequest()
                {
                    OrganizationId = this.OrganizationId,
                    ResellerId = this.ResellerId,
                    AccountId = updateResellerSiteRequestDto.AccountId,
                    Id = updateResellerSiteRequestDto.Id,
                    LoginPassword = updateResellerSiteRequestDto.LoginPassword,
                    LoginUsername = updateResellerSiteRequestDto.LoginUsername
                });
            return this.Ok(result);
        }
    }
}