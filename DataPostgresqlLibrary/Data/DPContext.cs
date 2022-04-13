namespace DataPostgresqlLibrary
{
    using DataModelsLibrary;

    using Microsoft.EntityFrameworkCore;

    public partial class DPContext : DbContext
    {
        public DPContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<SignificantEvent> SignificantEvent { get; set; }
        public DbSet<SiteInformation> SiteInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;database=postgres2;user id=postgres;password=~!AmyLee~!0");
        }
    }
}