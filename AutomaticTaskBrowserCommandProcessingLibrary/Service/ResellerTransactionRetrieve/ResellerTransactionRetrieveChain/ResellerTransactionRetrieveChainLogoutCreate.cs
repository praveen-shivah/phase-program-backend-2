using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainLogoutCreate : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        private readonly ILogoutPageFactory logoutPageFactory;

        public ResellerTransactionRetrieveChainLogoutCreate(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain, ILogoutPageFactory logoutPageFactory)
        {
            this.resellerTransactionRetrieveChain = resellerTransactionRetrieveChain;
            this.logoutPageFactory = logoutPageFactory;
        }

        ResellerTransactionRetrieveResponse IResellerTransactionRetrieveChain.Execute(IWebDriver driver, ResellerTransactionRetrieveRequest resellerTransactionRetrieveRequest)
        {
            var response = this.resellerTransactionRetrieveChain.Execute(driver, resellerTransactionRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerTransactionRetrieveResponseType.logoutCreate;

            try
            {
                response.LogoutPage = this.logoutPageFactory.Create(driver, resellerTransactionRetrieveRequest.LoginPageInformation.SoftwareType);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
