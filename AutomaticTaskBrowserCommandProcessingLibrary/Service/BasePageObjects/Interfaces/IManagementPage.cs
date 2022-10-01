namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IManagementPage
    {
        bool IsPageUrlSet();

        bool LocateDepositButtonAndClick(string userId);

        bool VerifyFundsAvailable(int points);

        bool MakeDeposit(int amountInPennies, string invoiceLineItemId);

        bool VerifyPageLoaded();

        string GetBalance();
    }
}
