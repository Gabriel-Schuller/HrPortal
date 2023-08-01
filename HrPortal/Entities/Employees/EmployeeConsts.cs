namespace HrPortal.Employees
{
    public static class EmployeeConsts
    {
        private const string DefaultSorting = "{0}TotalNumberOfDaysThisYear asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Employee." : string.Empty);
        }

        public const int CNPMaxLength = 13;
        public const int RelevancePhoneNumberMaxLength = 12;
        public const int PersonalPhoneNumberMaxLength = 12;
    }
}