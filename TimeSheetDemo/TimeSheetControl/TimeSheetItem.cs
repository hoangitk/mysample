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

		public List<PlannedItem> ShiftItems { get; set; }
		public List<RealTimeItem> RealTimeItems { get; set; }
		public List<TimeSheetStatus> Statuses { get; set; }
		public TimeSheetCatalog TimeSheetType { get; set; }
		
		public TimeSheetDay()
		{
            _day = DateTime.Now;
		}
	}
	
	public class PlannedItem
	{
		public DateTime FromTime { get; set; }
		public DateTime ToTime { get; set; }
        public TimeSheet TimeSheet { get; set; }
		
		public PlannedItem()
		{   
		}
	}
	
	public class RealTimeItem
	{
		public DateTime FromTime { get; set; }
		public DateTime ToTime { get; set; }
        public TimeSheet TimeSheet { get; set; }
		
		public RealTimeItem()
		{
			
		}
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
