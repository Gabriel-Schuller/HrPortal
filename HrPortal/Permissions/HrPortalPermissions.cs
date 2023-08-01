namespace HrPortal.Permissions;

public static class HrPortalPermissions
{
    public const string GroupName = "HrPortal";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class BonusSalaries
    {
        public const string Default = GroupName + ".BonusSalaries";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Holidays
    {
        public const string Default = GroupName + ".Holidays";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Employees
    {
        public const string Default = GroupName + ".Employees";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}