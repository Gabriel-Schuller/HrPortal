using Volo.Abp.Application.Dtos;
using System;

namespace HrPortal.Holidays
{
    public class GetHolidaysInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? DaysRemainedLastYearMin { get; set; }
        public int? DaysRemainedLastYearMax { get; set; }
        public int? DaysUsedThisYearMin { get; set; }
        public int? DaysUsedThisYearMax { get; set; }
        public int? DaysRemainedMin { get; set; }
        public int? DaysRemainedMax { get; set; }

        public GetHolidaysInput()
        {

        }
    }
}