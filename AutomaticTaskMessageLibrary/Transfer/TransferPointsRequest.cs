namespace AutomaticTaskMessageLibrary
{
    using NServiceBus;

    public class TransferPointsRequest : ICommand
    {
        public int SiteId { get; set; }
        public int OperatorId { get; set; }
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
}
