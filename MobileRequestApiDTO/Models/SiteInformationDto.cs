namespace ApiDTO
{
    public class SiteInformationDto : BaseDto
    {
        public string Description { get; set; }
        public string Item_Id { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VendorId { get; set; }
    }
}
