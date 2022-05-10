namespace AutomaticTaskLibrary
{
    public class AutomaticTaskResellerBalanceRetrieve : IAutomaticTask
    {
        public ResellerBalanceRetrieveRequest VendorBalanceRetrieveRequest { get; set; }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.vendorBalanceRetrieve;
    }
}
