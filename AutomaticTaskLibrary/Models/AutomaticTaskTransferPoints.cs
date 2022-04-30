namespace AutomaticTaskLibrary
{
    public class AutomaticTaskTransferPoints : IAutomaticTask
    {
        public VendorToOperatorSendPointsTransferRequest VendorToOperatorSendPointsTransferRequest { get; set; }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.vendorToOperatorSendPointsTransfer;
    }
}
