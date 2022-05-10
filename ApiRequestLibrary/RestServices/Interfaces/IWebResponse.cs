namespace ApiRequestLibrary
{
    public interface IWebResponse
    {
        bool IsSuccessful { get; }

        string Error { get; }

        string Text { get; }

        IReadOnlyList<byte> Bytes { get; }
    }
}
