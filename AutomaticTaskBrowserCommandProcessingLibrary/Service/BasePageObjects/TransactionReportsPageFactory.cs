using ApiDTO;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionRepositoryTypes;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class TransactionReportsPageFactory : ITransactionReportsPageFactory
    {
        private readonly ITransactionRepository transactionRepository;
        public TransactionReportsPageFactory(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        ITransactionReportsPage ITransactionReportsPageFactory.Create(IWebDriver webDriver, SoftwareTypeEnum softwareType)
        {
            switch (softwareType)
            {
                case SoftwareTypeEnum.goldenDragon:
                    return new GoldenDragonTransactionReportsPage(webDriver, transactionRepository);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
