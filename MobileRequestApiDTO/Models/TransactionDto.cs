
namespace ApiDTO
{
    public class TransactionDto:BaseDto
    {
        public string CustomerID { get; set; }
        public string Time { get; set; }
        public string Station { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Comps { get; set; }
        public string Free { get; set; }
        public int OrganizationId { get; set; }
        public int ResellerId { get; set; }
        public int VendorId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
    }
}
