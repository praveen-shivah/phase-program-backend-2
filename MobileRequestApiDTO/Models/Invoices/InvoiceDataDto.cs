namespace ApiDTO;

using System;

public class InvoiceDataDto
{
    public int Id { get; set; }    
    public string CustomerName { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string InvoiceNumber { get; set; }
    public string BalanceFormatted { get; set; }
}