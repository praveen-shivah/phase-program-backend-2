namespace AuthenticationRepository
{
    using ApiDTO;

    public class UpdateUserRequest
    {
        public UpdateUserRequest(string jwtToken, int organizationId, UpdateUserRequestDto updateUserRequestDto)
        {
            this.JwtToken = jwtToken;
            this.OrganizationId = organizationId;
            this.UpdateUserRequestDto = updateUserRequestDto;
        }

        public string JwtToken { get; }

        public int OrganizationId { get; }

        public UpdateUserRequestDto UpdateUserRequestDto { get; }
    }
}