namespace AutomaticTaskSharedLibrary
{
    public class AutomaticTaskTransferPoints : CallBackInformationRequest
    {
        public DistributorToResellerSendPointsTransferRequest DistributorToResellerSendPointsTransferRequest { get; set; }

        protected override AutomaticTaskType getAutomaticTaskType()
        {
            return AutomaticTaskType.distributorToResellerSendPointsTransfer;
        }
    }
}
