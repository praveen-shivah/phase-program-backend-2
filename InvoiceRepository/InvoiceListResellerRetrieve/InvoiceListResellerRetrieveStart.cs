namespace InvoiceRepository;

using DatabaseContext;

public class InvoiceListResellerRetrieveStart : IInvoiceListResellerRetrieve
{
    Task<InvoiceListResellerRetrieveResponse> IInvoiceListResellerRetrieve.InvoiceListResellerRetrieveAsync(DataContext context, InvoiceListResellerRetrieveRequest request)
    {
        return Task.FromResult(new InvoiceListResellerRetrieveResponse() { IsSuccessful = true });
    }
}