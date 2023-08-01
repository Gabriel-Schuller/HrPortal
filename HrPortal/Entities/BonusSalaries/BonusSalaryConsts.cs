namespace HrPortal.BonusSalaries
{
    public static class BonusSalaryConsts
    {
        private const string DefaultSorting = "{0}Ammount asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "BonusSalary." : string.Empty);
        }

    }
}