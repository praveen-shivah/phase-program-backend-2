namespace TransferRepository;

using DatabaseContext;

using RestServicesSupportTypes;

public class TransferPointsQueueGetOutstandingItemsResponse : BaseResponseDto
{
    public List<TransferPointsQueue> Items { get; set; } = new List<TransferPointsQueue>();
}