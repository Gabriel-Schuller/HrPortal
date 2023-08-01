using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HrPortal.Holidays
{
    public class HolidayUpdateDto
    {
        public int DaysRemainedLastYear { get; set; }
        public int DaysUsedThisYear { get; set; }
        public int DaysRemained { get; set; }

    }
}