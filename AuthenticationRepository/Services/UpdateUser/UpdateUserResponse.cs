namespace AuthenticationRepository
{
    using DatabaseContext;

    using RestServicesSupportTypes;

    public class UpdateUserResponse : BaseResponseDto
    {
        public User User { get; set; }
    }
}
