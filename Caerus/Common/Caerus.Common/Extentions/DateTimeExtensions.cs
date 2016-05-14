using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;

namespace Caerus.Common.Extentions
{
   public static class DateTimeExtensions
   {
       public static string ConvertToBrNetDate(this DateTime date)
       {
           return date.ToString("yyyy-MM-dd");
       }

       /// <summary>
       /// Convert a date to the end of the day (i.e. 2014-08-25 00:00 will be converted to 2014-08-25 23:59)
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       public static DateTime EndOfDay(this DateTime date)
       {
           return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
       }


       public static Int32 GetAge(this DateTime dateOfBirth)
       {
           var today = DateTime.Today;

           var a = (today.Year * 100 + today.Month) * 100 + today.Day;
           var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

           return (a - b) / 10000;
       }

       public static int MonthDifference(this DateTime lValue, DateTime rValue)
       {
           return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
       }

       public static DateTime StartOfThisMonth(this DateTime date)
       {
           return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
       }

       public static DateTime EndOfThisMonth(this DateTime date)
       {
           int year = date.Year;
           int month = date.Month;
           return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
       }

       public static DateTime StartOfMonth
       {
           get { return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0); }
       }

       public static DateTime EndOfMonth
       {
           get
           {
               int year = DateTime.Now.Year;
               int month = DateTime.Now.Month;
               return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
           }
       }

       public static DateTime MonthEndNotificationTime
       {
           get
           {
               int year = DateTime.Now.Year;
               int month = DateTime.Now.Month;
               return new DateTime(year, month, DateTime.DaysInMonth(year, month), 8, 0, 0);
           }
       }

       public static DateTime GetDateByInterval(this DateTime date, IntervalType type, int value)
       {
           var newDate = date;
           switch (type)
           {
               case IntervalType.Days:
                   {
                       newDate = date.AddDays(value);
                       break;
                   }
               case IntervalType.Hours:
                   {
                       newDate = date.AddHours(value);
                       break;
                   }
               case IntervalType.Minutes:
                   {
                       newDate = date.AddMinutes(value);
                       break;
                   }
               case IntervalType.Months:
                   {
                       newDate = date.AddMonths(value);
                       break;
                   }
               case IntervalType.Years:
                   {
                       newDate = date.AddYears(value);
                       break;
                   }
           }

           return newDate;
       }

       /// <summary>
       /// Compares 2 dates: checks if t1 is greater than t2
       /// </summary>
       /// <param name="t1">the date being checked</param>
       /// <param name="t2">the date to check against</param>
       /// <returns></returns>
       public static bool IsGreaterThan(this DateTime t1, DateTime t2)
       {
           /*
            Comparison indicator : Less than zero - means t1 is earlier than t2.
            Comparison indicator : Zero - means t1 is the same as t2.
            Comparison indicator : Greater than zero - means t1 is later than t2.
           */
           var indicator = DateTime.Compare(t1, t2);
           return indicator > 0;
       }

       /// <summary>
       /// Compares 2 dates: checks if t1 is less than t2
       /// </summary>
       /// <param name="t1">the date being checked</param>
       /// <param name="t2">the date to check against</param>
       /// <returns></returns>
       public static bool IsLessThan(this DateTime t1, DateTime t2)
       {
           /*
            Comparison indicator : Less than zero - means t1 is earlier than t2.
            Comparison indicator : Zero - means t1 is the same as t2.
            Comparison indicator : Greater than zero - means t1 is later than t2.
           */
           var indicator = DateTime.Compare(t1, t2);
           return indicator < 0;
       }

       /// <summary>
       /// Compares 2 dates: checks if t1 is less than t2
       /// </summary>
       /// <param name="t1">the date being checked</param>
       /// <param name="t2">the date to check against</param>
       /// <returns></returns>
       public static bool IsEquallTo(this DateTime t1, DateTime t2)
       {
           /*
            Comparison indicator : Less than zero - means t1 is earlier than t2.
            Comparison indicator : Zero - means t1 is the same as t2.
            Comparison indicator : Greater than zero - means t1 is later than t2.
           */
           var indicator = DateTime.Compare(t1, t2);
           return indicator == 0;
       }
    }
}
