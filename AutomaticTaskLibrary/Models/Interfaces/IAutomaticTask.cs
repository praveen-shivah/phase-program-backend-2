namespace AutomaticTaskLibrary
{
    using NServiceBus;

    public enum AutomaticTaskType
    {
        notSet,
        vendorToOperatorSendPointsTransfer
    }

    public interface IAutomaticTask : ICommand
    {
        AutomaticTaskType AutomaticTaskType { get; }
    }
}