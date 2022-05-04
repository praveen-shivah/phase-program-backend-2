namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IVendorToOperatorTransferManagementPage
    {
        bool IsPageUrlSet();

        bool LocateDepositButtonAndClick(string userId);

        bool VerifyFundsAvailable(int points);

        bool MakeDeposit(int amount);

        bool VerifyPageLoaded();
    }
}
