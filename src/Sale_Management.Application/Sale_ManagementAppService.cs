using System;
using System.Collections.Generic;
using System.Text;
using Sale_Management.Localization;
using Volo.Abp.Application.Services;

namespace Sale_Management;

/* Inherit your application services from this class.
 */
public abstract class Sale_ManagementAppService : ApplicationService
{
    protected Sale_ManagementAppService()
    {
        LocalizationResource = typeof(Sale_ManagementResource);
    }
}
