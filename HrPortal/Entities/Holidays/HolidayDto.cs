using System;
using Volo.Abp.Application.Dtos;

namespace HrPortal.Holidays
{
    public class HolidayDto : EntityDto<Guid>
    {
        public int DaysRemainedLastYear { get; set; }
        public int DaysUsedThisYear { get; set; }
        public int DaysRemained { get; set; }

    }
}