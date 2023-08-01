using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HrPortal.Holidays
{
    public class Holiday : Entity<Guid>
    {
        public virtual int DaysRemainedLastYear { get; set; }

        public virtual int DaysUsedThisYear { get; set; }

        public virtual int DaysRemained { get; set; }

        public Holiday()
        {

        }

        public Holiday(Guid id, int daysRemainedLastYear, int daysUsedThisYear, int daysRemained)
        {

            Id = id;
            DaysRemainedLastYear = daysRemainedLastYear;
            DaysUsedThisYear = daysUsedThisYear;
            DaysRemained = daysRemained;
        }

    }
}