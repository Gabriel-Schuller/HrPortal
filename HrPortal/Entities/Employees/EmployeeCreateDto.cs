using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HrPortal.Employees
{
    public class EmployeeCreateDto
    {
        public int TotalNumberOfDaysThisYear { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(EmployeeConsts.CNPMaxLength)]
        public string CNP { get; set; }
        public string? InformationsCI { get; set; }
        public string? Rezidence { get; set; }
        public string? SendingAddress { get; set; }
        [StringLength(EmployeeConsts.RelevancePhoneNumberMaxLength)]
        public string? RelevancePhoneNumber { get; set; }
        [StringLength(EmployeeConsts.PersonalPhoneNumberMaxLength)]
        public string? PersonalPhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime BirthDay { get; set; }
        public int StartingSalary { get; set; }
        public bool PaysProgrammerTaxes { get; set; } = false;
    }
}