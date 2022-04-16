namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class BillingAddress : BaseEntity
    {
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Attention { get; set; }
        public string Street2 { get; set; }
        public string State { get; set; }
        public string Fax { get; set; }
    }
}