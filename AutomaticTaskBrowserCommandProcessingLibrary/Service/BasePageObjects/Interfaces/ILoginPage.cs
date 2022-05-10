namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface ILoginPage
    {
        bool Submit();

        bool VerifyPageLoaded();

        bool VerifyPageUrl();
    }
}
