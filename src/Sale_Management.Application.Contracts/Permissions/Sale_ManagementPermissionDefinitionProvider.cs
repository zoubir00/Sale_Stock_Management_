using Sale_Management.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Sale_Management.Permissions;

public class Sale_ManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Sale_ManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(Sale_ManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Sale_ManagementResource>(name);
    }
}
