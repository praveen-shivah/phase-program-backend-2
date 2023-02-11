namespace OrganizationRepository
{
    using DatabaseContext;

    public class UpdateOrganizationResponse
    {
        public bool IsSuccessful { get; set; }

        public Organization Organization { get; set; }
    }
}
