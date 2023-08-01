using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HrPortal.BonusSalaries
{
    public class BonusSalaryCreateDto
    {
        public int Ammount { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}