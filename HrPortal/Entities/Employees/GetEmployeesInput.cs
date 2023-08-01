using Volo.Abp.Application.Dtos;
using System;

namespace HrPortal.Employees
{
    public class GetEmployeesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? TotalNumberOfDaysThisYearMin { get; set; }
        public int? TotalNumberOfDaysThisYearMax { get; set; }
        public string? Name { get; set; }
        public string? CNP { get; set; }
        public string? InformationsCI { get; set; }
        public string? Rezidence { get; set; }
        public string? SendingAddress { get; set; }
        public string? RelevancePhoneNumber { get; set; }
        public string? PersonalPhoneNumber { get; set; }
        public DateTime? HiringDateMin { get; set; }
        public DateTime? HiringDateMax { get; set; }
        public DateTime? BirthDayMin { get; set; }
        public DateTime? BirthDayMax { get; set; }
        public int? StartingSalaryMin { get; set; }
        public int? StartingSalaryMax { get; set; }
        public bool? PaysProgrammerTaxes { get; set; }

        public GetEmployeesInput()
        {

        }
    }
}