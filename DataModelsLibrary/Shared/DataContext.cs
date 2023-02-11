namespace DatabaseContext
{
    using CommonServices;

    using Microsoft.EntityFrameworkCore;

    public partial class DataContext : DbContext
    {
        private readonly IDateTimeService dateTimeService;

        private readonly string? connectionString;

        public DataContext(DbContextOptions<DataContext> options, IDateTimeService dateTimeService)
            : base(options)
        {
            this.dateTimeService = dateTimeService;
        }

        // This constructor is simpler and more robust. Use it if LINQPad errors on the constructor above.
        // Note that connectionString is picked up in the OnConfiguring method below.
        public DataContext(string? connectionString, IDateTimeService dateTimeService)
        {
            this.connectionString = connectionString;
            this.dateTimeService = dateTimeService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.saveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void saveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var added = this.ChangeTracker.Entries().Where(t => t.State == EntityState.Added).Select(t => t.Entity).ToArray();

            foreach (var entity in added)
            {
                if (entity is BaseEntity track)
                {
                    var createdOn = this.dateTimeService.UtcNow;
                    track.SetCreatedOn(createdOn);
                    track.SetLastModified(createdOn);
                }
            }

            var modified = this.ChangeTracker.Entries().Where(t => t.State == EntityState.Modified).Select(t => t.Entity).ToArray();

            foreach (var entity in modified)
            {
                if (entity is BaseEntity track)
                {
                    track.SetLastModified(this.dateTimeService.UtcNow);
                }
            }
        }

        public override int SaveChanges()
        {
            this.saveChanges();

            return base.SaveChanges();
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