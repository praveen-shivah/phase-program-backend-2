namespace ResellerRepositoryTypes
{
    using DatabaseContext;

    public class UpdateResellerResponse
    {
        public bool IsSuccessful { get; set; }

        public Reseller Reseller { get; set; }
    }
}
