using AutomaticTaskSharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface ITransactionReportsPage
    {
        bool IsPageUrlSet();
        bool VerifyPageLoaded();
        ResellerTransactionDetail[] FetchReport();
    }
}
