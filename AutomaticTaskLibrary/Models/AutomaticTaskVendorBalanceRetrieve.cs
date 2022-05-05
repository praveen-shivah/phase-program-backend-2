namespace AutomaticTaskLibrary
{
    public class AutomaticTaskVendorBalanceRetrieve : IAutomaticTask
    {
        public VendorBalanceRetrieveRequest VendorBalanceRetrieveRequest { get; set; }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.vendorBalanceRetrieve;
    }
}
