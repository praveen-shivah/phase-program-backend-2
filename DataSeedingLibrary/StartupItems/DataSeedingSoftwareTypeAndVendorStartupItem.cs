namespace DataSeedingLibrary
{
    using ApplicationLifeCycle;

    using DataPostgresqlLibrary;

    using UnitOfWorkTypesLibrary;

    public class DataSeedingSoftwareTypeAndVendorStartupItem : IRequestLifeCycleStartupItem
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        private readonly ISeedData seedData;

        public DataSeedingSoftwareTypeAndVendorStartupItem(IUnitOfWorkFactory<DPContext> unitOfWorkFactory, ISeedData seedData)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.seedData = seedData;
        }

        RequestLifeCycleStartupItemPriority IRequestLifeCycleStartupItem.RequestLifeCycleStartupItemPriority => RequestLifeCycleStartupItemPriority.seedingData;

        bool IRequestLifeCycleStartupItem.Execute()
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var response = await this.seedData.SeedAsync(context, new SeedDataRequest());

                        if (response.IsSuccessful)
                        {
                            return WorkItemResultEnum.commitSuccessfullyCompleted;
                        }

                        return WorkItemResultEnum.rollbackExit;
                    });
            uow.ExecuteAsync().Wait();

            return true;
        }
    }
}