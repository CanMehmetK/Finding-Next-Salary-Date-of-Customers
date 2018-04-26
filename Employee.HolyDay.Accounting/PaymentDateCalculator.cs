/********************************

    Problem 1 (Mandatory).
        Create a class to calculate next salary dates
    Problem 2 (Mandatory).
        Write unit tests to validate output values which are given in table
    Problem 3 (Bonus).
        1. Use the calculator class in Web Api application and expose an endpoint which calculates and
           the next salary date of customer by using same request and response type with the
           CalculateNextSalaryDate method.
        2. Install and Configure Swashbuckle.Core (Swagger) nuget library on your web api project.

 */

namespace Employee.HolyDay.Accounting
{
    using Employee.HolyDay.Core;
    using Employee.HolyDay.Core.Extensions;
    using System;

    /// <summary>
    /// Problem 1 (Mandatory).
    /// Create a class to calculate next salary dates
    /// </summary>
    public class PaymentDateCalculator : IPaymentDateCalculator
    {
        /// <summary>
        /// Includes Current Day
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public DateTime? Calculate(SalaryDateCalculationDTO parameter, DateTime CurrentDate)
        {
            DateTime? result = null;
            switch (parameter.PaymentFrequency)
            {
                case SalaryFrequency.SpecificDayofMonth:
                    //Include Current Day
                    if (CurrentDate.Day == parameter.Day) result = CurrentDate;
                    while (CurrentDate < CurrentDate.AddMonths(1))
                    {
                        CurrentDate = CurrentDate.AddDays(1);
                        if (CurrentDate.Day == parameter.Day) { result = CurrentDate; break; }
                    }
                    break;
                case SalaryFrequency.LastWorkingDayofMonth:
                    result = CurrentDate.LastWorkingDayofMonth();
                    break;
                case SalaryFrequency.DayBeforeLastWorkingDay:
                    result = CurrentDate.DayBeforeLastWorkingDay();
                    break;
                case SalaryFrequency.FirstWorkingdayofMonth:
                    result = CurrentDate.FirstWorkingDayofMonth();
                    break;
                case SalaryFrequency.FirstXDay:
                    return CurrentDate.FirstXDayofMonth(parameter.Day);
                    break;
                case SalaryFrequency.LastXDay:
                    return CurrentDate.LastXDayofMonth(parameter.Day);
                    break;
                case SalaryFrequency.NthXDay:
                    return CurrentDate.NthXDayofMonth(parameter.Week, parameter.Day);
                    break;
                case SalaryFrequency.NthWeeksXDay:
                    return CurrentDate.NthWeeksXDayofMonth(parameter.Week, parameter.Day);
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public DateTime? Calculate(SalaryDateCalculationDTO parameter)
        {
            return Calculate(parameter, DateTime.Now);
        }


    }
}

