using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HrPortal.Data;

public class HrPortalDbContextFactory : IDesignTimeDbContextFactory<HrPortalDbContext>
{
    public HrPortalDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<HrPortalDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new HrPortalDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
