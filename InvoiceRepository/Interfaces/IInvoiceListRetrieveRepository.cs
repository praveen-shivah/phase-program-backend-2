namespace InvoiceRepository;

public interface IInvoiceListRetrieveRepository
{
    Task<InvoiceListRetrieveResponse> InvoiceListRetrieveAsync(InvoiceListRetrieveRequest request);
}