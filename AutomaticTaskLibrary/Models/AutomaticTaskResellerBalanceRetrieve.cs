namespace AutomaticTaskSharedLibrary
{
    public class AutomaticTaskResellerBalanceRetrieve : CallBackInformationRequest
    {
        public ResellerBalanceRetrieveRequest ResellerBalanceRetrieveRequest { get; set; }

        protected override AutomaticTaskType getAutomaticTaskType()
        {
            return AutomaticTaskType.resellerBalanceRetrieve;
        }
    }
}
