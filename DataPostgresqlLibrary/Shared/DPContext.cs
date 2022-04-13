namespace DataPostgresqlLibrary
{
    using DataModelsLibrary;

    using Microsoft.EntityFrameworkCore;

    using SharedUtilities;

    // This is here so that when scaffolding we won't get errors
    public partial class DPContext : DbContext
    {
        private readonly IGuidFactory guidFactory;

        private readonly string connectionString;

        public DPContext(DbContextOptions options, IGuidFactory guidFactory) : base(options)
        {
            this.guidFactory = guidFactory;
        }

        // This constructor is simpler and more robust. Use it if LINQPad errors on the constructor above.
        // Note that connectionString is picked up in the OnConfiguring method below.
        public DPContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.connectionString == null)
            {
                base.OnConfiguring(optionsBuilder); // Normal operation
                return;
            }

            // We have a connection string
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(this.connectionString);
            base.OnConfiguring(dbContextOptionsBuilder);
        }
    }
}