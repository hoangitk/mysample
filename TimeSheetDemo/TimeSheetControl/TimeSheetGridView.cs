using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class TimeSheetGridView : DataGridView
    {
        #region Constants
        
        public static readonly int MIN_HEADER_HEIGHT = 40;
        public static readonly int MIN_HEADER_WIDTH = 100;
        public static readonly int MIN_CELL_HEIGHT = 25;
        public static readonly string COLUMN_TIMESHEET_NAME_ID_FORMAT = "yyyy_MM_dd";
        public static readonly string EMPLOYEE_ID_COLUMN_NAME = "EmployeeId";
        public static readonly string EMPLOYEE_FULLNAME_COLUMN_NAME = "EmployeeFullName";

        #endregion Constants

        #region Properties

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

        private string _headerDateFormat;

        [Category("TimeSheet")]
        public string HeaderDateFormat
        {
            get { return _headerDateFormat; }
            set { _headerDateFormat = value; }
        } 

        #endregion Properties
        
        public TimeSheetGridView()
        {
            this.SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);

            #region Default grid view
            
            this.AutoSize = true;
            this.AutoGenerateColumns = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToAddRows = false;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.RowTemplate.Height = MIN_CELL_HEIGHT;
            this.ColumnHeadersHeight = MIN_HEADER_HEIGHT;
            
            #endregion

            // Init FromDate and ToDate
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;

            _headerDateFormat = "ddd, dd/MM/yyyy";

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
                    var idDate = _fromDate.AddDays(i);
                    DataGridViewTimeSheetColumn tsColumn = new DataGridViewTimeSheetColumn()
                    {
                        Name = idDate.ToString(COLUMN_TIMESHEET_NAME_ID_FORMAT),
                        HeaderText = idDate.ToString(this.HeaderDateFormat),
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
                                dtgRow.Cells[EMPLOYEE_ID_COLUMN_NAME].Value = tsItem.EmployeeId;
                                dtgRow.Cells[EMPLOYEE_FULLNAME_COLUMN_NAME].Value = tsItem.EmployeeFullName;

                                // Bind timesheet days
                                for (int j = 0; j < tsItem.TimeSheetDays.Count; j++)
                                {
                                    var tsDay = tsItem.TimeSheetDays[j];
                                    
                                    // old method: bind by array
                                    //int columnDayIndex = (tsDay.Day - this.FromDate).Days + this.ColumnHeaderCount;
                                    //this.Rows[i].Cells[columnDayIndex].Value = tsDay;

                                    // new method: bind by column name
                                    var columnDayName = tsDay.Day.ToString(COLUMN_TIMESHEET_NAME_ID_FORMAT);
                                    var cell = this.Rows[i].Cells[columnDayName];                                    
                                    if (cell != null)
                                        cell.Value = tsDay;
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

                // Only show tooltip when the mouse is in topright corner of cell
                Rectangle rect = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                if (tsDay != null && rect.CheckPointInCorner(e.X, e.Y, ContentAlignment.TopRight))
                {
                    commentToolTip.Title = tsDay.GetTitle();
                    commentToolTip.Content = tsDay.GetContent();                    
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

        #region Other supporting functions
        
        #endregion
    }
}