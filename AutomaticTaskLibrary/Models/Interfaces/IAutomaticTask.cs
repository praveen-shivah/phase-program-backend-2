namespace AutomaticTaskLibrary
{
    using NServiceBus;

    public enum AutomaticTaskType
    {
        distributorToResellerSendPointsTransfer,
        vendorBalanceRetrieve
    }

    public interface IAutomaticTask : ICommand
    {
        AutomaticTaskType AutomaticTaskType { get; }
    }
}