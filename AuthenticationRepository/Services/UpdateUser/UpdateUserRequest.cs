namespace AuthenticationRepository
{
    using ApiDTO;

    public class UpdateUserRequest
    {
        public UpdateUserRequest(int organizationId, UserDto userDto)
        {
            this.OrganizationId = organizationId;
            this.UserDto = userDto;
        }

        public int OrganizationId { get; }

        public UserDto UserDto { get; }
    }
}