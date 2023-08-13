using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveAdapter : IResellerTransactionRetrieveAdapter
    {
        private readonly IResellerTransactionRetrieveChain resellerTransactionRetrieveChain;

        public ResellerTransactionRetrieveAdapter(IResellerTransactionRetrieveChain resellerTransactionRetrieveChain)
        {
            this.resellerTransactionRetrieveChain = resellerTransactionRetrieveChain;
        }

        ResellerTransactionRetrieveResponse IResellerTransactionRetrieveAdapter.Execute(IWebDriver driver, ResellerTransactionRetrieveRequest resellerTransactionRetrieveRequest)
        {
            return this.resellerTransactionRetrieveChain.Execute(driver, resellerTransactionRetrieveRequest);
        }
    }
}
