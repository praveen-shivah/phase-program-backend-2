namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IResellerBalancePage
    {
        bool IsPageUrlSet();

        bool VerifyPageLoaded();

        string GetBalance();
    }
}
