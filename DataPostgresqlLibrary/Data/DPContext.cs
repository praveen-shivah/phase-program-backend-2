namespace DataPostgresqlLibrary
{
    using DataModelsLibrary;

    using Microsoft.EntityFrameworkCore;

    public class DPContext : DbContext
    {
        public DbSet<SiteInformation> SiteInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;database=postgres;user id=postgres;password=~!AmyLee~!0");
        }
    }
}