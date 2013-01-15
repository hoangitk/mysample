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
                .AddMenuItem(new MyMenuItem("Copy", (s, me) => { this.tsGridView.CopyToClipBoard(); }))
                .AddMenuItem(new MyMenuItem("Paste")
                                    .AddChild(new MyMenuItem("Normal", (s, me) => { this.tsGridView.PasteFromClipBoard(false); }))
                                    .AddChild(new MyMenuItem("Into Selected cell(s)", (s, me) => { this.tsGridView.PasteFromClipBoard(); })));               

            #endregion

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
		
	}
}
