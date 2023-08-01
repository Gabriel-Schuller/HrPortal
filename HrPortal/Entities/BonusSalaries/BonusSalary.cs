using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HrPortal.BonusSalaries
{
    public class BonusSalary : Entity<Guid>
    {
        public virtual int Ammount { get; set; }

        public virtual DateTime AppliedDate { get; set; }

        public BonusSalary()
        {

        }

        public BonusSalary(Guid id, int ammount, DateTime appliedDate)
        {

            Id = id;
            Ammount = ammount;
            AppliedDate = appliedDate;
        }

    }
}