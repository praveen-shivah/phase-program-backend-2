namespace ApiHost
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiDTO;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using ResellerRepository;

    using ResellerRepositoryTypes;

    [ApiController]
    [Route("api/reseller")]
    public class ResellerController : ApiControllerBase
    {
        private readonly ILogger logger;

        private readonly IResellerBalanceService resellerBalanceService;

        private readonly IResellerRepository resellerRepository;

        private readonly IUpdateResellerSiteRepository updateResellerSiteRepository;

        public ResellerController(ILogger logger, IResellerBalanceService resellerBalanceService, IResellerRepository resellerRepository, IUpdateResellerSiteRepository updateResellerSiteRepository)
        {
            this.logger = logger;
            this.resellerBalanceService = resellerBalanceService;
            this.resellerRepository = resellerRepository;
            this.updateResellerSiteRepository = updateResellerSiteRepository;
        }

        [HttpPost("reseller-balance")]
        public async Task<IActionResult> ResellerBalance(ResellerBalanceDTO resellerBalance)
        {
            this.logger.Debug(LogClass.General, "ResellerBalance received");
            var result = await this.resellerBalanceService.UpdateBalance(resellerBalance);
            return result ? this.Ok() : this.StatusCode(500, 0);
        }

        [HttpPost("reseller-transfer-points-completed")]
        public async Task<IActionResult> ResellerTransferPointsCompleted(ResellerTransferPointsCompletedDto resellerTransferPointsCompleted)
        {
            this.logger.Debug(LogClass.General, "ResellerTransferPointsCompleted received");
            var result = await this.resellerBalanceService.TransferPointsCompleted(resellerTransferPointsCompleted);
            return result ? this.Ok() : this.StatusCode(500, 0);
        }

        [HttpGet("get-resellers")]
        public async Task<ActionResult<List<ResellerDto>>> GetResellers()
        {
            this.logger.Debug(LogClass.General, "GetResellers received");

            var result = await this.resellerRepository.GetResellers(this.OrganizationId);
            return this.Ok(result);
        }

        [HttpGet("get-reseller-sites")]
        public async Task<ActionResult<List<SiteInformationDto>>> GetResellerSites(int resellerId)
        {
            this.logger.Debug(LogClass.General, "GetResellerSites received");

            var result = await this.resellerRepository.GetResellerSites(this.OrganizationId, resellerId);
            return this.Ok(result);
        }

        [HttpPost("update-reseller")]
        public async Task<IActionResult> UpdateReseller(ResellerDto resellerDto)
        {
            this.logger.Debug(LogClass.General, "UpdateReseller received");

            var result = await this.resellerRepository.UpdateResellerRequestAsync(this.OrganizationId, resellerDto);
            return this.Ok(result);
        }

        [HttpPost("update-reseller-site")]
        public async Task<IActionResult> UpdateResellerSite(UpdateResellerSiteRequestDto updateResellerSiteRequestDto)
        {
            this.logger.Debug(LogClass.General, "UpdateResellerSite received");
            var result = await this.updateResellerSiteRepository.UpdateResellerSiteAsync(
                new UpdateResellerSiteRequest()
                {
                    OrganizationId = this.OrganizationId,
                    AccountId = updateResellerSiteRequestDto.AccountId,
                    Id = updateResellerSiteRequestDto.Id,
                    LoginPassword = updateResellerSiteRequestDto.LoginPassword,
                    LoginUsername = updateResellerSiteRequestDto.LoginUsername
                });
            return this.Ok(result);
        }
    }
}