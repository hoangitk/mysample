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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Linq;

namespace TimeSheetControl
{
    [Serializable]
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

        public TimeSheetItem()
        {
            _employeeId = string.Empty;
            _employeeFullName = string.Empty;
        }

        public TimeSheetDay this[string day]
        {
            get
            {
                if (TimeSheetDays == null)
                    return null;

                return TimeSheetDays.Where(d => d.Day.ToString("yyyy_MM_ddd") == day).SingleOrDefault();
            }

            set
            {
                if (TimeSheetDays == null)
                {
                    lock (TimeSheetDays)
                    {
                        TimeSheetDays = new List<TimeSheetDay>();
                    }
                }

                var tsDay = TimeSheetDays.Where(d => d.Day.ToString("yyyy_MM_ddd") == day).SingleOrDefault();
                if (tsDay == null)
                    TimeSheetDays.Add(value);
                else
                    tsDay = value;
            }
        }

        public TimeSheetDay this[DateTime day]
        {
            get
            {
                if (TimeSheetDays == null)
                    return null;

                return TimeSheetDays.Where(d => d.Day == day).SingleOrDefault();
            }

            set
            {
                if (TimeSheetDays == null)
                {
                    lock (TimeSheetDays)
                    {
                        TimeSheetDays = new List<TimeSheetDay>();
                    }
                }

                var tsDay = TimeSheetDays.Where(d => d.Day == day).SingleOrDefault();
                if (tsDay == null)
                    TimeSheetDays.Add(value);
                else
                    tsDay = value;
            }
        }
    }

	/// <summary>
	/// Description of TimeSheetDay.
	/// </summary>
    [Serializable]
	public class TimeSheetDay
	{
        private DateTime _day;

        public DateTime Day
        {
            get { return _day; }
            set { 
                _day = value;
                UpdateNewDay(_day);
            }
        }

        private TimeSheetStatus _status;

        public TimeSheetStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private TimeSheetCatalog _catalog;

        public TimeSheetCatalog Catalog
        {
            get { return _catalog; }
            set { _catalog = value; }
        }


		public List<ShiftRecord> ShiftItems { get; set; }
		public List<LeaveRecord> LeaveItems { get; set; }
		
		
		public TimeSheetDay()
		{            
		}

        public TimeSheetDay(TimeSheetDay tsDay)
        {
            // Copy status
            _status = tsDay.Status;
            _catalog = tsDay.Catalog;

            // Copy shifts
            if (tsDay.ShiftItems != null)
            {
                this.ShiftItems = new List<ShiftRecord>();
                this.ShiftItems.AddRange(tsDay.ShiftItems);                
            }

            // Copy leaves
            if (tsDay.LeaveItems != null)
            {
                this.LeaveItems = new List<LeaveRecord>();
                this.LeaveItems.AddRange(tsDay.LeaveItems);
            }

            // Copy day          
            _day = tsDay.Day;
        }

        public void UpdateNewDay(DateTime newDay)
        {
            // Update day for all Shifts
            if (this.ShiftItems != null && this.ShiftItems.Count > 0)
            {
                foreach (var tsRecord in this.ShiftItems)
                {
                    tsRecord.UpdateNewDay(newDay);
                }
            }

            // Update day for all Leaves
            if (this.LeaveItems != null && this.LeaveItems.Count > 0)
            {
                foreach (var tsRecord in this.LeaveItems)
                {
                    tsRecord.UpdateNewDay(newDay);
                }
            }
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

        /// <summary>
        /// TimeSheetDay is Empty
        /// </summary>
        public static readonly TimeSheetDay Empty = default(TimeSheetDay);
	}
	
    [Serializable]
    public abstract class TimeSheetRecord : INotifyPropertyChanged
    {
        private DateTime _fromTime;

        public DateTime FromTime
        {
            get { return _fromTime; }
            set { this.PropertyChanged.ChangeAndNotify(ref _fromTime, value, () => FromTime); }
        }

        private DateTime _toTime;

        public DateTime ToTime
        {
            get { return _toTime; }
            set { this.PropertyChanged.ChangeAndNotify(ref _toTime, value, () => ToTime); }
        }

        private TimeSheetType _timeSheetType;

        public TimeSheetType TimeSheetType
        {
            get { return _timeSheetType; }
            set { this.PropertyChanged.ChangeAndNotify(ref _timeSheetType, value, () => TimeSheetType); }
        }

        private TimeSheetStatus _status;

        public TimeSheetStatus Status
        {
            get { return _status; }
            set { this.PropertyChanged.ChangeAndNotify(ref _status, value, () => Status); }
        }

        public virtual int TotalHours()
        {
            return (this.ToTime - this.FromTime).Hours;
        }

        public TimeSheetRecord()
        {
            this.FromTime = DateTime.MinValue;
            this.ToTime = DateTime.MinValue;
            this.TimeSheetType = default(TimeSheetType);
        }

        public TimeSheetRecord(TimeSheetRecord tsRecord)
        {
            _fromTime = tsRecord.FromTime;
            _toTime = tsRecord.ToTime;
            _timeSheetType = tsRecord.TimeSheetType;
            _status = tsRecord.Status;
        }

        public virtual void UpdateNewDay(DateTime newDay)
        {
            var deltaTime = _toTime - _fromTime;

            // Update new FromTime
            _fromTime = new DateTime(
                newDay.Year, newDay.Month, newDay.Day,
                _fromTime.Hour, _fromTime.Minute, _fromTime.Second); ;
            
            // Update new ToTime
            _toTime = _fromTime + deltaTime;
        }

        public override string ToString()
        {
            return string.Format("[{0} -> {1}]/{2}: {3}",
                FromTime.ToString("HH:mm"), ToTime.ToString("HH:mm"),
                this.TotalHours(),
                this.TimeSheetType.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
    public class ShiftRecord : TimeSheetRecord
    {
        public ShiftRecord() : base()
        {

        }

        public ShiftRecord(ShiftRecord shiftRecord) : base(shiftRecord)
        {

        }
    }

    [Serializable]
    public class LeaveRecord : TimeSheetRecord
    {
        public LeaveRecord() : base()
        {

        }

        public LeaveRecord(LeaveRecord leaveRecord) : base(leaveRecord)
        {

        }
    }

    [Serializable]
    public class TimeSheetType : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { this.PropertyChanged.ChangeAndNotify(ref _id, value, () => Id); }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { this.PropertyChanged.ChangeAndNotify(ref _code, value, () => Code); }
        }

        private TimeSheetCatalog _catalog;

        public TimeSheetCatalog Catalog
        {
            get { return _catalog; }
            set { this.PropertyChanged.ChangeAndNotify(ref _catalog, value, () => Catalog); }
        }

        public TimeSheetType()
        {
            _id = 0;
            _code = string.Empty;
            _catalog = TimeSheetCatalog.None;
        }     

        public override string ToString()
        {
            return string.Format("{0} ({1})", Code, Catalog);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Code.GetHashCode() ^ Catalog.GetHashCode();
        }        

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
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

    [Serializable]
	public enum TimeSheetCatalog
	{
        None,
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
