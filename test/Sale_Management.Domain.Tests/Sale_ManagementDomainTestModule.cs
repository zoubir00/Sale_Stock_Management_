using Sale_Management.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Sale_Management;

[DependsOn(
    typeof(Sale_ManagementEntityFrameworkCoreTestModule)
    )]
public class Sale_ManagementDomainTestModule : AbpModule
{

}
