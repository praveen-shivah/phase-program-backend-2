namespace ApiDTO;

public class ResellerTransferPointsCompletedDto : CallBackInformationDTO
{
    public bool IsSuccessful { get; set; }

    public int InvoiceLineItemId { get; set; }
}