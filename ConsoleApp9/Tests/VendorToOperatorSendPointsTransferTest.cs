namespace ConsoleApp9
{
    using AutomaticTaskLibrary;

    using InvoiceRepository;

    public class VendorToOperatorSendPointsTransferTest : IVendorToOperatorSendPointsTransferTest
    {
        private readonly IVendorToOperatorSendPointsTransfer vendorToOperatorSendPointsTransfer;

        public VendorToOperatorSendPointsTransferTest(IVendorToOperatorSendPointsTransfer vendorToOperatorSendPointsTransfer)
        {
            this.vendorToOperatorSendPointsTransfer = vendorToOperatorSendPointsTransfer;
        }

        void IVendorToOperatorSendPointsTransferTest.RunTest()
        {
            var vendorToOperatorSendPointsTransferRequest = new VendorToOperatorSendPointsTransferRequest
                                                                {
                                                                    AccountId = "goldshop",
                                                                    Password = "239239",
                                                                    Points = 1,
                                                                    SoftwareType = SoftwareType.riverSweeps,
                                                                    UserId = "golddist"
            };
            var response = this.vendorToOperatorSendPointsTransfer.SendPointsTransfer(vendorToOperatorSendPointsTransferRequest);
        }
    }
}