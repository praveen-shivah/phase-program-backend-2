using ApiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskSharedLibrary
{
    public class ResellerTransactionRetrieveRequestDto
    {
        public int OrganizationId { get; set; }
        public string ApiKey { get; set; }
        public int ResellerId { get; set; }
        public SoftwareTypeEnum SoftwareType { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }
        public int Drawer { get; set; }
        public int VendorId { get; set; }
    }
}
