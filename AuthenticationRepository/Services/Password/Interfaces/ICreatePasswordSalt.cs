namespace AuthenticationRepository
{
    public interface ICreatePasswordSalt
    {
        string CreateSalt(int size);
    }
}
