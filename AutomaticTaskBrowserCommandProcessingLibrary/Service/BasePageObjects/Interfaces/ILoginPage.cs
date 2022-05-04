namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface ILoginPage
    {
        IManagementPage? Submit();

        bool VerifyPageLoaded();

        bool VerifyPageUrl();
    }
}
