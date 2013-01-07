﻿/*
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

        private TimeSheetStatus _status;

        public TimeSheetStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

		public List<ShiftRecord> ShiftItems { get; set; }
		public List<LeaveRecord> LeaveItems { get; set; }
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

            if (this.ShiftItems != null && this.ShiftItems.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Planned: ");
                for (int i = 0; i < this.ShiftItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", this.ShiftItems[i]);
                    if (i < this.ShiftItems.Count - 1)
                        sb.AppendLine();
                }
            }

            if (this.LeaveItems != null && this.LeaveItems.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Real time:");
                for (int i = 0; i < this.LeaveItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", this.LeaveItems[i]);
                    if (i < this.LeaveItems.Count - 1)
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
        public TimeSheetStatus Status { get; set; }

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

    public class ShiftRecord : TimeSheetRecord
    {
    }

    public class LeaveRecord : TimeSheetRecord
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
        None,
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