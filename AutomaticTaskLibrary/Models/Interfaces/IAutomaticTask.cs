namespace AutomaticTaskLibrary
{
    using NServiceBus;

    public enum AutomaticTaskType
    {
        vendorToOperatorSendPointsTransfer,
        vendorBalanceRetrieve
    }

    public interface IAutomaticTask : ICommand
    {
        AutomaticTaskType AutomaticTaskType { get; }
    }
}