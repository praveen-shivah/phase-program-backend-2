namespace ConsoleApp9
{
    using ApiDTO;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepository;

    public class DistributorToResellerSendPointsTransferTest : IDistributorToResellerSendPointsTransferTest
    {
        private readonly IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer;

        public DistributorToResellerSendPointsTransferTest(IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer)
        {
            this.distributorToOperatorSendPointsTransfer = distributorToOperatorSendPointsTransfer;
        }

        void IDistributorToResellerSendPointsTransferTest.RunTest()
        {
            var vendorToOperatorSendPointsTransferRequest = new DistributorToResellerSendPointsTransferRequestDto
                                                                {
                                                                    AccountId = "goldshop",
                                                                    Password = "239239",
                                                                    Points = 1,
                                                                    SoftwareType = SoftwareTypeEnum.riverSweeps,
                                                                    UserId = "golddist"
            };
            var response = this.distributorToOperatorSendPointsTransfer.SendPointsTransfer(vendorToOperatorSendPointsTransferRequest);
        }
    }
}