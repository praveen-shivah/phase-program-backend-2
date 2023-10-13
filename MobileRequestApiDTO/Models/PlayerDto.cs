namespace ApiDTO
{
    public class PlayerDto: BaseDto
    {
        public string PlayerId { get; set; }
        public string MobileId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public string Balance { get; set; }
        public int OrganizationId { get; set; }
        public int ResellerId { get; set; }
        public int VendorId { get; set; }

    }
}
