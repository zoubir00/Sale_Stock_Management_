using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Sale_Management.Data;

/* This is used if database provider does't define
 * ISale_ManagementDbSchemaMigrator implementation.
 */
public class NullSale_ManagementDbSchemaMigrator : ISale_ManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
