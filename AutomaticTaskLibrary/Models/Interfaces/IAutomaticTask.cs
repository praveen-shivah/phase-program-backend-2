namespace AutomaticTaskSharedLibrary
{
    using NServiceBus;

    public enum AutomaticTaskType
    {
        distributorToResellerSendPointsTransfer,
        resellerBalanceRetrieve
    }

    public interface IAutomaticTask : ICommand
    {
        AutomaticTaskType AutomaticTaskType { get; }
    }
}