﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum VendorBalanceRetrieveResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        managementCreate,
        managementVerifyLoad,
        managementRetrieveBalance
    }

    public class ResellerBalanceRetrieveResponse
    {
        public bool IsSuccessful { get; set; }

        public VendorBalanceRetrieveResponseType ResponseType { get; set; }

        public ILoginPage LoginPage { get; set; }

        public IManagementPage ManagementPage { get; set; }

        public string VendorBalance { get; set; }
    }
}
