namespace AuthenticationRepository
{
    using APISupportTypes;

    using DatabaseContext;

    public class UpdateUserResponse : BaseResponseDto
    {
        public User User { get; set; }
    }
}
