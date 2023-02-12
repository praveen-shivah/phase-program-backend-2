namespace InvoiceRepository;

using ApiDTO;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

public class InvoiceListResellerRetrieveProcess : IInvoiceListResellerRetrieve
{
    private readonly IInvoiceListResellerRetrieve invoiceListResellerRetrieve;

    public InvoiceListResellerRetrieveProcess(IInvoiceListResellerRetrieve invoiceListResellerRetrieve)
    {
        this.invoiceListResellerRetrieve = invoiceListResellerRetrieve;
    }

    async Task<InvoiceListResellerRetrieveResponse> IInvoiceListResellerRetrieve.InvoiceListResellerRetrieveAsync(
        DataContext context,
        InvoiceListResellerRetrieveRequest request)
    {
        var response = await this.invoiceListResellerRetrieve.InvoiceListResellerRetrieveAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        var list = await context.Invoice.Where(x => x.OrganizationId == request.OrganizationId && x.ResellerId == request.ResellerId).ToListAsync();
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