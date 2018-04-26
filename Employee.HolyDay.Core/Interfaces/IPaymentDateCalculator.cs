using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.HolyDay.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPaymentDateCalculator
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        DateTime? Calculate(SalaryDateCalculationDTO parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        DateTime? Calculate(SalaryDateCalculationDTO parameter, DateTime CurrentDate);
    }
}
