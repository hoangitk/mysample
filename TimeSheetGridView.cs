using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class TimeSheetGridView : DataGridView
    {
        public static readonly int MIN_HEADER_HEIGHT = 40;
        public static readonly int MIN_HEADER_WIDTH = 100;
        public static readonly int MIN_CELL_HEIGHT = 25;        

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
            this.SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);

            this.AutoSize = true;
            this.AutoGenerateColumns = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToAddRows = false;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.RowTemplate.Height = MIN_CELL_HEIGHT;
            this.ColumnHeadersHeight = MIN_HEADER_HEIGHT;

            // Init FromDate and ToDate
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;

            // Init PopupToolTip
            popupToolTip = new PopupControl.Popup(commentToolTip = new CommentToolTip());           

        }

        private void RenderGrid()
        {
            if (!this.DesignMode)
            {
                this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                this.ColumnHeadersHeight = MIN_HEADER_HEIGHT;

                // Add Employee Columns
                var employeeIdColumn = new DataGridViewTextAndImageColumn()
                {
                    Name = "EmployeeId",
                    HeaderText = "Id",
                    Frozen = true,
                    ImageAlign = ContentAlignment.MiddleLeft,
                };
                this.Columns.Add(employeeIdColumn);

                var employeeFullNameColumn = new DataGridViewTextBoxColumn()
                {
                    Name = "EmployeeFullName",
                    HeaderText = "Full Name",
                    Frozen = true,
                };
                this.Columns.Add(employeeFullNameColumn);

                // Add column date
                for (int i = 0; i < this.DayCount; i++)
                {
                    DataGridViewTimeSheetColumn tsColumn = new DataGridViewTimeSheetColumn()
                    {
                        HeaderText = _fromDate.AddDays(i).ToString("ddd, dd/MM/yyyy")
                    };
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

        // Auto adjust all columns with the same width
        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            if (e.Column.Width < MIN_HEADER_WIDTH)
                e.Column.Width = MIN_HEADER_WIDTH;

            // Auto resize column width of TSDay with same width after changed
            if (e.Column.Index >= this.ColumnHeaderCount)
            {
                for (int i = this.ColumnHeaderCount; i < this.Columns.Count; i++)
                {
                    this.Columns[i].Width = e.Column.Width;
                }
            }
        }

        // Auto adjust all rows with the same height
        protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
        {
            base.OnRowHeightChanged(e);

            if (e.Row.Height < MIN_CELL_HEIGHT)
                e.Row.Height = MIN_CELL_HEIGHT;

            // Auto resize height of all rows with same height
            for (int i = 0; i < this.Rows.Count; i++)
            {
                this.Rows[i].Height = e.Row.Height;
            }
        }

        // Auto adjust Column Header height
        protected override void OnColumnHeadersHeightChanged(EventArgs e)
        {
            if (this.ColumnHeadersHeight < MIN_HEADER_HEIGHT)
                this.ColumnHeadersHeight = MIN_HEADER_HEIGHT;

            base.OnColumnHeadersHeightChanged(e);
        }
                
        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            var data = DataSource as IList<TimeSheetItem>;
            if (data == null)
                throw new ArgumentException("DataSource must be a list of TimeSheetItem");

            // Re-draw after DataSource changed
            RenderGrid();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw the timesheet grid
            for (int i = 0; i < this.Rows.Count; i++)
            {
                var curRow = this.Rows[i];     

                for (int j = this.ColumnHeaderCount; j < curRow.Cells.Count; j++)
                {
                    var cell = curRow.Cells[j] as DataGridViewTimeSheetCell;
                    if (cell != null)
                    {
                        cell.Draw(e.Graphics);
                    }
                }
            }
        }

        #region Comment ToolTip

        PopupControl.Popup popupToolTip;
        CommentToolTip commentToolTip;            

        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseClick(e);
            
            if (e.Button == System.Windows.Forms.MouseButtons.Left
                && !popupToolTip.Visible
                && e.ColumnIndex >= this.ColumnHeaderCount
                && e.RowIndex >= 0)
            {
                var tsDay = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as TimeSheetDay;
                if (tsDay != null)
                {
                    commentToolTip.Title = tsDay.GetTitle();
                    commentToolTip.Content = tsDay.GetContent();
                    Rectangle rect = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    popupToolTip.Show(this, rect);
                }
            }
        }

        protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            popupToolTip.Hide();
            base.OnCellMouseLeave(e);
        }

        #endregion  Comment ToolTip

        #region Function for getting color from setting

        public Func<TimeSheetCatalog, Color> GetColorByTimeSheetCatalog;
        public Func<TimeSheetStatus, Color> GetColorByTimeSheetStatus;               

        #endregion
    }
}