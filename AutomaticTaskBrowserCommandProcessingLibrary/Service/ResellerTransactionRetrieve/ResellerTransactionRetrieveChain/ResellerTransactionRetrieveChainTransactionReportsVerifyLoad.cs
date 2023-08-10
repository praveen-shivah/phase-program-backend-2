using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary.Service.ResellerTransactionRetrieve.ResellerTransactionRetrieveChain
{
    public class ResellerTransactionRetrieveChainTransactionReportsVerifyLoad : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        public ResellerTransactionRetrieveChainTransactionReportsVerifyLoad(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain)
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

            response.ResponseType = ResellerTransactionRetrieveResponseType.transactionReportVerifyLoad;
            
            if (response.TransactionReportsPage.IsPageUrlSet() && response.TransactionReportsPage.VerifyPageLoaded())
            {
                return response;
            }

            response.IsSuccessful = false;

            return response;
        }
    }
}
