using System;
using Volo.Abp.Application.Dtos;

namespace HrPortal.BonusSalaries
{
    public class BonusSalaryDto : EntityDto<Guid>
    {
        public int Ammount { get; set; }
        public DateTime AppliedDate { get; set; }

    }
}