using HrPortal.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HrPortal.Permissions;

public class HrPortalPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HrPortalPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HrPortalPermissions.MyPermission1, L("Permission:MyPermission1"));

        var bonusSalaryPermission = myGroup.AddPermission(HrPortalPermissions.BonusSalaries.Default, L("Permission:BonusSalaries"));
        bonusSalaryPermission.AddChild(HrPortalPermissions.BonusSalaries.Create, L("Permission:Create"));
        bonusSalaryPermission.AddChild(HrPortalPermissions.BonusSalaries.Edit, L("Permission:Edit"));
        bonusSalaryPermission.AddChild(HrPortalPermissions.BonusSalaries.Delete, L("Permission:Delete"));

        var holidayPermission = myGroup.AddPermission(HrPortalPermissions.Holidays.Default, L("Permission:Holidays"));
        holidayPermission.AddChild(HrPortalPermissions.Holidays.Create, L("Permission:Create"));
        holidayPermission.AddChild(HrPortalPermissions.Holidays.Edit, L("Permission:Edit"));
        holidayPermission.AddChild(HrPortalPermissions.Holidays.Delete, L("Permission:Delete"));

        var employeePermission = myGroup.AddPermission(HrPortalPermissions.Employees.Default, L("Permission:Employees"));
        employeePermission.AddChild(HrPortalPermissions.Employees.Create, L("Permission:Create"));
        employeePermission.AddChild(HrPortalPermissions.Employees.Edit, L("Permission:Edit"));
        employeePermission.AddChild(HrPortalPermissions.Employees.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HrPortalResource>(name);
    }
}