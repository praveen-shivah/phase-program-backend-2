namespace ApiDTO
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SoftwareType SoftwareType { get; set; }
        public bool IsActive { get; set; }
    }
}
