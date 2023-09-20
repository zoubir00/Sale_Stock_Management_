using Volo.Abp.Settings;

namespace Sale_Management.Settings;

public class Sale_ManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(Sale_ManagementSettings.MySetting1));
    }
}
