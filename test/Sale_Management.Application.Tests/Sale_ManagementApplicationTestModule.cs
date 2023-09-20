using Volo.Abp.Modularity;

namespace Sale_Management;

[DependsOn(
    typeof(Sale_ManagementApplicationModule),
    typeof(Sale_ManagementDomainTestModule)
    )]
public class Sale_ManagementApplicationTestModule : AbpModule
{

}
