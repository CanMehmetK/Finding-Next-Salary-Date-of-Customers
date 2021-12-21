dotnet restore

dotnet run

...
Finding Next Salary Date of Customers	<- :) Job Interview question was with this header ! Question was exactly same as below

Solution in [Test](https://github.com/CanMehmetK/Finding-Next-Salary-Date-of-Customers/blob/master/Employee.HolyDay.Accounting.Tests/PaymentDateCalculator.cs)

# Finding-Next-Salary-Date-of-Customers


Finding Next Salary Date of Customers						

To find the next salary date of customers you should create a class by implementing						
IPaymentDateCalculator interface and using the information in below.						


      public interface IPaymentDateCalculator
      {
       DateTime CalculateNextSalaryDate(SalaryDateCalculationDTO date);
       }						
       
     public class SalaryDateCalculationDTO
      {
        public int Day {get; set;} public int Week {get; set;}
        public SalaryFrequency PaymentFrequency {get; set;}
       }
       
       
       public enum SalaryFrequency
       {
          SpecificDayofMonth, // nth day of month. LastWorkingDayofMonth, // last working day of month
          DayBeforeLastWorkingDay, // day before last working day of month FirstWorkingdayofMonth, // first working day of month
          FirstXDay, // first x day of month. ie. if day is 2, it means first tuesday of month
          LastXDay, // last x day of month. ie. if day is 1, it means last monday of  month
          NthXDay, // nth x day of month. ie. if week is 2 and day is 4, it means second thursday of the month. Week property is used to specify which nth thursday day of month. Not nth week!
          NthWeeksXDay // x day of nth week of month. ie. if week is 1 and day is 3, it means wednesday of first week of the month. Week property is used to specify  the nth week of month.
       }		
       
Input and output						

SalaryFrequency	Day	Week	Current Date	Next Salary Date (Output)	Comment	

SpecificDayofMonth	12	0	8.07.2017	12.07.2017		

SpecificDayofMonth	14	0	20.07.2017	14.08.2017		

LastWorkingDayofMonth	0	0	8.06.2017	30.06.2017		

LastWorkingDayofMonth	0	0	20.09.2017	29.09.2017	

DayBeforeLastWorkingDay	0	0	8.06.2017	29.06.2017		

DayBeforeLastWorkingDay	0	0	20.09.2017	28.09.2017		

FirstWorkingdayofMonth	0	0	8.06.2017	3.07.2017	1.10.2017 is Saturday. So, first working day of month should be calculated as 3.07.2017	

FirstWorkingdayofMonth	0	0	1.10.2017	2.10.2017	1.10.2017 is Sunday. So, first working day of month should be calculated as 2.10.2017	

FirstWorkingdayofMonth	0	0	1.08.2017	1.09.2017		

FirstXDay	2	0	3.07.2017	4.07.2017		

FirstXDay	2	0	6.07.2017	1.08.2017		

FirstXDay	4	0	1.07.2017	6.07.2017		

LastXDay	3	0	14.07.2017	26.07.2017		

LastXDay	1	0	18.08.2017	28.08.2017		

LastXDay	5	0	21.09.2017	29.09.2017		

NthXDay	1	1	5.06.2017	3.07.2017		

NthXDay	3	3	8.07.2017	19.07.2017		

NthXDay	5	5	14.06.2017	30.06.2017		

NthWeeksXDay	1	1	10.08.2017	throw NoSuchDateException	First week of september of 2017 does not have Monday. So throw an exception here	

NthWeeksXDay	3	3	8.07.2017	12.07.2017		

NthWeeksXDay	5	5	14.06.2017	30.06.2017		


Problem 1 (Mandatory).						
     Create a class to calculate next salary dates		
     
Problem 2 (Mandatory).						
     Write unit tests to validate output values which are given in table		
     
Problem 3 (Bonus).						
    1.   Use the calculator class in Web Api application and expose an endpoint which calculates and						
the next salary date of customer by using same request and response type with the						
CalculateNextSalaryDate method.						

    2.   Install and Configure Swashbuckle.Core (Swagger) nuget library on your web api project.						
