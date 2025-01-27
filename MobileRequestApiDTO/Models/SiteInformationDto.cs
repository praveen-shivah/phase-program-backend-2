﻿namespace ApiDTO
{
    public class SiteInformationDto : BaseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Item_Id { get; set; }
        public string Url { get; set; }
        public string AccountId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public string VendorId { get; set; }
        public int Balance { get; set; }
    }
}
