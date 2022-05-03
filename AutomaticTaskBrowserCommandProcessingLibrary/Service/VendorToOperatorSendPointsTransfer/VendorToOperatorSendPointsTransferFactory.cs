namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public class VendorToOperatorSendPointsTransferFactory : IVendorToOperatorSendPointsTransferFactory
    {
        IVendorToOperatorSendPointsTransferAdapter IVendorToOperatorSendPointsTransferFactory.Create(SoftwareType softwareType)
        {
            switch (softwareType)
            {
                case SoftwareType.riverSweeps:
                    return new RiverSweepsVendorToOperatorTransferAdapterAdapter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
