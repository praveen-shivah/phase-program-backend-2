namespace DatabaseContext
{
    using CommonServices;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using UnitOfWorkTypesLibrary;

    public class EntityContextFrameWorkFactoryNormal : IEntityContextFrameWorkFactory<DataContext>
    {
        private readonly IConnectionFactory connectionFactory;

        private readonly IConfiguration configuration;

        private readonly IDateTimeService dateTimeService;

        public EntityContextFrameWorkFactoryNormal(IConnectionFactory connectionFactory, IConfiguration configuration, IDateTimeService dateTimeService)
        {
            this.connectionFactory = connectionFactory;
            this.configuration = configuration;
            this.dateTimeService = dateTimeService;
        }

        DataContext IEntityContextFrameWorkFactory<DataContext>.CreateContext(string dbName)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            var dbConnection = this.connectionFactory.Create();
            builder.UseNpgsql(dbConnection);
            var options = builder.Options;

            return new DataContext(options, this.dateTimeService);
        }

        public void Dispose()
        {
        }
    }
}
