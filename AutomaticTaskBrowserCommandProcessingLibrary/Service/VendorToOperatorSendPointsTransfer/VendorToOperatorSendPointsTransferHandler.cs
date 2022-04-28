namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public class VendorToOperatorSendPointsTransferHandler : IVendorToOperatorSendPointsTransferHandler
    {
        private readonly IBrowserContextFactory browserContextFactory;

        public VendorToOperatorSendPointsTransferHandler(IBrowserContextFactory browserContextFactory)
        {
            this.browserContextFactory = browserContextFactory;
        }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.vendorToOperatorSendPointsTransfer;

        public Task<bool> Execute(IAutomaticTask message)
        {
            var automaticTaskTransferPoints = (AutomaticTaskTransferPoints)message;
            Task.Factory.StartNew(
                () =>
                    {
                        var softwareType = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.SoftwareType;
                        var url = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.SiteUrl;
                        var userId = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.UserId;
                        var password = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.Password;
                        var accountId = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.AccountId;
                        var points = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.Points;

                        // Login Page
                        // Transfer Page
                        //   Transfer points button
                    });

            throw new NotImplementedException();
        }
    }
}
