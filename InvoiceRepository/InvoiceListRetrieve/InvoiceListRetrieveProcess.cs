namespace InvoiceRepository;

using ApiDTO;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

public class InvoiceListRetrieveProcess : IInvoiceListRetrieve
{
    private IInvoiceListRetrieve invoiceListRetrieve;

    public InvoiceListRetrieveProcess(IInvoiceListRetrieve invoiceListRetrieve)
    {
        this.invoiceListRetrieve = invoiceListRetrieve;
    }

    async Task<InvoiceListRetrieveResponse> IInvoiceListRetrieve.InvoiceListRetrieveAsync(DataContext context, InvoiceListRetrieveRequest request)
    {
        var response = await this.invoiceListRetrieve.InvoiceListRetrieveAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        var list = await context.Invoice.Where(x => x.OrganizationId == request.OrganizationId).ToListAsync();
        foreach (var record in list)
        {
            response.InvoiceList.Add(
                new InvoiceDataDto
                {
                    BalanceFormatted = record.BalanceFormatted,
                    CreatedDate = record.CreatedTime,
                    CustomerName = record.CustomerName,
                    Id = record.Id,
                    InvoiceNumber = record.InvoiceNumber,
                    Status = record.Status
                });
        }

        return response;
    }
}