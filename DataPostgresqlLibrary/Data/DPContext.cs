﻿namespace DataPostgresqlLibrary
{
    using CommonServices;

    using DataModelsLibrary;

    using DataSharedLibrary;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using Npgsql;

    // This is here so that when scaffolding we won't get errors
    public class DPContext : DbContext
    {
        private readonly IConfiguration configuration;

        private readonly IDateTimeService dateTimeService;

        public DPContext(
            DbContextOptions options,
            IConfiguration configuration,
            IDateTimeService dateTimeService)
            : base(options)
        {
            this.configuration = configuration;
            this.dateTimeService = dateTimeService;
        }

        public DPContext()
        {
        }

        public DbSet<InvoiceRevision> InvoiceRevision { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItem { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<SignificantEvent> SignificantEvent { get; set; }
        public DbSet<SiteInformation> SiteInformation { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Vendor> Vendor { get; set; }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var added = this.ChangeTracker.Entries().Where(t => t.State == EntityState.Added).Select(t => t.Entity).ToArray();

            foreach (var entity in added)
            {
                if (entity is BaseEntity track)
                {
                    track.CreatedOn = this.dateTimeService.UtcNow;
                    track.ModifiedOn = this.dateTimeService.UtcNow;
                }
            }

            var modified = this.ChangeTracker.Entries().Where(t => t.State == EntityState.Modified).Select(t => t.Entity).ToArray();

            foreach (var entity in modified)
            {
                if (entity is BaseEntity track)
                {
                    track.ModifiedOn = this.dateTimeService.UtcNow;
                }
            }

            return base.SaveChanges();
        }

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