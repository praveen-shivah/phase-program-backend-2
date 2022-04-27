namespace AutomaticTaskLibrary
{
    using InvoiceRepositoryTypes;

    public class AutomaticTaskTransferPoints : IAutomaticTask
    {
        public VendorToOperatorSendPointsTransferRequest VendorToOperatorSendPointsTransferRequest { get; set; }
    }
}
