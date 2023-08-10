using AutomaticTaskSharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum ResellerTransactionRetrieveResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        logoutCreate,
        logoutVerifyLoad,
        transactionReportCreate,
        transactionReportVerifyLoad,
        transactionReportRetrive,
        apiStore
    }

    public class ResellerTransactionRetrieveResponse
    {
        public bool IsSuccessful { get; set; }

        public ResellerTransactionRetrieveResponseType ResponseType { get; set; }

        public ILoginPage LoginPage { get; set; }

        public ITransactionReportsPage TransactionReportsPage { get; set; }

        public ILogoutPage LogoutPage { get; set; }
        public ResellerTransactionDetail[] Details { get; set; }
    }

}
