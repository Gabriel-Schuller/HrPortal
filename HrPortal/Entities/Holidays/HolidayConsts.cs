namespace HrPortal.Holidays
{
    public static class HolidayConsts
    {
        private const string DefaultSorting = "{0}DaysRemainedLastYear asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Holiday." : string.Empty);
        }

    }
}