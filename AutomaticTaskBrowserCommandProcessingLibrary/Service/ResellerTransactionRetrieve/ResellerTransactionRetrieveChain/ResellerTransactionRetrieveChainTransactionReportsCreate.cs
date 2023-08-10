using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainTransactionReportsCreate : IResellerTransactionRetrieveChain
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        private readonly ITransactionReportsPageFactory transactionReportsPageFactory;

        public ResellerTransactionRetrieveChainTransactionReportsCreate(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain, ITransactionReportsPageFactory transactionReportsPageFactory)
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

            response.ResponseType = ResellerTransactionRetrieveResponseType.transactionReportCreate;

            try
            {
                response.TransactionReportsPage = this.transactionReportsPageFactory.Create(driver, resellerTransactionRetrieveRequest.SoftwareType);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
