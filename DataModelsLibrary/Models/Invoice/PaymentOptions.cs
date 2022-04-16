namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class PaymentOptions : BaseEntity
    {
        public List<object> PaymentGateways { get; set; }
    }
}