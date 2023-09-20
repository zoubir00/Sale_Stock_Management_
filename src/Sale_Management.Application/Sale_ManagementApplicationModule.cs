using Microsoft.Extensions.DependencyInjection;
using Sale_Management.BaseService;
using Sale_Management.IBaseServices;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Sale_Management;

[DependsOn(
    typeof(Sale_ManagementDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(Sale_ManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class Sale_ManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<Sale_ManagementApplicationModule>();
        });
    }
}
