namespace TransferRepository;

using APISupportTypes;

using DatabaseContext;

public class TransferPointsQueueGetOutstandingItemsResponse : BaseResponseDto
{
    public List<TransferPointsQueue> Items { get; set; } = new List<TransferPointsQueue>();
}