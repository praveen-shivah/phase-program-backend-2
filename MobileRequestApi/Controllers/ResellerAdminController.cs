﻿namespace ApiHost
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
    [Route("api/reseller")]
    public class ResellerAdminController : ApiControllerBase
    {
        private readonly ILogger logger;

        private readonly IResellerBalanceService resellerBalanceService;

        private readonly IResellerRepository resellerRepository;

        private readonly IUpdateResellerBalanceRepository updateResellerBalanceRepository;

        private readonly IUpdateResellerSiteRepository updateResellerSiteRepository;

        public ResellerAdminController(ILogger logger,
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

        [HttpPost("reseller-balance")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<IActionResult> UpdateBalance(ResellerBalanceDTO resellerBalance)
        {
            this.logger.Debug(LogClass.General, "ResellerBalance received");
            var result = await this.resellerBalanceService.UpdateBalance(resellerBalance);
            return result ? this.Ok() : this.StatusCode(500, 0);
        }

        [HttpPost("reseller-transfer-points-completed")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<IActionResult> ResellerTransferPointsCompleted(ResellerTransferPointsCompletedDto resellerTransferPointsCompleted)
        {
            this.logger.Debug(LogClass.General, "ResellerTransferPointsCompleted received");
            var result = await this.resellerBalanceService.TransferPointsCompleted(resellerTransferPointsCompleted);
            return result ? this.Ok() : this.StatusCode(500, 0);
        }

        [HttpGet("get-resellers")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<ActionResult<List<ResellerDto>>> GetResellers()
        {
            this.logger.Debug(LogClass.General, "GetResellers received");

            var result = await this.resellerRepository.GetResellers(this.OrganizationId);
            return this.Ok(result);
        }

        [HttpPost("update-reseller-balance")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<ActionResult<List<UpdateResellerBalanceResponseDto>>> UpdateResellerBalance(UpdateResellerBalanceRequestDto request)
        {
            this.logger.Debug(LogClass.General, "UpdateResellerBalanceRepository received");

            var response = await this.updateResellerBalanceRepository.UpdateResellerBalanceAsync(
                             new UpdateResellerBalanceRequest()
                             {
                                 OrganizationId = this.OrganizationId,
                                 Balance = request.Balance,
                                 ResellerId = request.ResellerId
                             });
            var result = new UpdateResellerBalanceResponseDto()
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.ErrorMessage,
                HttpStatusCode = response.HttpStatusCode,
                ResponseTypeEnum = response.ResponseTypeEnum
            };

            return this.Ok(result);
        }

        [HttpGet("get-reseller-sites")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<ActionResult<List<SiteInformationDto>>> GetResellerSites(int resellerId)
        {
            this.logger.Debug(LogClass.General, "GetResellerSites received");

            var result = await this.resellerRepository.GetResellerSites(this.OrganizationId, resellerId);
            return this.Ok(result);
        }

        [HttpPost("update-reseller")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_USER)]
        public async Task<IActionResult> UpdateReseller(ResellerDto resellerDto)
        {
            this.logger.Debug(LogClass.General, "UpdateReseller received");

            var result = await this.resellerRepository.UpdateResellerRequestAsync(this.OrganizationId, resellerDto);
            return this.Ok(result);
        }

        [HttpPost("update-reseller-site")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<IActionResult> UpdateResellerSite(UpdateResellerSiteRequestDto updateResellerSiteRequestDto)
        {
            this.logger.Debug(LogClass.General, "UpdateResellerSite received");
            var result = await this.updateResellerSiteRepository.UpdateResellerSiteAsync(
                new UpdateResellerSiteRequest()
                {
                    OrganizationId = this.OrganizationId,
                    IgnoreResellerId = true,
                    AccountId = updateResellerSiteRequestDto.AccountId,
                    Id = updateResellerSiteRequestDto.Id,
                    LoginPassword = updateResellerSiteRequestDto.LoginPassword,
                    LoginUsername = updateResellerSiteRequestDto.LoginUsername
                });
            return this.Ok(result);
        }
    }
}