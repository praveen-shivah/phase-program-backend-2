namespace AuthenticationRepository
{
    public class LogoutRequest
    {
        public LogoutRequest(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; }
    }
}