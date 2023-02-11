namespace InvoiceRepository;

using DatabaseContext;

public interface IInvoiceListRetrieve
{
    Task<InvoiceListRetrieveResponse> InvoiceListRetrieveAsync(DataContext context, InvoiceListRetrieveRequest request);
}