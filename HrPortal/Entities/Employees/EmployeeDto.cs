using System;
using Volo.Abp.Application.Dtos;

namespace HrPortal.Employees
{
    public class EmployeeDto : AuditedEntityDto<Guid>
    {
        public int TotalNumberOfDaysThisYear { get; set; }
        public string Name { get; set; }
        public string CNP { get; set; }
        public string? InformationsCI { get; set; }
        public string? Rezidence { get; set; }
        public string? SendingAddress { get; set; }
        public string? RelevancePhoneNumber { get; set; }
        public string? PersonalPhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime BirthDay { get; set; }
        public int StartingSalary { get; set; }
        public bool PaysProgrammerTaxes { get; set; }

    }
}