namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IBrowserContext
    {
        public bool Start();

        public bool Stop();

        public bool NavigateToPage(string url, bool forceRefresh = false);
    }
}
