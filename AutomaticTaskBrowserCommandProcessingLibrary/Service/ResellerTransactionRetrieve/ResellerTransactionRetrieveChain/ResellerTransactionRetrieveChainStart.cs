using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveChainStart : IResellerTransactionRetrieveChain
    {
        ResellerTransactionRetrieveResponse IResellerTransactionRetrieveChain.Execute(
            IWebDriver driver,
            ResellerTransactionRetrieveRequest resellerTransactionRetrieveRequest)
        {
            return new ResellerTransactionRetrieveResponse
            {
                IsSuccessful = true,
                ResponseType = ResellerTransactionRetrieveResponseType.start
            };
        }
    }
}
