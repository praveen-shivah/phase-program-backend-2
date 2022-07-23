namespace ResellerRepositoryTypes
{
    using DataModelsLibrary;

    public class UpdateResellerResponse
    {
        public bool IsSuccessful { get; set; }

        public Reseller Reseller { get; set; }
    }
}
