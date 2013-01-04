using System;
using System.Collections;
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
            tsday.Catalog = GetTimeSheetType(tsday.Day);
            
            // Generate Planned Items
            tsday.PlannedItems = new List<PlannedItem>();
            int plannedCount = rand.Next(3);

            for (int k = 0; k < plannedCount; k++)
            {
                PlannedItem plannedItem = new PlannedItem();
                plannedItem.FromTime = k == 0 ? tsday.Day.AddHours(-tsday.Day.Hour + rand.Next(8)) : tsday.PlannedItems[k-1].ToTime.AddHours(rand.Next(2));
                plannedItem.ToTime = plannedItem.FromTime.AddHours(rand.Next(4) + 6);
                plannedItem.TimeSheetType = new TimeSheet();
                plannedItem.TimeSheetType.Catalog = 
                    k == 0 
                    ? GetRandomFrom<TimeSheetCatalog>(TimeSheetCatalog.Leave, TimeSheetCatalog.Shift)
                    : GetRandomFrom<TimeSheetCatalog>(TimeSheetCatalog.Leave, TimeSheetCatalog.Shift);
                plannedItem.TimeSheetType.Code = GetTimeSheetCode(plannedItem.TimeSheetType.Catalog);                
                
                tsday.PlannedItems.Add(plannedItem);
            }

            // Generate RealTime Items
            tsday.RealTimeItems = new List<RealTimeItem>();
            int realTimeCount = plannedCount;

            for (int k = 0; k < realTimeCount; k++)
            {
                RealTimeItem realItem = new RealTimeItem();
                realItem.FromTime = k == 0 ? tsday.PlannedItems[0].FromTime : tsday.RealTimeItems[k - 1].ToTime.AddHours(rand.Next(3));
                realItem.ToTime = realItem.FromTime.AddHours(rand.Next(4) + 6);
                realItem.TimeSheetType = new TimeSheet();
                realItem.TimeSheetType.Catalog =
                    k == 0
                    ? GetRandomFrom<TimeSheetCatalog>(TimeSheetCatalog.Leave, TimeSheetCatalog.Shift)
                    : GetRandomFrom<TimeSheetCatalog>(TimeSheetCatalog.Leave, TimeSheetCatalog.Shift, TimeSheetCatalog.Overtime);
                realItem.TimeSheetType.Code = GetTimeSheetCode(realItem.TimeSheetType.Catalog);      

                tsday.RealTimeItems.Add(realItem);    
            }

            return tsday;
        }

        public static TimeSheetCatalog GetTimeSheetType(DateTime day)
        {
            if (rand.Next(1000) % 23 == 0)
                return TimeSheetCatalog.BusinessTrip;

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
                    return GetRandomFrom<TimeSheetCatalog>(
                        TimeSheetCatalog.WeekendOff, 
                        TimeSheetCatalog.WeekendOffHalf);
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
                    return string.Format("H{0:d2}", rand.Next(1, 9));

                case TimeSheetCatalog.WeekendOff:
                    return string.Format("W{0:d2}", rand.Next(1, 9));

                case TimeSheetCatalog.WeekendOffHalf:
                    return string.Format("WH{0:d1}", rand.Next(1, 9));

                case TimeSheetCatalog.Leave:
                    return string.Format("L{0:d2}", rand.Next(1, 99));

                case TimeSheetCatalog.BusinessTrip:
                    return string.Format("BT{0:d1}", rand.Next(1, 9));

                case TimeSheetCatalog.Overtime:
                    return string.Format("OT{0:d1}", rand.Next(1, 9));

                case TimeSheetCatalog.Shift:
                    return string.Format("S{0:d2}", rand.Next(1, 99));

                default:
                    return string.Empty;
            }
        }

        public static T RandomEnum<T>()
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[rand.Next(values.Length)];
        }

        public static T RandomEnum<T>(params T[] excludes)
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().Where(t => !excludes.Contains(t)).ToArray();
            return values[rand.Next(values.Length)];
        }

        public static T GetRandomFrom<T>(params T[] list)
        {
            return list[rand.Next(list.Length)];
        }

        public static T SelectRandom<T>(this IList list)
        {
            return (T)list[rand.Next(list.Count)];
        }
    }
}
