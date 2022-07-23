namespace ResellerRepositoryTypes
{
    using ApiDTO;

    public class UpdateResellerRequest
    {
        public UpdateResellerRequest(int organizationId, ResellerDto resellerDto)
        {
            this.OrganizationId = organizationId;
            this.ResellerDto = resellerDto;
        }

        public int OrganizationId { get; }

        public ResellerDto ResellerDto { get; }
    }
}