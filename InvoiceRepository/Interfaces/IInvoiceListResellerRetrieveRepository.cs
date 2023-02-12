namespace InvoiceRepository;

public interface IInvoiceListResellerRetrieveRepository
{
    Task<InvoiceListResellerRetrieveResponse> InvoiceListResellerRetrieveAsync(InvoiceListResellerRetrieveRequest request);
}