namespace ConsoleApp9
{
    using AutomaticTaskSharedLibrary;

    using InvoiceRepository;

    public class ResellerBalanceRetrieveTest : IResellerBalanceRetrieveTest
    {
        private readonly IResellerBalanceRetrieve vendorBalanceRetrieve;

        public ResellerBalanceRetrieveTest(IResellerBalanceRetrieve vendorBalanceRetrieve)
        {
            this.vendorBalanceRetrieve = vendorBalanceRetrieve;
        }

        void IResellerBalanceRetrieveTest.RunTest()
        {
            var request = new ResellerBalanceRetrieveRequest
                                                   {
                                                       OrganizationId = "1",
                                                       ApiKey = "test",
                                                       ResellerId = 1,
                                                       Password = "239239",
                                                       SoftwareType = SoftwareType.riverSweeps,
                                                       UserId = "golddist"
                                                   };
            var response = this.vendorBalanceRetrieve.GetBalance(request);
        }
    }
}