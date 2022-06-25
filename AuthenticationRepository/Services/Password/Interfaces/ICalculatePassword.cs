namespace AuthenticationRepository
{
    public interface ICalculatePassword
    {
        string calculatePassword(string password, string passwordSalt);
    }
}
