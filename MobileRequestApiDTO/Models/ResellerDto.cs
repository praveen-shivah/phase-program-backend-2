namespace ApiDTO
{
    public class ResellerDto : BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Balance { get; set; }
    }
}
