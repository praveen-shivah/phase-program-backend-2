using AutomaticTaskBrowserCommandProcessingLibrary;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainLoginCreate : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        private readonly ILoginPageFactory loginPageFactory;

        public ResellerTransactionRetrieveChainLoginCreate(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain, ILoginPageFactory loginPageFactory)
        {
            this.resellerTransactionRetrieveChain = resellerTransactionRetrieveChain;
            this.loginPageFactory = loginPageFactory;
        }

        ResellerTransactionRetrieveResponse IResellerTransactionRetrieveChain.Execute(IWebDriver driver, ResellerTransactionRetrieveRequest resellerTransactionRetrieveRequest)
        {
            var response = this.resellerTransactionRetrieveChain.Execute(driver, resellerTransactionRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerTransactionRetrieveResponseType.loginCreate;

            try
            {
                response.LoginPage = this.loginPageFactory.Create(driver, resellerTransactionRetrieveRequest.LoginPageInformation);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
