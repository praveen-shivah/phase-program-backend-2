namespace InvoiceRepository;

using DatabaseContext;

public interface IInvoiceListResellerRetrieve
{
    Task<InvoiceListResellerRetrieveResponse> InvoiceListResellerRetrieveAsync(DataContext context, InvoiceListResellerRetrieveRequest request);
}