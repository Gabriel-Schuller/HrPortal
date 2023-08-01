using Volo.Abp.Application.Dtos;
using System;

namespace HrPortal.BonusSalaries
{
    public class GetBonusSalariesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? AmmountMin { get; set; }
        public int? AmmountMax { get; set; }
        public DateTime? AppliedDateMin { get; set; }
        public DateTime? AppliedDateMax { get; set; }

        public GetBonusSalariesInput()
        {

        }
    }
}