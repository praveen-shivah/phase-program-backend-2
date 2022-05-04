namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IVendorToOperatorTransferLoginPage
    {
        IVendorToOperatorTransferManagementPage? Submit();

        bool VerifyPageLoaded();

        bool VerifyPageUrl();
    }
}
