using Sale_Management.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Sale_Management.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(Sale_ManagementEntityFrameworkCoreModule),
    typeof(Sale_ManagementApplicationContractsModule)
    )]
public class Sale_ManagementDbMigratorModule : AbpModule
{
}
