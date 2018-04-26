using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.HolyDay.Core.Extensions
{
    public static class DateTimeExtensions
    {
        #region Common
        public static DayOfWeek ToDayOfWeek(this int Day)
        {
            return Enum.Parse<DayOfWeek>(Day.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public static DateTime FirstDayofMonth(this DateTime CurrentDate)
        {
            return new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
        }
        private static DateTime _FirstWorkingDayofMonth(this DateTime CurrentDate)
        {
            var FirstWorkingDayofMonthResult = CurrentDate.FirstDayofMonth();
            // Must Not be Saturday
            if (FirstWorkingDayofMonthResult.FirstDayofMonth().DayOfWeek == DayOfWeek.Saturday)
                FirstWorkingDayofMonthResult = FirstWorkingDayofMonthResult.AddDays(2);
            // Must Not be Sunday
            else if (FirstWorkingDayofMonthResult.DayOfWeek == DayOfWeek.Sunday)
                FirstWorkingDayofMonthResult = FirstWorkingDayofMonthResult.AddDays(1);
            return FirstWorkingDayofMonthResult;
        }

        private static DateTime _LastWorkingDayofMonth(this DateTime CurrentDate)
        {
            var LastWorkingDayofMonthResult = CurrentDate.LastDayofMonth();
            // Must Not be Sunday
            if (LastWorkingDayofMonthResult.LastDayofMonth().DayOfWeek == DayOfWeek.Sunday)
                LastWorkingDayofMonthResult = LastWorkingDayofMonthResult.AddDays(-2);
            // Must Not be Saturday
            else if (LastWorkingDayofMonthResult.DayOfWeek == DayOfWeek.Saturday)
                LastWorkingDayofMonthResult = LastWorkingDayofMonthResult.AddDays(-1);

            return LastWorkingDayofMonthResult;

        } 
      


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public static DateTime LastDayofMonth(this DateTime CurrentDate)
        {
            return new DateTime(CurrentDate.Year, CurrentDate.Month, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month));
        }
        #endregion


        /// <summary>
        ///  LastWorkingDayofMonth, // last working day of month
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public static DateTime LastWorkingDayofMonth(this DateTime CurrentDate)
        {
            var LastWorkingDayofMonthResult = CurrentDate._LastWorkingDayofMonth();            

            // What if Current date later then CurrentDate
            if (LastWorkingDayofMonthResult < CurrentDate) LastWorkingDayofMonthResult = CurrentDate.LastDayofMonth().AddDays(1)._LastWorkingDayofMonth();

            return LastWorkingDayofMonthResult;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public static DateTime DayBeforeLastWorkingDay(this DateTime CurrentDate)
        {
            var DayBeforeLastWorkingDayResult = CurrentDate.LastWorkingDayofMonth().AddDays(-1);
            // Must Not be Sunday
            if (DayBeforeLastWorkingDayResult.LastDayofMonth().DayOfWeek == DayOfWeek.Sunday)
                DayBeforeLastWorkingDayResult = DayBeforeLastWorkingDayResult.AddDays(-2);
            // Must Not be Saturday
            else if (DayBeforeLastWorkingDayResult.DayOfWeek == DayOfWeek.Saturday)
                DayBeforeLastWorkingDayResult = DayBeforeLastWorkingDayResult.AddDays(-1);

            // What if Current date later then CurrentDate
            if (DayBeforeLastWorkingDayResult < CurrentDate) DayBeforeLastWorkingDayResult = CurrentDate.LastDayofMonth().AddDays(1).DayBeforeLastWorkingDay();

            return DayBeforeLastWorkingDayResult;

        }

        /// <summary>
        /// FirstWorkingdayofMonth, // first working day of month
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public static DateTime FirstWorkingDayofMonth(this DateTime CurrentDate)
        {
            var FirstWorkingDayofMonthResult = CurrentDate._FirstWorkingDayofMonth();
           
            // What if later or equal then CurrentDate
            if (FirstWorkingDayofMonthResult <= CurrentDate) FirstWorkingDayofMonthResult = CurrentDate.LastDayofMonth().AddDays(1)._FirstWorkingDayofMonth();

            return FirstWorkingDayofMonthResult;

        }

        /// <summary>
        /// FirstXDay, // first x day of month. ie. if day is 2, it means first tuesday of month
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <param name="DayofWeek"></param>
        /// <returns></returns>
        public static DateTime FirstXDayofMonth(this DateTime CurrentDate, int DayofWeek)
        {
            var FirstXDayofMonthResult = CurrentDate.FirstDayofMonth();

            // Include Current Day 
            if (FirstXDayofMonthResult.DayOfWeek == DayofWeek.ToDayOfWeek())
            {
                // Happy Hour
            }
            else
            {
                while (FirstXDayofMonthResult.Day < CurrentDate.LastDayofMonth().Day)
                {
                    FirstXDayofMonthResult.AddDays(1);
                    if (FirstXDayofMonthResult.DayOfWeek == DayofWeek.ToDayOfWeek())
                        break;
                }
            }

            // What if  later then CurrentDate
            if (FirstXDayofMonthResult > CurrentDate) FirstXDayofMonthResult = CurrentDate.LastDayofMonth().AddDays(1).FirstXDayofMonth(DayofWeek);

            return FirstXDayofMonthResult;

        }

        /// <summary>
        /// LastXDay, // last x day of month. ie. if day is 1, it means last monday of month
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <param name="DayofWeek"></param>
        /// <returns></returns>
        public static DateTime LastXDayofMonth(this DateTime CurrentDate, int DayofWeek)
        {
            var LastXDayofMonthResult = CurrentDate.LastDayofMonth();
            // Include Current Day 
            if (LastXDayofMonthResult.DayOfWeek == DayofWeek.ToDayOfWeek())
            {
                // Happy Hour
            }
            else
            {
                while (LastXDayofMonthResult.Day > CurrentDate.Day)
                {
                    LastXDayofMonthResult.AddDays(-1);
                    if (LastXDayofMonthResult.DayOfWeek == DayofWeek.ToDayOfWeek())
                        break;
                }
            }

            // What if  earlier then CurrentDate
            if (LastXDayofMonthResult > CurrentDate) LastXDayofMonthResult = CurrentDate.LastDayofMonth().AddDays(1).LastXDayofMonth(DayofWeek);

            return LastXDayofMonthResult;
        }

        /// <summary>
        ///  NthXDay, // nth x day of month. ie. if week is 2 and day is 4, it means 
        ///  second thursday of the month.Week property is used to specify which nth thursday
        ///  day of month.Not nth week!
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <param name="Week"></param>
        /// <param name="DayofWeek"></param>
        /// <returns></returns>
        public static DateTime NthXDayofMonth(this DateTime CurrentDate, int Week, int DayofWeek)
        {
            return CurrentDate.FirstXDayofMonth(DayofWeek).AddDays(7 * Week);
        }





        /// <summary>
        /// NthWeeksXDay // x day of nth week of month. ie. if week is 1 and day is 3,
        /// it means wednesday of first week of the month.Week property is used to specify
        /// the nth week of month.
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <param name="Week"></param>
        /// <param name="DayofWeek"></param>
        /// <returns></returns>
        public static DateTime NthWeeksXDayofMonth(this DateTime CurrentDate, int Week, int DayofWeek)
        {
            var NthWeeksXDayofMonthResult = CurrentDate.FirstDayofMonth();
            int _week = 0;
            while (NthWeeksXDayofMonthResult.Day < CurrentDate.LastDayofMonth().Day || (_week == Week & NthWeeksXDayofMonthResult.DayOfWeek == DayofWeek.ToDayOfWeek()))
            {
                if (NthWeeksXDayofMonthResult.DayOfWeek == DayOfWeek.Sunday)
                    _week++;

            }

            // What if  later then CurrentDate
            if (NthWeeksXDayofMonthResult > CurrentDate) NthWeeksXDayofMonthResult = CurrentDate.LastDayofMonth().AddDays(1).NthWeeksXDayofMonth(Week, DayofWeek);

            return NthWeeksXDayofMonthResult;

        }
    }
}
