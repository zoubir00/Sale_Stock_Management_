using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Sale_Management;

[Dependency(ReplaceServices = true)]
public class Sale_ManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Sale_Management";
}
