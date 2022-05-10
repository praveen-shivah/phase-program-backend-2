namespace AutomaticTaskLibrary
{
    public class AutomaticTaskTransferPoints : IAutomaticTask
    {
        public DistributorToResellerSendPointsTransferRequest DistributorToResellerSendPointsTransferRequest { get; set; }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.distributorToResellerSendPointsTransfer;
    }
}
