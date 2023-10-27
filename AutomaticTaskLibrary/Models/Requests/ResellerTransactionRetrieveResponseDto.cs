using APISupportTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskSharedLibrary
{
    public class ResellerTransactionRetrieveResponseDto : BaseResponseDto
    {
        public ResellerTransactionDetail[] Details { get; set; }
        public bool IsSuccessful { get; set; }
    }


    public class ResellerTransactionDetail
    {
        public string Time { get; set; }
        public string Station { get; set; }
        public string CustomerID { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Comps { get; set; }
        public string Free { get; set; }
        public int OrganizationId { get; set; }
        public int ResellerId { get; set; }
        public int VendorId { get; set; }
    }
}
