using Sale_Management.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Sale_Management.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Sale_ManagementController : AbpControllerBase
{
    protected Sale_ManagementController()
    {
        LocalizationResource = typeof(Sale_ManagementResource);
    }
}
