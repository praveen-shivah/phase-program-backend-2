﻿namespace TransferRepository;

public interface ITransferPointsQueueGetOutstandingItemsRepository
{
    Task<TransferPointsQueueGetOutstandingItemsResponse> TransferPointsQueueGetOutstandingItemsAsync(TransferPointsQueueGetOutstandingItemsRequest request);
}