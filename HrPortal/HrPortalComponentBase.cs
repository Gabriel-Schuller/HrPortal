using HrPortal.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HrPortal;

public abstract class HrPortalComponentBase : AbpComponentBase
{
    protected HrPortalComponentBase()
    {
        LocalizationResource = typeof(HrPortalResource);
    }
}
