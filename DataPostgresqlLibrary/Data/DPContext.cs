namespace DataPostgresqlLibrary
{
    using Microsoft.EntityFrameworkCore;

    public class DPContext : DbContext
    {
        public DPContext(DbContextOptions<DPContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;database=postgres;user id=postgres;password=~!AmyLee~!0");
        }
    }
}
