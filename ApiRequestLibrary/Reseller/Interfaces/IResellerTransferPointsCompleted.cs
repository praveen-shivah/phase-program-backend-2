namespace ApiRequestLibrary;

using ApiDTO;

public interface IResellerTransferPointsCompleted
{
    Task<bool> MarkAsCompleted(ResellerTransferPointsCompletedDto resellerTransferPointsCompleted);
}