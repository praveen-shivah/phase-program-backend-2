using AutomaticTaskSharedLibrary;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public abstract class BaseTransactionReportsPage : BasePage, ITransactionReportsPage
    {
        protected BaseTransactionReportsPage(IWebDriver driver)
            : base(driver)
        {
        }

        bool ITransactionReportsPage.IsPageUrlSet()
        {
            try
            {
                return this.isPageUrlSet();
            }
            catch
            {
            }

            return false;
        }

        bool ITransactionReportsPage.VerifyPageLoaded()
        {
            try
            {
                return this.verifyPageLoaded();
            }
            catch
            {
            }

            return false;
        }

        ResellerTransactionDetail[] ITransactionReportsPage.SaveReport(ResellerTransactionRetrieveRequest request)
        {
            try
            {
                return this.saveReport(request);
            }
            catch
            {
            }
            return null;
        }

        protected abstract bool isPageUrlSet();

        protected abstract bool verifyPageLoaded();

        protected abstract ResellerTransactionDetail[] saveReport(ResellerTransactionRetrieveRequest request);
    }
}
