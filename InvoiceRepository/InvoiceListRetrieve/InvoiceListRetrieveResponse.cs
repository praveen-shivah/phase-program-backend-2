﻿namespace InvoiceRepository;

using ApiDTO;

public class InvoiceListRetrieveResponse
{
    public bool IsSuccessful { get; set; }

    public List<InvoiceDataDto> InvoiceList { get; set; } = new List<InvoiceDataDto>();
}