namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class CustomerCustomFieldHash : BaseEntity
    {
        public string CfCustomerType { get; set; }

        public string CfCustomerTypeUnformatted { get; set; }

        public string CfSiteNumber { get; set; }

        public string CfSiteNumberUnformatted { get; set; }
    }
}