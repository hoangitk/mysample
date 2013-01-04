/*
 * Created by SharpDevelop.
 * User: HoangITK
 * Date: 12/29/2012
 * Time: 12:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TimeSheetControl
{
    public class TimeSheetItem
    {
        private string _employeeId;

        public string EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        private string _employeeFullName;

        public string EmployeeFullName
        {
            get { return _employeeFullName; }
            set { _employeeFullName = value; }
        }

        private List<TimeSheetDay> _timeSheetDays;

        public List<TimeSheetDay> TimeSheetDays
        {
            get { return _timeSheetDays; }
            set { _timeSheetDays = value; }
        }
    }

	/// <summary>
	/// Description of TimeSheetDay.
	/// </summary>
	public class TimeSheetDay
	{
        private DateTime _day;

        public DateTime Day
        {
            get { return _day; }
            set { _day = value; }
        }

		public List<PlannedItem> PlannedItems { get; set; }
		public List<RealTimeItem> RealTimeItems { get; set; }
		public List<TimeSheetStatus> Statuses { get; set; }
		public TimeSheetCatalog Catalog { get; set; }
		
		public TimeSheetDay()
		{
            _day = DateTime.Now;
		}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Day.ToString("yyyy/MM/dd"));
            sb.Append(Catalog.ToString());

            if (this.PlannedItems != null && this.PlannedItems.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Planned: ");
                for (int i = 0; i < this.PlannedItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", this.PlannedItems[i]);
                    if (i < this.PlannedItems.Count - 1)
                        sb.AppendLine();
                }
            }

            if (this.RealTimeItems != null && this.RealTimeItems.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Real time:");
                for (int i = 0; i < this.RealTimeItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", this.RealTimeItems[i]);
                    if (i < this.RealTimeItems.Count - 1)
                        sb.AppendLine();
                }
            }

            if (this.Statuses != null && this.Statuses.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Status:");
                for (int i = 0; i < this.Statuses.Count; i++)
                {
                    sb.AppendFormat("+ {0}", this.Statuses[i]);
                    if (i < this.Statuses.Count - 1)
                        sb.AppendLine();
                }
            }

            return sb.ToString();
        }
	}
	
    public abstract class TimeSheetRecord
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public TimeSheet TimeSheetType { get; set; }

        public virtual int TotalHours()
        {
            return (this.ToTime - this.FromTime).Hours;
        }

        public override string ToString()
        {
            return string.Format("[{0} -> {1}]/{2}: {3}",
                FromTime.ToString("HH:mm"), ToTime.ToString("HH:mm"),
                this.TotalHours(),
                this.TimeSheetType.ToString());
        }
    }

    public class PlannedItem : TimeSheetRecord
    {
    }

    public class RealTimeItem : TimeSheetRecord
    {
    }


    public class TimeSheet
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private TimeSheetCatalog _catalog;

        public TimeSheetCatalog Catalog
        {
            get { return _catalog; }
            set { _catalog = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Code, Catalog);
        }
    }
	
	public enum TimeSheetStatus
	{
		InvalidTS,
		ValidTS,
		UnApprovedOT,
		ApprovedOT,
		ApprovedLeave,
		Locked
	}
	
	public enum TimeSheetCatalog
	{
		WorkingDay,
		Holiday,
		WeekendOff,
		WeekendOffHalf,
		Leave,
		BusinessTrip,
		Overtime,
		Shift
	}
	
	
	
}
