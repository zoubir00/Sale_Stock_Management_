using System.Threading.Tasks;

namespace Sale_Management.Data;

public interface ISale_ManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
