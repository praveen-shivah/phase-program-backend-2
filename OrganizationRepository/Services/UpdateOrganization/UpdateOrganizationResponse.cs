namespace OrganizationRepository
{
    using DataModelsLibrary;

    public class UpdateOrganizationResponse
    {
        public bool IsSuccessful { get; set; }

        public Organization Organization { get; set; }
    }
}
