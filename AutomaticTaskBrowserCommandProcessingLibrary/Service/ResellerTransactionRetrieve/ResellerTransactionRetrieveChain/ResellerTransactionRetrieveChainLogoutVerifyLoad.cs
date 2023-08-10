using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainLogoutVerifyLoad : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        public ResellerTransactionRetrieveChainLogoutVerifyLoad(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain)
        {
            this.resellerTransactionRetrieveChain = resellerTransactionRetrieveChain;
        }

        ResellerTransactionRetrieveResponse IResellerTransactionRetrieveChain.Execute(IWebDriver driver, ResellerTransactionRetrieveRequest resellerTransactionRetrieveRequest)
        {
            var response = this.resellerTransactionRetrieveChain.Execute(driver, resellerTransactionRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.LogoutPage.Logout();
            response.ResponseType = ResellerTransactionRetrieveResponseType.logoutVerifyLoad;
            response.LogoutPage.VerifyPageUrl();

            return response;
        }
    }
}
