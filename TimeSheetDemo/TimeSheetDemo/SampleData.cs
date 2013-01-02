using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeSheetControl;

namespace TimeSheetDemo
{
    public static class SampleData
    {
        private static Random rand = new Random();

        public static TimeSheetDay GenerateATimeSheetDay(DateTime initDay)
        {
            var tsday = new TimeSheetDay();
            tsday.Day = initDay;
            tsday.TimeSheetType = GetTimeSheetType(tsday.Day);
            tsday.ShiftItems = new List<PlannedItem>();

            int shiftCount = rand.Next(3);
            for (int k = 0; k < shiftCount; k++)
            {
                PlannedItem plannedItem = new PlannedItem
                {
                    TimeSheet = new TimeSheet
                    {
                        Catalog = RandomEnum<TimeSheetCatalog>(TimeSheetCatalog.WorkingDay)                        
                    }, 
                };
                plannedItem.FromTime = k == 0 ? tsday.Day.AddHours(-tsday.Day.Hour + rand.Next(8)) : tsday.ShiftItems[k-1].ToTime.AddHours(rand.Next(1));
                plannedItem.ToTime = plannedItem.FromTime.AddHours(rand.Next(6) + 4); 
                plannedItem.TimeSheet.Code = GetTimeSheetCode(plannedItem.TimeSheet.Catalog);                

                tsday.ShiftItems.Add(plannedItem);
            }

            return tsday;
        }

        public static TimeSheetCatalog GetTimeSheetType(DateTime day)
        {
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return TimeSheetCatalog.Holiday;

                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return TimeSheetCatalog.WorkingDay;

                case DayOfWeek.Saturday:
                    return TimeSheetCatalog.WeekendOff;
                default:
                    throw new Exception("Invalid value for DayOfWeek");
            }
        }

        public static string GetTimeSheetCode(TimeSheetCatalog catalog)
        {
            switch (catalog)
            {
                case TimeSheetCatalog.WorkingDay:
                    return string.Empty;

                case TimeSheetCatalog.Holiday:
                    return string.Format("H{0:d2}", rand.Next(9));

                case TimeSheetCatalog.WeekendOff:
                    return string.Format("W{0:d2}", rand.Next(9));

                case TimeSheetCatalog.WeekendOffHalf:
                    return string.Format("WH{0:d1}", rand.Next(9));

                case TimeSheetCatalog.Leave:
                    return string.Format("L{0:d2}", rand.Next(99));

                case TimeSheetCatalog.BusinessTrip:
                    return string.Format("BT{0:d1}", rand.Next(9));

                case TimeSheetCatalog.Overtime:
                    return string.Format("OT{0:d1}", rand.Next(9));

                case TimeSheetCatalog.Shift:
                    return string.Format("S{0:d2}", rand.Next(99));

                default:
                    return string.Empty;
            }
        }

        public static T RandomEnum<T>()
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[rand.Next(0, values.Length - 1)];
        }

        public static T RandomEnum<T>(params T[] excludes)
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().Where(t => !excludes.Contains(t)).ToArray();
            return values[rand.Next(values.Length)];
        }
    }
}
