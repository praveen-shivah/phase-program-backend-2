namespace AuthenticationRepository
{
    using ApiDTO;

    public class UpdateUserRequest
    {
        public UpdateUserRequest(UserDto userDto)
        {
            this.UserDto = userDto;
        }

        public UserDto UserDto { get; }
    }
}