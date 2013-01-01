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
	/// <summary>
	/// Description of TimeSheetItem.
	/// </summary>
	public class TimeSheetDay
	{
		public List<ShiftItem> ShiftItems { get; set; }
		public List<RealTimeItem> RealTimeItems { get; set; }
		public List<TimeSheetStatus> Statuses { get; set; }
		public TimeSheetType TimeSheetType { get; set; }
		
		public TimeSheetDay()
		{
		}
		
		public static Color GetColor(TimeSheetType tsType)
		{
			switch (tsType) {
				case TimeSheetType.WorkingDay:
					return Color.FromArgb(255, 255, 255);
					
				case TimeSheetType.Holiday:
					return Color.FromArgb(252, 213, 180);
					
				case TimeSheetType.WeekendOff:
					return Color.FromArgb(255, 190, 0);
					
				case TimeSheetType.WeekendOffHalf:
					return Color.FromArgb(178, 161, 199);
					
				case TimeSheetType.Leave:
					return Color.FromArgb(0, 112, 192);
					
				case TimeSheetType.BusinessTrip:
					return Color.FromArgb(165, 165, 165);
					
				case TimeSheetType.Overtime:
					return Color.FromArgb(192, 0, 0);
					
				case TimeSheetType.Shift:
					return Color.FromArgb(182, 221, 232);
					
				default:
					throw new Exception("Invalid value for TimeSheetType");
			}
		}
		
		public static Color GetColor(TimeSheetStatus tsStatus)
		{
			switch (tsStatus) {
				case TimeSheetControl.TimeSheetStatus.InvalidTS:
					return Color.FromArgb(182, 221, 232);

				case TimeSheetControl.TimeSheetStatus.ValidTS:
					return Color.FromArgb(182, 221, 232);

				case TimeSheetControl.TimeSheetStatus.UnApprovedOT:
					return Color.FromArgb(182, 221, 232);

				case TimeSheetControl.TimeSheetStatus.ApprovedOT:
					return Color.FromArgb(182, 221, 232);

				case TimeSheetControl.TimeSheetStatus.ApprovedLeave:
					return Color.FromArgb(182, 221, 232);

				case TimeSheetControl.TimeSheetStatus.Locked:
					return Color.FromArgb(182, 221, 232);
				default:
					throw new Exception("Invalid value for TimeSheetStatus");
			}
		}
	}
	
	public class ShiftItem
	{
		public DateTime FromtTime { get; set; }
		public DateTime ToTime { get; set; }
		public TimeSheetType TSType { get; set; }
		
		public ShiftItem()
		{
			
		}
	}
	
	public class RealTimeItem
	{
		public DateTime FromtTime { get; set; }
		public DateTime ToTime { get; set; }
		public TimeSheetType TSType { get; set; }
		
		public RealTimeItem()
		{
			
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
	
	public enum TimeSheetType
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
