using PlayersRepositoryTypes;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IResellerPlayerPage
    {
        bool IsPageUrlSet();
        bool VerifyPageLoaded();
        ResellerPlayersDetail[] SavePlayersDetails(ResellerPlayersRetrieveRequest request);
    }
}
