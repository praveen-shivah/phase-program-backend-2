namespace DataPostgresqlLibrary
{
    using CommonServices;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using UnitOfWorkTypesLibrary;

    public class EntityContextFrameWorkFactoryNormal : IEntityContextFrameWorkFactory<DPContext>
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

        DPContext IEntityContextFrameWorkFactory<DPContext>.CreateContext(string dbName)
        {
            var builder = new DbContextOptionsBuilder<DPContext>();
            var dbConnection = this.connectionFactory.Create();
            builder.UseNpgsql(dbConnection);
            var options = builder.Options;

            return new DPContext(options, this.configuration, this.dateTimeService);
        }

        public void Dispose()
        {
        }
    }
}
