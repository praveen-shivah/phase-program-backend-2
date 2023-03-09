﻿namespace TransferRepository;

using DatabaseContext;

public interface ITransferPointsQueueGetOutstandingItems
{
    Task<TransferPointsQueueGetOutstandingItemsResponse> TransferPointsQueueGetOutstandingItemsAsync(DataContext context, TransferPointsQueueGetOutstandingItemsRequest request);
}