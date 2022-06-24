namespace AuthenticationRepository
{
    using DataModelsLibrary;

    public class UpdateUserResponse
    {
        public bool IsSuccessful { get; set; }

        public User User { get; set; }
    }
}
