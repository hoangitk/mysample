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
using System.Collections;

namespace TimeSheetDemo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Random rand = new Random();

        private BindingList<TimeSheetItem> _timeSheetItems;

        private ArrayList _styleList;
		
		public MainForm()
		{
			InitializeComponent();

            #region Style
            _styleList = new ArrayList()
            {
                new KeyValuePair<string, Func<TimeSheetCatalog, Color>>(
                    "Style 1",
                    (tsc) => 
                    {
                        switch (tsc)
                        {
                            case TimeSheetCatalog.WorkingDay:
                                return Color.FromArgb(255, 255, 255);

                            case TimeSheetCatalog.Holiday:
                                return Color.FromArgb(248, 210, 255);

                            case TimeSheetCatalog.WeekendOff:
                                return Color.FromArgb(172, 156, 157);

                            case TimeSheetCatalog.WeekendOffHalf:
                                return Color.FromArgb(227, 227, 227);

                            case TimeSheetCatalog.Leave:
                                return Color.FromArgb(255, 249, 189);

                            case TimeSheetCatalog.BusinessTrip:
                                return Color.FromArgb(97, 166, 171);

                            case TimeSheetCatalog.Overtime:
                                return Color.FromArgb(246, 134, 134);

                            case TimeSheetCatalog.Shift:
                                return Color.FromArgb(228, 249, 255);

                            default:
                                return Color.Empty;
                        }
                    }
                ),

                new KeyValuePair<string, Func<TimeSheetCatalog, Color>>(
                    "Style 2",
                    (tsc) =>
                    {
                        switch (tsc)
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
                ),
            };
        
            #endregion Style

            this.cmbStyle.DataSource = _styleList;
            this.cmbStyle.DisplayMember = "Key";
            this.cmbStyle.ValueMember = "Value";

            this.tsGridView.GetColorByTimeSheetCatalog = this.cmbStyle.SelectedValue as Func<TimeSheetCatalog, Color>;
            this.tsGridView.GetColorByTimeSheetStatus = GetColorByTimeSheetStatus;

            this.Load += new EventHandler(MainForm_Load);            

            this.tsGridView.CellContentDoubleClick += TimeSheetGridView_CellContentDoubleClick;
            this.tsGridView.CellPasting += TimeSheetGridView_CellPasting;
            this.tsGridView.MouseUp += TimeSheetGridView_MouseUp;
		}

        
        

		void MainForm_Load(object sender, EventArgs e)
		{            
            this.tsGridView.FromDate = DateTime.Now.AddDays(-15);
            this.tsGridView.ToDate = DateTime.Now.AddDays(15);

            // Add Cells           
            _timeSheetItems = SampleData.Default.GenerateTimeSheetItemsBindingList(
                this.tsGridView.FromDate, this.tsGridView.ToDate);

            this.tsGridView.DataSource = _timeSheetItems;

            // Sample set a image for a employee
            for (int i = 0; i < this.tsGridView.Rows.Count; i++)
            {
                var employeeIdCell = this.tsGridView.Rows[i].Cells["EmployeeId"] as DataGridViewTextAndImageCell;
                if (employeeIdCell != null)
                {
                    employeeIdCell.Image = this.imageList1.Images.SelectRandom<Image>();
                }

            }

            // Define context menu for grid
            #region Sample define context menu

            this.ctxmnuGridView
                .AddNewCommand("cmdCopy", "Copy", (s, me) =>
                {
                    this.tsGridView.CopyToClipBoard();
                })
                .AddNewCommand("cmdPaste", "Paste", (s, me) =>
                {
                    this.tsGridView.PasteFromClipBoard(false);
                })
                .AddNewCommand("cmdPasteOnSelectedCells", "Paste on selected cell(s)", (s, me) =>
                {
                    this.tsGridView.PasteFromClipBoard();
                });

            this.ctxmnuTimeSheetRecord
                .AddMenuItem(new MyMenuItem("Assign")
                                    .AddChild(new MyMenuItem("1:1")
                                        .SetCommand((s, me) => { MessageBox.Show("1:1"); }))
                                    .AddChild(new MyMenuItem("2:2")
                                        .SetCommand((s, me) => { MessageBox.Show("2:2"); }))
                                    .AddChild(new MyMenuItem("3:3")
                                        .SetCommand((s, me) => { MessageBox.Show("3:3"); }))
                                    .AddChild(new MyMenuItem("4:4")
                                        .SetCommand((s, me) => { MessageBox.Show("4:4"); }))
                                    .AddChild(new MyMenuItem("5:5")))
                .AddMenuItem(new MyMenuItem("Delete")
                                    .SetCommand((s, me) => { MessageBox.Show("Delete"); }));
            
            #endregion

            this.cmbStyle.SelectedIndexChanged += (s, cbe) =>
            {
                if (cmbStyle.SelectedIndex > -1)
                {
                    this.tsGridView.GetColorByTimeSheetCatalog = this.cmbStyle.SelectedValue as Func<TimeSheetCatalog, Color>;
                    this.tsGridView.Refresh();
                }
            };
		}

        private void TimeSheetGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitInfo = this.tsGridView.HitTest(e.X, e.Y);
                if (hitInfo.ColumnIndex >= this.tsGridView.ColumnHeaderCount
                    && hitInfo.RowIndex >= 0)
                {
                    this.ctxmnuGridView.Show(this.tsGridView, e.X, e.Y);
                }
            }
        }

        void TimeSheetGridView_CellPasting(object sender, CellPastingEventArgs e)
        {
            // Handle validate selected cell
            // If the cell is not validated then set cancel = true
            // Ex: do not allow copy&paste with BusinessTrip
            if(e.NewValue.Catalog == TimeSheetCatalog.BusinessTrip)
                e.Cancel = true;
        }

        private void TimeSheetGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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
