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
using System.Diagnostics;

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

            this.tsGridView.GetColorByTimeSheetCatalog = GetColorByTimeSheetCatalog;
            this.tsGridView.GetColorByTimeSheetStatus = GetColorByTimeSheetStatus;
			
			this.Load += new EventHandler(MainForm_Load);

            this.tsGridView.CellContentDoubleClick += OnTimeSheetGridView_CellContentDoubleClick;            
		}

        private void OnTimeSheetGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= this.tsGridView.ColumnHeaderCount
                && e.RowIndex >= 0)
            {
                var selectedCell = this.tsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                TimeSheetEditorForm tsEditor = new TimeSheetEditorForm(selectedCell.Value as TimeSheetDay);
                tsEditor.StartPosition = FormStartPosition.CenterParent;
                if (tsEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    selectedCell.Value = tsEditor.Data;
                }

                //Debug.WriteLine(this.timeSheetGridView1.Rows[e.RowIndex].DataBoundItem);
            }
        }

		void MainForm_Load(object sender, EventArgs e)
		{            
            this.tsGridView.FromDate = DateTime.Now.AddDays(-15);
            this.tsGridView.ToDate = DateTime.Now.AddDays(15);

            // Add Cells           
            _timeSheetItems = SampleData.Default.GenerateTimeSheetItemsBindingList(
                this.tsGridView.FromDate, this.tsGridView.ToDate);

            this.tsGridView.DataSource = _timeSheetItems;

            for (int i = 0; i < this.tsGridView.Rows.Count; i++)
            {
                var employeeIdCell = this.tsGridView.Rows[i].Cells["EmployeeId"] as DataGridViewTextAndImageCell;
                if (employeeIdCell != null)
                {
                    employeeIdCell.Image = this.imageList1.Images.SelectRandom<Image>();
                }

            }

            // Define context menu for grid
            this.gridContextMenu.AddNewCommand(
                "Copy",
                (s, me) =>
                {
                    this.tsGridView.CopyToClipBoard();
                }, null);

            this.gridContextMenu.AddNewCommand(
                "Paste",
                (s, me) =>
                {
                    this.tsGridView.PasteFromClipBoard();
                }, null);
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
                    return Color.FromArgb(255, 255, 0);

                default:
                    return Color.Empty;
            }
        }
		
		
	}
}
