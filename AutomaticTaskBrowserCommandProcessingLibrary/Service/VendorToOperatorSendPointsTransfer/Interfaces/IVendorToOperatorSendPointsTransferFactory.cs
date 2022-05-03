namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public interface IVendorToOperatorSendPointsTransferFactory
    {
        IVendorToOperatorSendPointsTransferAdapter Create(SoftwareType softwareType);
    }
}
