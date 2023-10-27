using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainTransactionReportsRetrive : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        public ResellerTransactionRetrieveChainTransactionReportsRetrive(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain)
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

            response.ResponseType = ResellerTransactionRetrieveResponseType.transactionReportRetrive;

            response.Details = response.TransactionReportsPage.SaveReport(resellerTransactionRetrieveRequest);

            return response;
        }
    }
}
