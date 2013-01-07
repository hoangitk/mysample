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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TimeSheetControl;
using Cadena.WinForms;

namespace TimeSheetDemo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Random rand = new Random();

        private BindingList<TimeSheetItem> _timeSheetItems;
		
		public MainForm()
		{
			InitializeComponent();

            this.timeSheetGridView1.GetColorByTimeSheetCatalog = GetColorByTimeSheetCatalog;
            this.timeSheetGridView1.GetColorByTimeSheetStatus = GetColorByTimeSheetStatus;
			
			this.Load += new EventHandler(MainForm_Load);

            this.btnEdit.Click += BtnEdit_Click;

            this.timeSheetGridView1.CellContentDoubleClick += TimeSheetGridView_CellContentDoubleClick;
		}

        void TimeSheetGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > this.timeSheetGridView1.ColumnHeaderCount && e.RowIndex >= 0)
            {
                var tsDay = this.timeSheetGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as TimeSheetDay;
                if (tsDay != null)
                {
                    TimeSheetEditorForm editorForm = new TimeSheetEditorForm(tsDay);
                    editorForm.Show();
                }

            }
        }

        void BtnEdit_Click(object sender, EventArgs e)
        {
            if (this.timeSheetGridView1.SelectedCells.Count > 0)
            {
                var selectedCell = this.timeSheetGridView1.SelectedCells[0];
                TimeSheetEditorForm tsEditor = new TimeSheetEditorForm(selectedCell.Value as TimeSheetDay);
                tsEditor.StartPosition = FormStartPosition.CenterParent;
                tsEditor.Show();
            }
        }

		void MainForm_Load(object sender, EventArgs e)
		{
            _timeSheetItems = new BindingList<TimeSheetItem>();
            this.timeSheetGridView1.FromDate = DateTime.Now.AddDays(-15);
            this.timeSheetGridView1.ToDate = DateTime.Now.AddDays(15);

            // Add Cells
            int empCount = rand.Next(20, 50);
            for (int i = 1; i <= empCount; i++)
            {
                var newTsItem = new TimeSheetItem();

                newTsItem.EmployeeId = string.Format("{0:d6}", i);
                newTsItem.EmployeeFullName = "Employee " + i;

                newTsItem.TimeSheetDays = new List<TimeSheetDay>();

                for (int j = 0; j < this.timeSheetGridView1.DayCount; j++)
                {
                    var tsday = SampleData.GenerateATimeSheetDay(this.timeSheetGridView1.FromDate.AddDays(j));
                    newTsItem.TimeSheetDays.Add(tsday);
                }

                _timeSheetItems.Add(newTsItem);
            }


            this.timeSheetGridView1.DataSource = _timeSheetItems;

            for (int i = 0; i < this.timeSheetGridView1.Rows.Count; i++)
            {
                var employeeIdCell = this.timeSheetGridView1.Rows[i].Cells["EmployeeId"] as DataGridViewTextAndImageCell;
                if (employeeIdCell != null)
                {
                    employeeIdCell.Image = this.imageList1.Images.SelectRandom<Image>();
                }

            }
		}

        public Color GetColorByTimeSheetCatalog(TimeSheetCatalog tsType)
        {
            switch (tsType)
            {
                case TimeSheetCatalog.WorkingDay:
                    return Color.FromArgb(165, 165, 165);

                case TimeSheetCatalog.Holiday:
                    return Color.FromArgb(252, 213, 180);

                case TimeSheetCatalog.WeekendOff:
                    return Color.FromArgb(255, 190, 0);

                case TimeSheetCatalog.WeekendOffHalf:
                    return Color.FromArgb(178, 161, 199);

                case TimeSheetCatalog.Leave:
                    return Color.FromArgb(0, 112, 192);

                case TimeSheetCatalog.BusinessTrip:
                    return Color.FromArgb(255, 255, 255);

                case TimeSheetCatalog.Overtime:
                    return Color.FromArgb(192, 0, 0);

                case TimeSheetCatalog.Shift:
                    return Color.FromArgb(182, 221, 232);

                default:
                    return Color.Empty;
            }
        }

        public Color GetColorByTimeSheetStatus(TimeSheetStatus tsStatus)
        {
            switch (tsStatus)
            {
                case TimeSheetControl.TimeSheetStatus.InvalidTS:
                    return Color.FromArgb(255, 0, 0);

                case TimeSheetControl.TimeSheetStatus.ValidTS:
                    return Color.FromArgb(0, 255, 0);

                case TimeSheetControl.TimeSheetStatus.UnApprovedOT:
                    return Color.FromArgb(255, 0, 0);

                case TimeSheetControl.TimeSheetStatus.ApprovedOT:
                    return Color.FromArgb(0, 255, 0);

                case TimeSheetControl.TimeSheetStatus.ApprovedLeave:
                    return Color.FromArgb(0, 255, 0);

                case TimeSheetControl.TimeSheetStatus.Locked:
                    return Color.FromArgb(255, 0, 0);

                default:
                    return Color.Empty;
            }
        }
		
		
	}
}
