namespace DataPostgresqlLibrary
{
    using DataModelsLibrary;

    using Microsoft.EntityFrameworkCore;

    public partial class DPContext : DbContext
    {
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<SignificantEvent> SignificantEvent { get; set; }
        public DbSet<SiteInformation> SiteInformation { get; set; }
    }
}