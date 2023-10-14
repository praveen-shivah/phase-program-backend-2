namespace PlayersRepository
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    using PlayersRepositoryTypes;

    public class CompositeRoot:CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IPlayersRepository, PlayersRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<ICreatePlayer, CreatePlayerStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<ICreatePlayer, CreatePlayerCreate>(Lifestyle.Transient);

            return true;
        }
    }
}
