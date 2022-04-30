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
                                                                    SiteUrl = "",
                                                                    AccountId = "1234",
                                                                    Password = "",
                                                                    Points = 100,
                                                                    SoftwareType = SoftwareType.riverSweeps,
                                                                    UserId = ""
                                                                };
            var response = this.vendorToOperatorSendPointsTransfer.SendPointsTransfer(vendorToOperatorSendPointsTransferRequest);
        }
    }
}