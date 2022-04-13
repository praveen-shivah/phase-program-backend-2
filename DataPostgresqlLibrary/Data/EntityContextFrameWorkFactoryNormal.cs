namespace DataPostgresqlLibrary
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using UnitOfWorkTypesLibrary;

    public class EntityContextFrameWorkFactoryNormal : IEntityContextFrameWorkFactory<DPContext>
    {
        private readonly IConnectionFactory connectionFactory;

        private readonly IConfiguration configuration;

        public EntityContextFrameWorkFactoryNormal(IConnectionFactory connectionFactory, IConfiguration configuration)
        {
            this.connectionFactory = connectionFactory;
            this.configuration = configuration;
        }

        DPContext IEntityContextFrameWorkFactory<DPContext>.CreateContext(string dbName)
        {
            var builder = new DbContextOptionsBuilder<DPContext>();
            var dbConnection = this.connectionFactory.Create();
            builder.UseNpgsql(dbConnection);
            var options = builder.Options;

            return new DPContext(options, this.configuration);
        }

        public void Dispose()
        {
        }
    }
}
