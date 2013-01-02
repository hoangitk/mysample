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
                        Catalog = shiftCount % 2 == 0 ? TimeSheetCatalog.Shift : TimeSheetCatalog.Leave                        
                    },                    
                    FromTime = tsday.Day.AddHours(-tsday.Day.Hour + rand.Next(8) + 8),
                };
                plannedItem.TimeSheet.Code = GetTimeSheetCode(plannedItem.TimeSheet.Catalog);
                plannedItem.ToTime = plannedItem.FromTime.AddHours(8);

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
    }
}
