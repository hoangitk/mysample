using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class TimeSheetGridView : DataGridView
    {
        private DateTime _fromDate;

        public DateTime FromDate
        {
            get { return _fromDate; }
            set 
            {
                _fromDate = value;
                _toDate = _fromDate;
            }
        }

        private DateTime _toDate;

        public DateTime ToDate
        {
            get { return _toDate; }
            set 
            {
                if (value < _fromDate)
                    throw new ArgumentOutOfRangeException("ToDate must be great than or equal FromDate");

                _toDate = value;                
            }
        }   

        public int DayCount
        {
            get { return (_toDate - _fromDate).Days + 1; }
        }

        public int ColumnHeaderCount
        {
            get { return 2; }
        }

        public TimeSheetGridView()
        {
            this.AutoSize = true;
            this.AutoGenerateColumns = false;
            this.AllowUserToOrderColumns = false;            
            this.AllowUserToAddRows = false;                        
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            // Init FromDate and ToDate
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;
        }

        private void Render()
        {
            if (!this.DesignMode)
            {
                this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                this.ColumnHeadersHeight = 40;

                // Add Employee Columns
                var employeeIdColumn = new DataGridViewTextBoxColumn();
                employeeIdColumn.Name = "EmployeeId";
                employeeIdColumn.HeaderText = "Id";
                employeeIdColumn.Frozen = true;
                this.Columns.Add(employeeIdColumn);

                var employeeFullNameColumn = new DataGridViewTextBoxColumn();
                employeeFullNameColumn.Name = "EmployeeFullName";
                employeeFullNameColumn.HeaderText = "Full Name";
                employeeFullNameColumn.Frozen = true;
                this.Columns.Add(employeeFullNameColumn);               

                // Add column date
                for (int i = 0; i < this.DayCount; i++)
                {
                    DataGridViewTimeSheetColumn tsColumn = new DataGridViewTimeSheetColumn();
                    tsColumn.HeaderText = _fromDate.AddDays(i).ToString("ddd, dd/MM/yyyy");
                    this.Columns.Add(tsColumn);
                }

                var Data = this.DataSource as IList<TimeSheetItem>;
                if (Data != null)
                {

                    if (Data != null && Data.Count > 0)
                    {
                        for (int i = 0; i < Data.Count; i++)
                        {
                            var tsItem = Data[i];
                            if (tsItem.TimeSheetDays != null && tsItem.TimeSheetDays.Count > 0)
                            {
                                var dtgRow = this.Rows[i];

                                // Bind employee info into header column
                                dtgRow.Cells["EmployeeId"].Value = tsItem.EmployeeId;
                                dtgRow.Cells["EmployeeFullName"].Value = tsItem.EmployeeFullName;

                                // Bind timesheet days
                                //for (int j = 0; j < tsItem.TimeSheetDays.Count; j++)
                                //{
                                //    var tsDay = tsItem.TimeSheetDays[j];
                                //    int columnDayIndex = (tsDay.Day - this.FromDate).Days + this.ColumnHeaderCount;
                                //    this.Rows[i].Cells[columnDayIndex].Value = tsDay;
                                //}
                                for (int j = tsItem.TimeSheetDays.Count-1; j >= 0; j--)
                                {
                                    var tsDay = tsItem.TimeSheetDays[j];
                                    int columnDayIndex = (tsDay.Day - this.FromDate).Days + this.ColumnHeaderCount;
                                    this.Rows[i].Cells[columnDayIndex].Value = tsDay;
                                }

                            }// Check TimeSheetDay is available

                        }// Bind data row

                    }// Check Data is available
                }                
            }
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);

            // Auto resize column width of TSDay with same width after changed
            if (e.Column.Index >= this.ColumnHeaderCount)
            {
                for (int i = this.ColumnHeaderCount; i < this.Columns.Count; i++)
                {
                    this.Columns[i].Width = e.Column.Width;
                }
            }
        }

        protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
        {
            base.OnRowHeightChanged(e);

            // Auto resize height of all rows with same height
            for (int i = 0; i < this.Rows.Count; i++)
            {
                this.Rows[i].Height = e.Row.Height;
            }
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            var data = DataSource as IList<TimeSheetItem>;
            if (data == null)
                throw new ArgumentException("DataSource must be a list of TimeSheetItem");

            // Re-draw after DataSource changed
            Render();
        }

        //protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        //{
        //    // Show tooltip for cell
        //    DataGridViewCell cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    cell.ToolTipText = cell.Value.ToString();

        //    base.OnCellFormatting(e);
        //}
    }
}
