using ApiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class ResellerTransactionRetrieveRequest
    {
        public ResellerTransactionRetrieveRequest(
            SoftwareTypeEnum softwareType,
            int organizationId,
            string apiKey,
            int resellerId,
            string siteUserId,
            string sitePassword, int drawer, int vendorId)
        {
            this.SoftwareType = softwareType;
            this.OrganizationId = organizationId;
            this.ApiKey = apiKey;
            this.ResellerId = resellerId;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
            this.LoginPageInformation.LoginPage = this.ApiKey;
            this.Drawer = drawer;
            this.LoginPageInformation.Drawer = drawer;
            this.VendorId = vendorId;
        }

        public LoginPageInformation LoginPageInformation { get; }

        public SoftwareTypeEnum SoftwareType { get; }

        public int OrganizationId { get; }

        public string ApiKey { get; }

        public int ResellerId { get; }
        public int VendorId { get; }

        public int Drawer { get; }
    }
}
