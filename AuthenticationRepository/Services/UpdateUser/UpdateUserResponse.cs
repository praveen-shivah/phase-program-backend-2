namespace AuthenticationRepository
{
    using DatabaseContext;

    public class UpdateUserResponse
    {
        public bool IsSuccessful { get; set; }

        public User User { get; set; }
    }
}
