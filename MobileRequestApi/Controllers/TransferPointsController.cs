namespace ApiHost
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiDTO;
    using APISupport;

    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using SharedUtilities.Extensions;

    using TransferRepository;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/transferpoints")]
    public class TransferPointsController : ApiControllerBase
    {
        private readonly ILogger logger;

        private readonly ITransferPointsQueueGetOutstandingItemsRepository transferPointsQueueGetOutstandingItemsRepository;

        public TransferPointsController(ILogger logger, ITransferPointsQueueGetOutstandingItemsRepository transferPointsQueueGetOutstandingItemsRepository)
        {
            this.logger = logger;
            this.transferPointsQueueGetOutstandingItemsRepository = transferPointsQueueGetOutstandingItemsRepository;
        }

        [HttpPost("get-transfer-queue")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<ActionResult<TransferPointsQueueGetOutstandingItemsResponseDto>> GetTransferQueue(TransferPointsQueueGetOutstandingItemsRequestDto transferPointsQueueGetOutstandingItemsRequestDto)
        {
            this.logger.Debug(LogClass.General, "GetTransferQueue");
            var request = new TransferPointsQueueGetOutstandingItemsRequest { OrganizationId = transferPointsQueueGetOutstandingItemsRequestDto.OrganizationId };
            var response = await this.transferPointsQueueGetOutstandingItemsRepository.TransferPointsQueueGetOutstandingItemsAsync(request);

            var items = new List<TransferPointsQueueDto>();
            foreach (var item in response.Items)
            {
                items.Add(
                    new TransferPointsQueueDto()
                    {
                        AccountId = item.AccountId,
                        DateTimeProcessStarted = item.DateTimeProcessStarted,
                        DateTimeSent = item.DateTimeSent,
                        Id = item.Id,
                        Points = item.Points,
                        SoftwareType = ((SoftwareTypeEnum)item.SoftwareType).GetEnumAttributeDescription(),
                        UserId = item.UserId,
                        TransferPointsQueueType = ((TransferPointsQueueTypeEnum)item.TransferPointsQueueTypeId).GetEnumAttributeDescription()
                    }); ;
            }

            var result = new TransferPointsQueueGetOutstandingItemsResponseDto
            {
                IsSuccessful = response.IsSuccessful,
                ErrorMessage = response.ErrorMessage,
                HttpStatusCode = response.HttpStatusCode,
                Items = items,
                ResponseTypeEnum = response.ResponseTypeEnum
            };

            return this.Ok(result);
        }
    }
}