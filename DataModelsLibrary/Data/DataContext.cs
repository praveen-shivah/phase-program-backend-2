namespace DatabaseContext
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; } = null!;
        public virtual DbSet<City> City { get; set; } = null!;
        public virtual DbSet<Contact> Contact { get; set; } = null!;
        public virtual DbSet<Country> Country { get; set; } = null!;
        public virtual DbSet<ErrorLog> ErrorLog { get; set; } = null!;
        public virtual DbSet<Invoice> Invoice { get; set; } = null!;
        public virtual DbSet<InvoiceLineItem> InvoiceLineItem { get; set; } = null!;
        public virtual DbSet<InvoiceRevision> InvoiceRevision { get; set; } = null!;
        public virtual DbSet<Organization> Organization { get; set; } = null!;
        public virtual DbSet<PhoneNumber> PhoneNumber { get; set; } = null!;
        public virtual DbSet<PostalCode> PostalCode { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshToken { get; set; } = null!;
        public virtual DbSet<Reseller> Reseller { get; set; } = null!;
        public virtual DbSet<SignificantEvent> SignificantEvent { get; set; } = null!;
        public virtual DbSet<SignificantEventType> SignificantEventType { get; set; } = null!;
        public virtual DbSet<SiteInformation> SiteInformation { get; set; } = null!;
        public virtual DbSet<SoftwareType> SoftwareType { get; set; } = null!;
        public virtual DbSet<StateProvince> StateProvince { get; set; } = null!;
        public virtual DbSet<TransferPointsQueue> TransferPointsQueue { get; set; } = null!;
        public virtual DbSet<TransferPointsQueueType> TransferPointsQueueType { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<Vendor> Vendor { get; set; } = null!;
        public virtual DbSet<VendorCredentialsByOrganizations> VendorCredentialsByOrganizations { get; set; } = null!;
        public virtual DbSet<Players> Players { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(LOCALTIMESTAMP AT TIME ZONE 'UTC'::text)");
            });

            modelBuilder.Entity<SignificantEvent>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(LOCALTIMESTAMP AT TIME ZONE 'UTC'::text)");
            });


            modelBuilder.Entity<SiteInformation>(entity =>
            {
                entity.Property(e => e.LoginPassword).HasDefaultValueSql("''::character varying");

                entity.Property(e => e.LoginUsername).HasDefaultValueSql("''::character varying");
            });

            modelBuilder.Entity<TransferPointsQueue>(entity =>
            {
                entity.Property(e => e.ItemId).HasDefaultValueSql("''::text");

                entity.HasOne(d => d.TransferPointsQueueType)
                    .WithMany(p => p.TransferPointsQueue)
                    .HasForeignKey(d => d.TransferPointsQueueTypeId)
                    .HasConstraintName("FK_TransferPointsQueue_TransferPointsType_TransferPointsTypeId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
