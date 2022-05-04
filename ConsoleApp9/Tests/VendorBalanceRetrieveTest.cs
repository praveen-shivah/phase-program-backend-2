namespace ConsoleApp9
{
    using AutomaticTaskLibrary;

    using InvoiceRepository;

    public class VendorBalanceRetrieveTest : IVendorBalanceRetrieveTest
    {
        private readonly IVendorBalanceRetrieve vendorBalanceRetrieve;

        public VendorBalanceRetrieveTest(IVendorBalanceRetrieve vendorBalanceRetrieve)
        {
            this.vendorBalanceRetrieve = vendorBalanceRetrieve;
        }

        void IVendorBalanceRetrieveTest.RunTest()
        {
            var request = new VendorBalanceRetrieveRequest
                                                   {
                                                       Password = "239239",
                                                       SoftwareType = SoftwareType.riverSweeps,
                                                       UserId = "golddist"
                                                   };
            var response = this.vendorBalanceRetrieve.GetBalance(request);
        }
    }
}