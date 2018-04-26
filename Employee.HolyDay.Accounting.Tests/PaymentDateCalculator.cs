using Employee.HolyDay.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace Employee.HolyDay.Accounting.Tests
{
    public class PaymentDateCalculatorTest
    {        

        public PaymentDateCalculatorTest()
        {

        }

        public static IEnumerable<object[]> TestData()
        {
            //SalaryFrequency	Day	Week	Current Date Next Salary Date (Output) Comment	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.SpecificDayofMonth, Day = 12, Week = 0, CurrentDate = new DateTime(2017, 07, 08), NextSalaryDate = new DateTime(2017, 07, 12) } }; //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.SpecificDayofMonth, Day = 14, Week = 0, CurrentDate = new DateTime(2017, 07, 20), NextSalaryDate = new DateTime(2017, 08, 14) } }; //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.LastWorkingDayofMonth, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 06, 08), NextSalaryDate = new DateTime(2017, 06, 30) } };   //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.LastWorkingDayofMonth, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 09, 20), NextSalaryDate = new DateTime(2017, 09, 29) } };   //
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.DayBeforeLastWorkingDay, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 06, 08), NextSalaryDate = new DateTime(2017, 06, 29) } }; //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.DayBeforeLastWorkingDay, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 09, 20), NextSalaryDate = new DateTime(2017, 09, 28) } }; //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.FirstWorkingdayofMonth, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 06, 08), NextSalaryDate = new DateTime(2017, 07, 03) } };  //	1.10.2017 is Saturday. So, first working day of month should be calculated as 3.07.2017
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.FirstWorkingdayofMonth, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 10, 01), NextSalaryDate = new DateTime(2017, 10, 02) } };  //	1.10.2017 is Sunday. So, first working day of month should be calculated as 2.10.2017
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.FirstWorkingdayofMonth, Day = 0, Week = 0, CurrentDate = new DateTime(2017, 08, 01), NextSalaryDate = new DateTime(2017, 09, 01) } };  //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.FirstXDay, Day = 2, Week = 0, CurrentDate = new DateTime(2017, 07, 03), NextSalaryDate = new DateTime(2017, 07, 04) } };   //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.FirstXDay, Day = 2, Week = 0, CurrentDate = new DateTime(2017, 07, 06), NextSalaryDate = new DateTime(2017, 08, 01) } };   //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.FirstXDay, Day = 4, Week = 0, CurrentDate = new DateTime(2017, 07, 01), NextSalaryDate = new DateTime(2017, 07, 06) } };   //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.LastXDay, Day = 3, Week = 0, CurrentDate = new DateTime(2017, 07, 14), NextSalaryDate = new DateTime(2017, 07, 26) } };    //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.LastXDay, Day = 1, Week = 0, CurrentDate = new DateTime(2017, 08, 18), NextSalaryDate = new DateTime(2017, 08, 28) } };    //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.LastXDay, Day = 5, Week = 0, CurrentDate = new DateTime(2017, 09, 21), NextSalaryDate = new DateTime(2017, 09, 29) } };    //	 
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.NthXDay, Day = 1, Week = 1, CurrentDate = new DateTime(2017, 06, 05), NextSalaryDate = new DateTime(2017, 07, 03) } }; //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.NthXDay, Day = 3, Week = 3, CurrentDate = new DateTime(2017, 07, 08), NextSalaryDate = new DateTime(2017, 07, 19) } }; //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.NthXDay, Day = 5, Week = 5, CurrentDate = new DateTime(2017, 06, 14), NextSalaryDate = new DateTime(2017, 06, 30) } }; //	 
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.NthWeeksXDay, Day = 1, Week = 1, CurrentDate = new DateTime(2017, 08, 10), NextSalaryDate = null } };   //	First week of september of 2017 does not have Monday. So throw an exception here
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.NthWeeksXDay, Day = 3, Week = 3, CurrentDate = new DateTime(2017, 07, 08), NextSalaryDate = new DateTime(2017, 07, 12) } };    //	
            yield return new object[] { new SalaryDateCalculation() { PaymentFrequency = SalaryFrequency.NthWeeksXDay, Day = 5, Week = 5, CurrentDate = new DateTime(2017, 06, 14), NextSalaryDate = new DateTime(2017, 06, 30) } };	//	 

        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Calculate(SalaryDateCalculation parameter)
        {
            var _paymentDateCalculator = new PaymentDateCalculator();
            var actual = _paymentDateCalculator.Calculate(parameter, parameter.CurrentDate);
            var expected = parameter.NextSalaryDate;
            Assert.Equal(expected, actual);

        }
    }
}
