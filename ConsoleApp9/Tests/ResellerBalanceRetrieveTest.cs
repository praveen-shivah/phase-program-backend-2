namespace ConsoleApp9
{
    using ApiDTO;

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
            var request = new ResellerBalanceRetrieveRequestDto
                                                   {
                                                       OrganizationId = "1",
                                                       ApiKey = "test",
                                                       ResellerId = 1,
                                                       Password = "239239",
                                                       SoftwareType = SoftwareTypeEnum.riverSweeps,
                                                       UserId = "golddist"
                                                   };
            var response = this.vendorBalanceRetrieve.GetBalance(request);
        }
    }
}