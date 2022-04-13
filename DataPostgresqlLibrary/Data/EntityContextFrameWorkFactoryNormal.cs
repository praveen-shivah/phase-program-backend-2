namespace DataPostgresqlLibrary
{
    using Microsoft.EntityFrameworkCore;

    using SharedUtilities;

    using UnitOfWorkTypesLibrary;

    public class EntityContextFrameWorkFactoryNormal : IEntityContextFrameWorkFactory<DPContext>
    {
        private readonly IConnectionFactory connectionFactory;

        public EntityContextFrameWorkFactoryNormal(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        DPContext IEntityContextFrameWorkFactory<DPContext>.CreateContext(string dbName)
        {
            var builder = new DbContextOptionsBuilder<DPContext>();
            var dbConnection = this.connectionFactory.Create();
            builder.UseNpgsql(dbConnection);
            var options = builder.Options;

            return new DPContext(options, null);
        }

        public void Dispose()
        {
        }
    }
}
