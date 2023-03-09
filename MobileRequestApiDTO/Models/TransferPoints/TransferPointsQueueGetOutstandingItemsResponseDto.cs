using ApiDTO;

namespace TransferRepository;

using RestServicesSupportTypes;

public class TransferPointsQueueGetOutstandingItemsResponseDto : BaseResponseDto
{
    public List<TransferPointsQueueDto> Items { get; set; } = new List<TransferPointsQueueDto>();
}