using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace HrPortal.Data;

public class HrPortalEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public HrPortalEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the HrPortalDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HrPortalDbContext>()
            .Database
            .MigrateAsync();
    }
}
