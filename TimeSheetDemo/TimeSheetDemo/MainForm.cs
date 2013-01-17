using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TimeSheetControl;
using Cadena.WinForms;
using System.Diagnostics;
using System.Collections;
using System.Linq;

namespace TimeSheetDemo
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Random rand = new Random();

        private BindingList<TimeSheetItem> _timeSheetItems;        

        TimeSheetGridContextMenu ctxmnuGridView;

		public MainForm()
		{
			InitializeComponent();           

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
            
            this.ctxmnuGridView = new TimeSheetGridContextMenu();
            InitContextMenu();

            #endregion            
            this.propertyGrid1.SelectedObject = this.tsGridView;

		}

        private void InitContextMenu()
        {
            this.ctxmnuGridView.MenuItemCopy
                .SetCommand((s, me) => this.tsGridView.CopyToClipBoard())
                .SetShortcutKeys(Keys.Control | Keys.C);
            this.ctxmnuGridView.MenuItemPasteNormal
                .SetCommand((s, me) => this.tsGridView.PasteFromClipBoard())
                .SetShortcutKeys(Keys.Control | Keys.V);
            this.ctxmnuGridView.MenuItemPasteSelectedCells
                .SetCommand((s, me) => this.tsGridView.PasteFromClipBoard(false))
                .SetShortcutKeys(Keys.Control | Keys.Shift | Keys.V);            
            this.ctxmnuGridView.MenuItemAssignFullDayOff
                .SetCommand((s, me) => MessageBox.Show(s.ToString()));
            this.ctxmnuGridView.MenuItemAssignFullLeave
                .SetCommand((s, me) => MessageBox.Show(s.ToString()));
            this.ctxmnuGridView.MenuItemAssignHalfDayOff
                .SetCommand((s, me) => MessageBox.Show(s.ToString()));
            this.ctxmnuGridView.MenuItemAssignHalfLeave
                .SetCommand((s, me) => MessageBox.Show(s.ToString()));
            this.ctxmnuGridView.MenuItemDelete
                .SetCommand((s, me) => MessageBox.Show(s.ToString()));

            // Sample: Assign Shift
            var shifts = SampleData.Default.SampleTimeSheetTypeList.Where(t => t.Catalog == TimeSheetCatalog.Shift);
            foreach (var tsShift in shifts)
            {
                this.ctxmnuGridView.MenuItemAssignShift
                    .AddChild(new MyMenuItem(tsShift.Code.ToString())
                        .SetCommand((s, me) => MessageBox.Show(s.ToString())));
            }
        }

        private void TimeSheetGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitInfo = this.tsGridView.HitTest(e.X, e.Y);
                if (hitInfo.ColumnIndex >= this.tsGridView.ColumnHeaderCount
                    && hitInfo.RowIndex >= 0)
                {
                    this.ctxmnuGridView.MenuItemDelete.ClearChildren();

                    // Generate Delete command for ContextMenu
                    // Assume: Only accept 1 cell same time
                    if (this.tsGridView.SelectedCells.Count == 1)
                    {
                        var tsDay = this.tsGridView.SelectedCells[0].Value as TimeSheetDay;
                        if (tsDay != null)
                        {
                            if (tsDay.ShiftItems != null && tsDay.ShiftItems.Count > 0)
                            {                                
                                foreach (TimeSheetRecord tsRecord in tsDay.ShiftItems)
                                {
                                    this.ctxmnuGridView.MenuItemDelete
                                        .AddChild(new MyMenuItem(string.Format("{0} [{1}-{2}]", 
                                                                    tsRecord.TimeSheetType,
                                                                    tsRecord.FromTime.ToString("HH:mm"),
                                                                    tsRecord.ToTime.ToString("HH:mm")))
                                            .SetCommand((s, me) => MessageBox.Show(s.ToString())));
                                }
                            }// Shift

                            if (tsDay.LeaveItems != null && tsDay.LeaveItems.Count > 0)
                            {
                                this.ctxmnuGridView.MenuItemDelete.AddSeperator();

                                foreach (TimeSheetRecord tsRecord in tsDay.LeaveItems)
                                {
                                    this.ctxmnuGridView.MenuItemDelete
                                        .AddChild(new MyMenuItem(string.Format("{0} [{1}-{2}]",
                                                                    tsRecord.TimeSheetType,
                                                                    tsRecord.FromTime.ToString("HH:mm"),
                                                                    tsRecord.ToTime.ToString("HH:mm")))
                                            .SetCommand((s, me) => MessageBox.Show(s.ToString()))); 
                                }
                            }// Leave                            
                        }
                    }

                    // Show ContextMenu
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
		
	}
}
