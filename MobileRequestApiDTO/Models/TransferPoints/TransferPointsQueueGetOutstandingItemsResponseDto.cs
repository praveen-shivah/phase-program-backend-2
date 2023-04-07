using ApiDTO;

namespace TransferRepository;

using APISupportTypes;

public class TransferPointsQueueGetOutstandingItemsResponseDto : BaseResponseDto
{
    public List<TransferPointsQueueDto> Items { get; set; } = new List<TransferPointsQueueDto>();
}