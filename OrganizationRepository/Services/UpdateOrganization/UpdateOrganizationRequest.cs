namespace OrganizationRepository
{
    using ApiDTO;

    public class UpdateOrganizationRequest
    {
        public UpdateOrganizationRequest(OrganizationDto updateOrganizationDto)
        {
            this.UpdateOrganizationDto = updateOrganizationDto;
        }

        public OrganizationDto UpdateOrganizationDto { get; }
    }
}