namespace ApiDTO;

public class InvoiceListResponseDto
{
    public bool IsSuccessful { get; set; }
    public List<InvoiceDataDto> InvoiceList { get; set; }
}