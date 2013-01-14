using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TimeSheetControl;

namespace TimeSheetControl
{
    public class SampleData
    {
        private static Random rand = new Random();

        public IList<TimeSheetType> SampleTimeSheetTypeList = GenerateListOfTimeSheetType();

        private static SampleData _default = null;
        public static SampleData Default
        {
            get
            {
                if (_default == null)
                    _default = new SampleData();
                return _default;
            }
        }

        private SampleData()
        {
        }

        public BindingList<TimeSheetItem> GenerateTimeSheetItemsBindingList(DateTime fromDate, DateTime toDate)
        {
            var results = new BindingList<TimeSheetItem>();
            int dayCount = (toDate - fromDate).Days + 1;

            // Add Cells
            int empCount = rand.Next(20, 50);
            for (int i = 1; i <= empCount; i++)
            {
                var newTsItem = new TimeSheetItem();

                newTsItem.EmployeeId = string.Format("{0:d6}", i);
                newTsItem.EmployeeFullName = "Employee " + i;

                newTsItem.TimeSheetDays = new List<TimeSheetDay>();

                for (int j = 0; j < dayCount; j++)
                {
                    var day = fromDate.AddDays(j);
                    var tsday = GenerateATimeSheetDay(day);
                    newTsItem.TimeSheetDays.Add(tsday);
                }

                results.Add(newTsItem);
            }

            return results;
        }

        public TimeSheetDay GenerateATimeSheetDay(DateTime initDay)
        {
            var tsday = new TimeSheetDay();
            tsday.Day = initDay;
            tsday.Catalog = GetTimeSheetType(tsday.Day);
            
            // Generate Planned Items
            tsday.ShiftItems = new List<ShiftRecord>();
            int plannedCount = rand.Next(3);

            for (int k = 0; k < plannedCount; k++)
            {
                ShiftRecord plannedItem = new ShiftRecord();
                plannedItem.FromTime = k == 0 ? tsday.Day.AddHours(-tsday.Day.Hour + rand.Next(8)) : tsday.ShiftItems[k-1].ToTime.AddHours(rand.Next(2));
                plannedItem.ToTime = plannedItem.FromTime.AddHours(rand.Next(4) + 6);
                plannedItem.TimeSheetType =  k == 0 
                    ? SampleTimeSheetTypeList.RandomBelongsCatalogs(TimeSheetCatalog.Shift)
                    : SampleTimeSheetTypeList.RandomBelongsCatalogs(TimeSheetCatalog.Shift, TimeSheetCatalog.Overtime);                
                plannedItem.Status = GetTimeSheetStatus(plannedItem.TimeSheetType.Catalog);
                tsday.ShiftItems.Add(plannedItem);
            }

            // Generate RealTime Items
            tsday.LeaveItems = new List<LeaveRecord>();
            int realTimeCount = plannedCount;

            for (int k = 0; k < realTimeCount; k++)
            {
                LeaveRecord realItem = new LeaveRecord();
                realItem.FromTime = k == 0 ? tsday.ShiftItems[0].FromTime : tsday.LeaveItems[k - 1].ToTime.AddHours(rand.Next(3));
                realItem.ToTime = realItem.FromTime.AddHours(rand.Next(4) + 6);
                realItem.TimeSheetType = SampleTimeSheetTypeList.RandomBelongsCatalogs(TimeSheetCatalog.Leave);
                realItem.Status = GetTimeSheetStatus(realItem.TimeSheetType.Catalog);
                tsday.LeaveItems.Add(realItem);
            }

            if(tsday.ShiftItems.Count > 0 || tsday.LeaveItems.Count > 0)
                tsday.Status = rand.Next(2) == 0 ? TimeSheetStatus.None : TimeSheetStatus.Locked;

            return tsday;
        }

        public TimeSheetType GenerateTimeSheetType(DateTime day)
        {
            if (rand.Next(1000) % 23 == 0)
                return SampleTimeSheetTypeList.RandomBelongsCatalogs(TimeSheetCatalog.BusinessTrip);

            switch (day.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return SampleTimeSheetTypeList.RandomBelongsCatalogs(TimeSheetCatalog.Holiday);

                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return SampleTimeSheetTypeList.RandomBelongsCatalogs(TimeSheetCatalog.WorkingDay);

                case DayOfWeek.Saturday:
                    return SampleTimeSheetTypeList.RandomBelongsCatalogs(GetRandomFrom<TimeSheetCatalog>(
                        TimeSheetCatalog.WeekendOff,
                        TimeSheetCatalog.WeekendOffHalf));
                default:
                    throw new Exception("Invalid value for DayOfWeek");
            }
        }

        #region static methods
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

        public static IList<TimeSheetType> GenerateListOfTimeSheetType(int minLengthOfEachCatalog = 5, int maxLengthOfEachCatalog = 10)
        {
            var results = new List<TimeSheetType>();            

            var listCatalog = (TimeSheetCatalog[])Enum.GetValues(typeof(TimeSheetCatalog));

            if (minLengthOfEachCatalog < 1)
                minLengthOfEachCatalog = 5;

            if (maxLengthOfEachCatalog < 10)
                maxLengthOfEachCatalog = 10;

            for (int c = 2; c < listCatalog.Length; c++)
            {
                var cat = listCatalog[c];
                var catLen = rand.Next(minLengthOfEachCatalog, maxLengthOfEachCatalog);
                for (int i = 0; i < catLen; i++)
                {
                    var tsType = new TimeSheetType()
                    {
                        Id = i + 1,
                        Catalog = cat,
                        Code = GetTimeSheetCode(cat),
                    };

                    results.Add(tsType);
                }
            }

            return results;
        }

        public static TimeSheetStatus GetTimeSheetStatus(TimeSheetCatalog catalog)
        {
            var st = TimeSheetStatus.None;
            switch (catalog)
            {
                case TimeSheetCatalog.WorkingDay:
                    st = TimeSheetStatus.None;
                    break;

                case TimeSheetCatalog.Holiday:
                    st = TimeSheetStatus.None;
                    break;

                case TimeSheetCatalog.WeekendOff:
                    st = rand.Next(2) == 0 ? TimeSheetStatus.None : TimeSheetStatus.ApprovedLeave;
                    break;

                case TimeSheetCatalog.WeekendOffHalf:
                    st = rand.Next(2) == 0 ? TimeSheetStatus.None : TimeSheetStatus.ApprovedLeave;
                    break;

                case TimeSheetCatalog.Leave:
                    st = rand.Next(2) == 0 ? TimeSheetStatus.None : TimeSheetStatus.ApprovedLeave;
                    break;

                case TimeSheetCatalog.BusinessTrip:
                    return TimeSheetStatus.None;

                case TimeSheetCatalog.Overtime:
                    switch (rand.Next(3))
                    {
                        case 0:
                            st = TimeSheetStatus.None;
                            break;
                        case 1:
                            st =  TimeSheetStatus.ApprovedOT;
                            break;
                        case 2:
                            st = TimeSheetStatus.UnApprovedOT;
                            break;
                    }
                    return st;

                case TimeSheetCatalog.Shift:
                    switch (rand.Next(3))
                    {
                        case 0:
                            st = TimeSheetStatus.None;
                            break;
                        case 1:
                            st =  TimeSheetStatus.ValidTS;
                            break;
                        case 2:
                            st = TimeSheetStatus.InvalidTS;
                            break;
                    }
                    break;

                default:
                    return TimeSheetStatus.None;
            }

            return st;
        }

        #endregion

        #region extend methods
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
        #endregion
    }

    public static class SampleDataExt
    {
        private static Random rand = new Random();

        public static T SelectRandom<T>(this IList list)
        {            
            return (T)list[rand.Next(list.Count)];
        }

        public static TimeSheetType RandomBelongsCatalogs(this IList<TimeSheetType> list, params TimeSheetCatalog[] catalog)
        {
            var listWithCatalog = list.Where(t => t != null && catalog.Contains(t.Catalog)).ToList();

            return listWithCatalog[rand.Next(listWithCatalog.Count)];
        }
    }
}
