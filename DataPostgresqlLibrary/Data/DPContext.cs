namespace DataPostgresqlLibrary
{
    using DataModelsLibrary;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using Npgsql;

    // This is here so that when scaffolding we won't get errors
    public class DPContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DPContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        public DPContext()
        {
        }

        public DbSet<ErrorLog> ErrorLog { get; set; }

        public DbSet<SignificantEvent> SignificantEvent { get; set; }

        public DbSet<SiteInformation> SiteInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = this.configuration.GetConnectionString("MobileOMatic") ?? "host=localhost;database=postgres2;user id=postgres;password=~!AmyLee~!0";
            var sqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                MaxPoolSize = 10000,
                MinPoolSize = 200
            };

            // We have a connection string
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(sqlConnectionStringBuilder.ConnectionString);
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>(
                entity =>
                    {
                        entity.HasIndex(
                            e => new
                            {
                                e.Hash,
                                e.CreatedOn
                            }).HasDatabaseName("IX_ErrorLog_Hash");

                        entity.Property(e => e.CreatedOn).HasDefaultValueSql("LOCALTIMESTAMP AT TIME ZONE 'UTC'");

                        entity.Property(e => e.Hash).IsUnicode(false);
                    });

            modelBuilder.Entity<SignificantEvent>(
                entity =>
                    {
                        entity.HasIndex(
                            e => new
                            {
                                e.Id,
                                e.ShortDescription,
                                e.CreatedBy,
                                e.EventTypeId,
                                e.CreatedOn
                            }).HasDatabaseName("IX_SignificantEvent_EventId_CreatedOn");

                        entity.Property(e => e.CreatedOn).HasDefaultValueSql("LOCALTIMESTAMP AT TIME ZONE 'UTC'");
                    });

            modelBuilder.Entity<SignificantEventType>(entity => { entity.Property(e => e.Id).ValueGeneratedNever(); });

            base.OnModelCreating(modelBuilder);
        }
    }
}