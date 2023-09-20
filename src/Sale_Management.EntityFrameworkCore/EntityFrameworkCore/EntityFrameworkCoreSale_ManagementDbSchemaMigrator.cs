using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sale_Management.Data;
using Volo.Abp.DependencyInjection;

namespace Sale_Management.EntityFrameworkCore;

public class EntityFrameworkCoreSale_ManagementDbSchemaMigrator
    : ISale_ManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSale_ManagementDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the Sale_ManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Sale_ManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
