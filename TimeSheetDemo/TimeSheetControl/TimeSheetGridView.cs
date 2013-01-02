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

        private List<TimeSheetItem> _data;

        public List<TimeSheetItem> Data
        {
            get { return _data; }
            set 
            { 
                _data = value;
                Render();
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
            base.AutoGenerateColumns = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.AllowUserToAddRows = false;
            this.AutoSize = true;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;         
            
            
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;
        }

        private void Render()
        {
            if (!this.DesignMode)
            {
                this.Rows.Clear();
                this.Columns.Clear();

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

                Debug.WriteLine("DayCount: {0}", this.DayCount);

                // Add column date
                for (int i = 0; i < this.DayCount; i++)
                {
                    DataGridViewTimeSheetColumn tsColumn = new DataGridViewTimeSheetColumn();
                    tsColumn.HeaderText = _fromDate.AddDays(i).ToString("ddd, dd/MM/yyyy");
                    this.Columns.Add(tsColumn);
                }

                this.RowCount = Data == null ? 0 : Data.Count;

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
                            for (int j = 0; j < tsItem.TimeSheetDays.Count; j++)
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
}
