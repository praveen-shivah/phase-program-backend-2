namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IManagementPage
    {
        bool IsPageUrlSet();

        bool LocateDepositButtonAndClick(string userId);

        bool VerifyFundsAvailable(int points);

        bool MakeDeposit(int amount);

        bool VerifyPageLoaded();

        string GetBalance();
    }
}
