namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public abstract class BaseVendorToOperatorTransferLogin : IVendorToOperatorTransferLoginPage
    {
        private readonly IWebDriver driver;

        private readonly VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest;

        protected BaseVendorToOperatorTransferLogin(IWebDriver driver,
                                                    VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            this.driver = driver;
            this.vendorToOperatorSendPointsTransferRequest = vendorToOperatorSendPointsTransferRequest;
        }

        IVendorToOperatorTransferManagementPage? IVendorToOperatorTransferLoginPage.Submit()
        {
            try
            {
                return this.submit();
            }
            catch (Exception e)
            {
            }

            return null;
        }

        bool IVendorToOperatorTransferLoginPage.VerifyPageLoaded()
        {
            try
            {
                return this.verifyPageLoaded();
            }
            catch (Exception e)
            {
            }

            return false;
        }

        bool IVendorToOperatorTransferLoginPage.VerifyPageUrl()
        {
            try
            {
                return this.verifyPageUrl();
            }
            catch (Exception e)
            {
            }

            return false;
        }

        protected abstract IVendorToOperatorTransferManagementPage? submit();

        protected abstract bool verifyPageLoaded();

        protected abstract bool verifyPageUrl();
    }
}