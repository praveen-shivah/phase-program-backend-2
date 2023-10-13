namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;
    
    using OpenQA.Selenium;
    using PlayersRepositoryTypes;
    using System;
    public class PlayersPageFactory:IResellerPlayerPageFactory
    {
        private readonly IPlayersRepository playersInformationRepository;


        public PlayersPageFactory(IPlayersRepository playersInformationRepository)
        {
            this.playersInformationRepository = playersInformationRepository;
        }
        IResellerPlayerPage IResellerPlayerPageFactory.Create(IWebDriver webDriver, SoftwareTypeEnum softwareType)
        {
            switch (softwareType)
            {
                case SoftwareTypeEnum.goldenDragon:
                    return new GoldenDragonPlayersPage(webDriver, playersInformationRepository);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
