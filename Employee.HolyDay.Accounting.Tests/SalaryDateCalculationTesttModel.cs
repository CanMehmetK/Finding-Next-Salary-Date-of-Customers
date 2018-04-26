using Employee.HolyDay.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.HolyDay.Accounting.Tests
{
    public class SalaryDateCalculation : SalaryDateCalculationDTO
    {
        public DateTime CurrentDate { get; set; }
        public DateTime? NextSalaryDate { get; set; }
    }

}
