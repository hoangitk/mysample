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
			
			this.Load += new EventHandler(MainForm_Load);            
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
		
		
	}
}
