using DataPostgresqlLibrary;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory :
    IDesignTimeDbContextFactory<DPContext>
{
    public DPContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<DPContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseNpgsql(connectionString);
        return new DPContext(builder.Options, configuration);
    }
}