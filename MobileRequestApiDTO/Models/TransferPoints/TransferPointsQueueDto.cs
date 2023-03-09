namespace ApiDTO;
public class TransferPointsQueueDto
{
    public string AccountId { get; set; } = string.Empty;

    public DateTime? DateTimeProcessStarted { get; set; }

    public DateTime? DateTimeSent { get; set; }

    public int Id { get; set; }

    public int Points { get; set; }

    public string SoftwareType { get; set; } = string.Empty;

    public string UserId { get; set; } = null!;
}