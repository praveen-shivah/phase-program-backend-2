namespace DataSeedingLibrary
{
    using ApplicationLifeCycle;

    using DatabaseContext;

    using UnitOfWorkTypesLibrary;

    public class DataSeedingSoftwareTypeAndVendorStartupItem : IRequestLifeCycleStartupItem
    {
        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        private readonly ISeedData seedData;

        public DataSeedingSoftwareTypeAndVendorStartupItem(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, ISeedData seedData)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.seedData = seedData;
        }

        RequestLifeCycleStartupItemPriority IRequestLifeCycleStartupItem.RequestLifeCycleStartupItemPriority => RequestLifeCycleStartupItemPriority.seedingData;

        async Task<bool> IRequestLifeCycleStartupItem.ExecuteAsync()
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
            await uow.ExecuteAsync();

            return true;
        }
    }
}