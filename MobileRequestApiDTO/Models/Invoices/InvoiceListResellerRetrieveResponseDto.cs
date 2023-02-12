namespace ApiDTO;
public class InvoiceListResellerRetrieveResponseDto
{
    public bool IsSuccessful { get; set; }

    public List<InvoiceDataDto> InvoiceList { get; set; } = new List<InvoiceDataDto>();
}