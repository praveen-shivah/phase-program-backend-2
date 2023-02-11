namespace InvoiceRepository;

using DatabaseContext;

public class InvoiceListRetrieveStart : IInvoiceListRetrieve
{
    Task<InvoiceListRetrieveResponse> IInvoiceListRetrieve.InvoiceListRetrieveAsync(DataContext context, InvoiceListRetrieveRequest request)
    {
        return Task.FromResult(new InvoiceListRetrieveResponse() { IsSuccessful = true });
    }
}