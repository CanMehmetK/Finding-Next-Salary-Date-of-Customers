using System;
using System.ComponentModel.DataAnnotations;

namespace Employee.HolyDay.Core
{

    public class SalaryDateCalculationDTO
    {
        public int Day { get; set; }
        public int Week { get; set; }

        public SalaryFrequency PaymentFrequency { get; set; }

    }
}