namespace ApiDTO
{
    public class VendorDto : BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public SoftwareTypeEnum SoftwareType { get; set; } = SoftwareTypeEnum.riverSweeps;

        public bool IsActive { get; set; }
    }
}
