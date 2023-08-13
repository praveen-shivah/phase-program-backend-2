using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainLoginSubmit : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        private readonly ITransactionReportsPageFactory transactionReportsPageFactory;

        public ResellerTransactionRetrieveChainLoginSubmit(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain, ITransactionReportsPageFactory transactionReportsPageFactory)
        {
            this.resellerTransactionRetrieveChain = resellerTransactionRetrieveChain;
            this.transactionReportsPageFactory = transactionReportsPageFactory;
        }

        ResellerTransactionRetrieveResponse IResellerTransactionRetrieveChain.Execute(IWebDriver driver, ResellerTransactionRetrieveRequest resellerTransactionRetrieveRequest)
        {
            var response = this.resellerTransactionRetrieveChain.Execute(driver, resellerTransactionRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerTransactionRetrieveResponseType.loginSubmit;

            response.IsSuccessful = response.LoginPage.Submit();
            if (response.IsSuccessful)
            {
                response.TransactionReportsPage = this.transactionReportsPageFactory.Create(driver, resellerTransactionRetrieveRequest.SoftwareType);
            }

            return response;
        }
    }
}
