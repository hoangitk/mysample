/*
 * Created by SharpDevelop.
 * User: HoangITK
 * Date: 12/29/2012
 * Time: 12:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using TimeSheetControl;

namespace TimeSheetDemo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Random rand = new Random();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.Load += new EventHandler(MainForm_Load);
		}

		void MainForm_Load(object sender, EventArgs e)
		{	        
			DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();			
			column1.Frozen = true;
	        column1.Name = "EmployeeID";
	        dataGridView1.Columns.Add(column1);
	        
	        DateTime fromDate = new DateTime(2012, 12, 27);
	        DateTime toDate = new DateTime(2013, 1, 27);
	        
	        int dayCount = (toDate - fromDate).Days + 1;
	        
	        // Add column date
	        for (int i = 0; i < dayCount; i++) {
	        	DataGridViewTimeSheetColumn tsColumn = new DataGridViewTimeSheetColumn();
	        	tsColumn.HeaderText = fromDate.AddDays(i).ToString("ddd, dd/MM/yyyy");
	        	dataGridView1.Columns.Add(tsColumn);
	        }
	        
	        dataGridView1.AutoSize = true;
	        dataGridView1.AllowUserToAddRows = false;
	        dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
	            DataGridViewContentAlignment.MiddleCenter;
	        dataGridView1.RowCount = 20;
	        
	        // Add Cells
	        for (int i = 0; i < dataGridView1.Rows.Count; i++) 
	        {
	        	var curRow = dataGridView1.Rows[i];
	        	curRow.Cells[0].Value = "EmployeeID " + i;
	        	for (int j = 1; j < dataGridView1.Columns.Count; j++) 
	        	{
	        		var tsday = new TimeSheetDay();
	        		var curDay = fromDate.AddDays(j -1);
	        		tsday.TimeSheetType = GetTimeSheetType(curDay);
	        		tsday.ShiftItems = new List<ShiftItem>();
	        		
	        		int shiftCount = rand.Next(3);
	        		for (int k = 0; k < shiftCount; k++) 
	        		{
	        			ShiftItem newShift = new ShiftItem();
	        			newShift.TSType = shiftCount % 2 == 0 ? TimeSheetType.Shift : TimeSheetType.Leave;
	        			newShift.FromtTime = curDay.AddHours(-curDay.Hour + rand.Next(8) + 8);
	        			newShift.ToTime = newShift.FromtTime.AddHours(8);
	        			tsday.ShiftItems.Add(newShift);
	        		}
	        		
	        		curRow.Cells[j].Value = tsday;
	        	}
	        }
		}
		
		private TimeSheetType GetTimeSheetType(DateTime day)
		{
			switch (day.DayOfWeek) {
				case DayOfWeek.Sunday:
					return TimeSheetType.Holiday;
					
				case DayOfWeek.Monday:					
				case DayOfWeek.Tuesday:
				case DayOfWeek.Wednesday:
				case DayOfWeek.Thursday:
				case DayOfWeek.Friday:
					return TimeSheetType.WorkingDay;
				
				case DayOfWeek.Saturday:
					return TimeSheetType.WeekendOff;
				default:
					throw new Exception("Invalid value for DayOfWeek");
			}	
		}
	}
}
