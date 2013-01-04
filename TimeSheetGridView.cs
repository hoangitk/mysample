using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);

            this.AutoSize = true;
            this.AutoGenerateColumns = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToAddRows = false;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.RowTemplate.Height = TimeSheetRender.MIN_CELL_HEIGHT;
            this.ColumnHeadersHeight = TimeSheetRender.MIN_HEADER_HEIGHT;

            // Init FromDate and ToDate
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;

            // Init PopupToolTip
            popupToolTip = new PopupControl.Popup(commentToolTip = new CommentToolTip())
            {
                AutoClose = true,
                FocusOnOpen = true,                
                //ShowingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.LeftToRight,
                //HidingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.LeftToRight,
            };

        }

        private void Render()
        {
            if (!this.DesignMode)
            {
                this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                this.ColumnHeadersHeight = TimeSheetRender.MIN_HEADER_HEIGHT;

                // Add Employee Columns
                var employeeIdColumn = new DataGridViewTextAndImageColumn();
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

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            if (e.Column.Width < TimeSheetRender.MIN_HEADER_WIDTH)
                e.Column.Width = TimeSheetRender.MIN_HEADER_WIDTH;

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

            if (e.Row.Height < TimeSheetRender.MIN_CELL_HEIGHT)
                e.Row.Height = TimeSheetRender.MIN_CELL_HEIGHT;

            // Auto resize height of all rows with same height
            for (int i = 0; i < this.Rows.Count; i++)
            {
                this.Rows[i].Height = e.Row.Height;
            }
        }

        protected override void OnColumnHeadersHeightChanged(EventArgs e)
        {
            if (this.ColumnHeadersHeight < TimeSheetRender.MIN_HEADER_HEIGHT)
                this.ColumnHeadersHeight = TimeSheetRender.MIN_HEADER_HEIGHT;

            base.OnColumnHeadersHeightChanged(e);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < this.Rows.Count; i++)
            {
                var curRow = this.Rows[i];
                for (int j = this.ColumnHeaderCount; j < curRow.Cells.Count; j++)
                {
                    var cell = curRow.Cells[j];
                    var tsDay = cell.Value as TimeSheetDay;

                    var cellRect = cell.GetCellBoundRectangle();
                    if (cellRect != Rectangle.Empty)
                    {
                        TimeSheetRender.DrawTimeSheetDay(e.Graphics, cellRect,
                               cell.DataGridView.DefaultCellStyle, tsDay);
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
            base.OnCellMouseLeave(e);

            popupToolTip.Hide();
        }

            

        #endregion
        //protected override void OnScroll(ScrollEventArgs e)
        //{
        //    base.OnScroll(e);

        //    // Trick to solve tearing problem in GDI+
        //    if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll
        //        && e.NewValue <= e.OldValue)
        //    {
        //        this.Refresh();
        //    }
        //}
    }
}