using ApiDTO;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface ITransactionReportsPageFactory
    {
        ITransactionReportsPage Create(IWebDriver webDriver, SoftwareTypeEnum softwareType);
    }
}
