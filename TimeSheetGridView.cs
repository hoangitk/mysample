﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

internal class resfinder { }

namespace TimeSheetControl
{
    [ToolboxBitmap(typeof(resfinder), "TimeSheetControl.grid.ico")]
    [ToolboxItem(true)]
    [Description("TimeSheetGridView show time-sheet data on grid")]
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

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        [Category("TimeSheet")]
        [Browsable(false)]
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

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">ToDate must be great than or equal FromDate</exception>
        [Category("TimeSheet")]
        [Browsable(false)]
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

        [Category("TimeSheet")]
        public int DayCount
        {
            get { return (_toDate - _fromDate).Days + 1; }
        }

        [Category("TimeSheet")]
        public int ColumnHeaderCount
        {
            get { return 2; }
        }

        private string _headerDateFormat;

        [Category("TimeSheet")]
        [DisplayName("Header Date Format")]
        public string HeaderDateFormat
        {
            get { return _headerDateFormat; }
            set { _headerDateFormat = value; }
        }

        private ContentAlignment _positionShowToolTip;

        /// <summary>
        /// Gets or sets the position show tool tip.
        /// </summary>
        /// <value>
        /// The position show tool tip.
        /// </value>
        [Category("TimeSheet")]
        [DisplayName("PositionShowToolTip")]
        public ContentAlignment PositionShowToolTip
        {
            get { return _positionShowToolTip; }
            set { _positionShowToolTip = value; }
        }

        [Category("TimeSheet")]
        [Editor(typeof(GenericCollectionEditor), typeof(UITypeEditor))]
        [DisplayName("Catalog Colors")]
        public GenericCollectionBase<TimeSheetCatalogColor> CatalogColors { get; set; }

        [Category("TimeSheet")]
        [Editor(typeof(GenericCollectionEditor), typeof(UITypeEditor))]
        [DisplayName("Status Colors")]
        public GenericCollectionBase<TimeSheetStatusColor> StatusColors { get; set; }

        #endregion Properties

        public TimeSheetGridView()
        {
            string[] sa = this.GetType().Assembly.GetManifestResourceNames();

            foreach (string s in sa)
                System.Diagnostics.Trace.WriteLine(s);

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

            #endregion Default grid view

            // Init FromDate and ToDate
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;

            // Header Format
            _headerDateFormat = "ddd, dd/MM/yyyy";

            // Default Color
            DefaultColor();

            // Check position for Tooltip
            _positionShowToolTip = ContentAlignment.TopRight;

            // Init PopupToolTip
            popupToolTip = new PopupControl.Popup(commentToolTip = new CommentToolTip());
        }

        private void DefaultColor()
        {
            this.CatalogColors = new GenericCollectionBase<TimeSheetCatalogColor>();
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.None, Color.Empty));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.WorkingDay, Color.FromArgb(29, 43, 78)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.Holiday, Color.FromArgb(248, 210, 255)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.WeekendOff, Color.FromArgb(125, 125, 125)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.WeekendOffHalf, Color.FromArgb(227, 227, 227)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.Leave, Color.FromArgb(255, 249, 189)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.BusinessTrip, Color.FromArgb(30, 143, 151)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.Overtime, Color.FromArgb(246, 134, 134)));
            this.CatalogColors.Add(new TimeSheetCatalogColor(TimeSheetCatalog.Shift, Color.FromArgb(228, 249, 255)));

            this.StatusColors = new GenericCollectionBase<TimeSheetStatusColor>();
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetStatus.None, Color.Empty));
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetControl.TimeSheetStatus.InvalidTS, Color.FromArgb(255, 0, 0)));
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetControl.TimeSheetStatus.ValidTS, Color.FromArgb(0, 255, 0)));
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetControl.TimeSheetStatus.UnApprovedOT, Color.FromArgb(255, 0, 0)));
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetControl.TimeSheetStatus.ApprovedOT, Color.FromArgb(0, 255, 0)));
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetControl.TimeSheetStatus.ApprovedLeave, Color.FromArgb(0, 255, 0)));
            this.StatusColors.Add(new TimeSheetStatusColor(TimeSheetControl.TimeSheetStatus.Locked, Color.FromArgb(255, 0, 0)));
        }

        private void RenderGrid()
        {
            if (!this.DesignMode && this.DataSource != null)
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
                    DataGridViewTimeSheetColumn tsColumn = new DataGridViewTimeSheetColumn(idDate)
                    {
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

        #region Mouse event

        private PopupControl.Popup popupToolTip;
        private CommentToolTip commentToolTip;

        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseClick(e);

            if (e.ColumnIndex >= this.ColumnHeaderCount && e.RowIndex >= 0)
            {
                var tsDay = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as TimeSheetDay;

                if (tsDay != null)
                {
                    // show tool tip
                    if (e.Button == System.Windows.Forms.MouseButtons.Left && !popupToolTip.Visible)
                    {
                        // Only show tooltip when the mouse is in topright corner of cell
                        Rectangle rect = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        if (rect.CheckPointInCorner(e.X, e.Y, this.PositionShowToolTip))
                        {
                            commentToolTip.Title = tsDay.GetTitle();
                            commentToolTip.Content = tsDay.GetContent();
                            popupToolTip.Show(this, rect);
                        }
                    }// ToolTip
                }// Check CellValue is not null
            }
        }

        protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            popupToolTip.Hide();
            base.OnCellMouseLeave(e);
        }

        #endregion Mouse event

        #region Action on interface

        /// <summary>
        /// Copies to clip board.
        /// </summary>
        public void CopyToClipBoard()
        {
            if (this.SelectedCells.Count > 0)
            {
                TimeSheetDayCopiedArray copiedArray = new TimeSheetDayCopiedArray(this.SelectedCells);
                if (copiedArray.HasData)
                {
                    var copyDataObject = new DataObject(copiedArray);
                    Clipboard.SetDataObject(copyDataObject);
                }
            }
        }

        /// <summary>
        /// Pastes from clip board.
        /// </summary>
        public void PasteFromClipBoard(bool onlySelectedCells = true)
        {
            if (this.SelectedCells.Count > 0)
            {
                var clipboardDataObject = (DataObject)Clipboard.GetDataObject();
                if (clipboardDataObject.GetDataPresent(typeof(TimeSheetDayCopiedArray)))
                {
                    var clipboardTimeSheetDayArray = clipboardDataObject.GetData(typeof(TimeSheetDayCopiedArray)) as TimeSheetDayCopiedArray;
                    if (clipboardTimeSheetDayArray != null)
                    {                       

                        int minRow, minCol, maxRow, maxCol;
                        if (TimeSheetDayCopiedArray.FindArrayBound(this.SelectedCells,
                            out minRow, out minCol, out maxRow, out maxCol))
                        {
                            // Before pasting a array cell, control needs to notify user to handle other business
                            var cellArrangePastingEventArgs = new CellArrangePastingEventArgs(clipboardTimeSheetDayArray, this.Rows[minRow].Cells[minCol]);
                            OnCellArrayPasting(cellArrangePastingEventArgs);

                            if(cellArrangePastingEventArgs.Cancel == true)
                            {
                                Debug.WriteLine("User has just canceled pasting action");
                                return;
                            }
                            
                            // Only fill on selected cells
                            // Case 1: copied array is equal selected cells
                            // Case 2: copied array is inside selected cells
                            // Case 3: copied array is outside selected cells
                            int deltaRow, deltaCol;
                            if (onlySelectedCells)
                            {
                                deltaRow = maxRow - minRow + 1;
                                deltaCol = maxCol - minCol + 1;
                            }
                            else
                            {
                                // Only use a presented selected cell
                                // Case 1: enough range to fill from copied array
                                // Case 2: not enough range to fill from copied array

                                deltaRow = this.Rows.Count - minRow;
                                deltaCol = this.Columns.Count - minCol - this.ColumnHeaderCount;
                            }

                            // Get max bound
                            if (clipboardTimeSheetDayArray.MaxRow < deltaRow)
                                maxRow = minRow + clipboardTimeSheetDayArray.MaxRow;
                            if (clipboardTimeSheetDayArray.MaxCol < deltaCol)
                                maxCol = minCol + clipboardTimeSheetDayArray.MaxCol;

                            for (int i = minRow; i <= maxRow; i++)
                            {
                                for (int j = minCol; j <= maxCol; j++)
                                {
                                    var selectedCell = this.Rows[i].Cells[j];

                                    int rowIndex = i - minRow;
                                    int colIndex = j - minCol;

                                    // Check index inbound of copied array
                                    if (rowIndex < clipboardTimeSheetDayArray.MaxRow
                                        && colIndex < clipboardTimeSheetDayArray.MaxCol)
                                    {
                                        var clipboardValue = clipboardTimeSheetDayArray[rowIndex, colIndex];

                                        var cellPastingEventArgs = new CellPastingEventArgs(selectedCell, clipboardValue);

                                        // Perform before cell is pasted
                                        OnCellPasting(cellPastingEventArgs);
                                        if (!cellPastingEventArgs.Cancel)
                                        {
                                            CopyAndPasteCell(selectedCell, clipboardValue);

                                            // Perform after cell is pasted
                                            OnCellPasted(new CellPastedEventArgs(selectedCell));
                                        }
                                    }// Check index inbound of copied array
                                }
                            }
                            
                            
                        }
                    }
                }// Clipboard present TimeSheetDayCopiedArray
            } // Check SelectedCells
        }

        private static void CopyAndPasteCell(DataGridViewCell selectedCell, TimeSheetDay clipboarValue)
        {
            // Copy new
            TimeSheetDay selectedCellValue = selectedCell.Value as TimeSheetDay;
            var updateDay = DateTime.MinValue;

            if (selectedCellValue != null)
            {
                updateDay = selectedCellValue.Day;
            }
            else
            {
                var selectedColumn = selectedCell.OwningColumn as DataGridViewTimeSheetColumn;
                if (selectedColumn != null)
                    updateDay = selectedColumn.PresentDay;
            }

            // Paste new
            if (updateDay > DateTime.MinValue)
            {
                var newTimeSheetDay = new TimeSheetDay(clipboarValue);
                newTimeSheetDay.Day = updateDay;
                selectedCell.Value = newTimeSheetDay;
            }
        }

        #endregion Action on interface

        #region Events

        #region CellArray is pasting
        private static readonly object EVENT_CELLARRAYPASTING = new object();

        public event EventHandler<CellArrangePastingEventArgs> CellArrayPasting
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLARRAYPASTING, value);
            }

            remove
            {
                base.Events.RemoveHandler(EVENT_CELLARRAYPASTING, value);
            }
        }

        protected virtual void OnCellArrayPasting(CellArrangePastingEventArgs e)
        {
            var handler = base.Events[TimeSheetGridView.EVENT_CELLARRAYPASTING] as EventHandler<CellArrangePastingEventArgs>;
            if (handler != null && !base.IsDisposed)
            {
                handler(this, e);
            }
        } 
        #endregion

        #region Cell is Pasting

        private static readonly object EVENT_CELLPASTING = new object();

        /// <summary>
        /// Occurs when [cell pasting].
        /// </summary>
        public event EventHandler<CellPastingEventArgs> CellPasting
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLPASTING, value);
            }

            remove
            {
                base.Events.RemoveHandler(EVENT_CELLPASTING, value);
            }
        }

        protected virtual void OnCellPasting(CellPastingEventArgs e)
        {
            EventHandler<CellPastingEventArgs> handler = base.Events[TimeSheetGridView.EVENT_CELLPASTING] as EventHandler<CellPastingEventArgs>;
            if (handler != null && !base.IsDisposed)
            {
                handler(this, e);
            }
        }

        #endregion Cell is Pasting

        #region Cell is pasted

        private static readonly object EVENT_CELLPASTED = new object();

        /// <summary>
        /// Occurs when [cell pasted].
        /// </summary>
        public event EventHandler<CellPastedEventArgs> CellPasted
        {
            add
            {
                base.Events.AddHandler(EVENT_CELLPASTED, value);
            }

            remove
            {
                base.Events.RemoveHandler(EVENT_CELLPASTED, value);
            }
        }

        protected virtual void OnCellPasted(CellPastedEventArgs e)
        {
            EventHandler<CellPastedEventArgs> handler = base.Events[EVENT_CELLPASTED] as EventHandler<CellPastedEventArgs>;
            if (handler != null && !base.IsDisposed)
            {
                handler(this, e);
            }
        }

        #endregion Cell is pasted

        #endregion Events

        #region Function for getting color from setting

        public Color GetColorByTimeSheetCatalog(TimeSheetCatalog catalog)
        {
            if (this.CatalogColors != null && this.CatalogColors.Count > 0)
            {
                foreach (TimeSheetCatalogColor catColor in this.CatalogColors)
                {
                    if (catColor.Catalog == catalog)
                        return catColor.Color;
                }
            }

            return Color.Empty;
        }

        public Color GetColorByTimeSheetStatus(TimeSheetStatus status)
        {
            if (this.StatusColors != null && this.StatusColors.Count > 0)
            {
                foreach (TimeSheetStatusColor statusColor in this.StatusColors)
                {
                    if (statusColor.Status == status)
                        return statusColor.Color;
                }
            }

            return Color.Empty;
        }

        #endregion Function for getting color from setting

        #region Other supporting functions

        #endregion Other supporting functions
    }

    #region EventArgs

    public class CellArrangePastingEventArgs : EventArgs
    {
        public TimeSheetDayCopiedArray CopiedTimeSheetArray { get; set; }
        public DataGridViewCell PresentCell { get; set; }
        public bool Cancel { get; set; }

        public CellArrangePastingEventArgs()
        {
            Cancel = false;
        }

        public CellArrangePastingEventArgs(TimeSheetDayCopiedArray copiedArray, DataGridViewCell presentCell) 
            : this()
        {
            this.CopiedTimeSheetArray = copiedArray;
            this.PresentCell = presentCell;
        }
    }

    /// <summary>
    /// CellPastingEventArgs
    /// </summary>
    public class CellPastingEventArgs : EventArgs
    {
        public DataGridViewCell SelectedCell { get; set; }

        public TimeSheetDay NewValue { get; set; }

        public bool Cancel { get; set; }

        public CellPastingEventArgs()
        {
            this.Cancel = false;
        }

        public CellPastingEventArgs(DataGridViewCell selectedCell, TimeSheetDay newValue)
            : this()
        {
            this.SelectedCell = selectedCell;
            this.NewValue = newValue;
        }
    }

    /// <summary>
    /// CellPastedEventArgs
    /// </summary>
    public class CellPastedEventArgs : EventArgs
    {
        public DataGridViewCell SelectedCell { get; set; }

        public CellPastedEventArgs()
        {
        }

        public CellPastedEventArgs(DataGridViewCell selectedCell)
        {
            this.SelectedCell = selectedCell;
        }
    }

    #endregion EventArgs

    #region GUI editor

    public class GenericCollectionBase<T> : CollectionBase where T : class
    {
        public T this[int index]
        {
            get { return (T)List[index]; }
        }

        public void Add(T catColor)
        {
            List.Add(catColor);
        }

        public void Remove(T catColor)
        {
            List.Remove(catColor);
        }

        public int Count
        {
            get { return List.Count; }
        }
    }

    /// <summary>
    /// TimeSheetCatalogColor
    /// </summary>
    public class TimeSheetCatalogColor
    {
        public TimeSheetCatalog Catalog { get; set; }

        public Color Color { get; set; }

        public TimeSheetCatalogColor()
        {
        }

        public TimeSheetCatalogColor(TimeSheetCatalog catalog, Color color)
        {
            this.Catalog = catalog;
            this.Color = color;
        }

        public override string ToString()
        {
            return string.Format("{0} / {1}", this.Catalog, this.Color);
        }
    }

    /// <summary>
    /// TimeSheetStatusColor
    /// </summary>
    public class TimeSheetStatusColor
    {
        public TimeSheetStatus Status { get; set; }

        public Color Color { get; set; }

        public TimeSheetStatusColor()
        {
        }

        public TimeSheetStatusColor(TimeSheetStatus status, Color color)
        {
            this.Status = status;
            this.Color = color;
        }

        public override string ToString()
        {
            return string.Format("{0} / {1}", this.Status, this.Color);
        }
    }

    /// <summary>
    /// GenericCollectionEditor for PropertyGrid
    /// </summary>
    [ToolboxItem(false)]
    public class GenericCollectionEditor : System.ComponentModel.Design.CollectionEditor
    {
        public GenericCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override string GetDisplayText(object value)
        {
            return base.GetDisplayText(value.ToString());
        }
    }

    #endregion GUI editor

    /// <summary>
    /// TimeSheetDayCopiedArray is used for storing copied from gridview
    /// </summary>
    [Serializable]
    public class TimeSheetDayCopiedArray
    {
        public int MaxRow { get; private set; }

        public int MaxCol { get; private set; }

        private TimeSheetDay[,] _timeSheetDayArray;

        public TimeSheetDayCopiedArray()
        {
            this.MaxRow = 0;
            this.MaxCol = 0;
        }

        public TimeSheetDayCopiedArray(DataGridViewSelectedCellCollection selectedCells)
            : this()
        {
            CopyFrom(selectedCells);
        }

        public void CopyFrom(DataGridViewSelectedCellCollection selectedCells)
        {
            int minRow = int.MaxValue;
            int minCol = int.MaxValue;
            int maxRow = int.MinValue;
            int maxCol = int.MinValue;

            if (FindArrayBound(selectedCells, out minRow, out minCol, out maxRow, out maxCol))
            {
                Debug.WriteLine("({0}, {1}), ({2}, {3})", minRow, minCol, maxRow, maxCol);

                // Delta
                int deltaRow = maxRow - minRow + 1;
                int deltaCol = maxCol - minCol + 1;

                // Adjust index
                if (deltaRow > 0 && deltaCol > 0)
                {
                    MaxRow = deltaRow;
                    MaxCol = deltaCol;
                }

                // New copy
                _timeSheetDayArray = new TimeSheetDay[MaxRow, MaxCol];

                for (int i = 0; i < selectedCells.Count; i++)
                {
                    DataGridViewCell selectedCell = selectedCells[i];
                    int ri = selectedCell.RowIndex - minRow;
                    int ci = selectedCell.ColumnIndex - minCol;
                    _timeSheetDayArray[ri, ci] = selectedCell.Value as TimeSheetDay;
                }

                Debug.WriteLine(_timeSheetDayArray.Print(ts => ts == null ? "(null)" : ts.Day.ToString("dd/MM")));
            }
        }

        public TimeSheetDay this[int rowIndex, int colIndex]
        {
            get
            {
                try
                {
                    return _timeSheetDayArray[rowIndex, colIndex];
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool HasData
        {
            get
            {
                return Count > 0;
            }
        }

        public int Count
        {
            get
            {
                return MaxCol * MaxRow;
            }
        }

        public static bool FindArrayBound(DataGridViewSelectedCellCollection selectedCells,
            out int minRow, out int minCol, out int maxRow, out int maxCol)
        {
            minRow = int.MaxValue;
            minCol = int.MaxValue;
            maxRow = int.MinValue;
            maxCol = int.MinValue;

            if (selectedCells != null && selectedCells.Count > 0)
            {
                for (int i = 0; i < selectedCells.Count; i++)
                {
                    DataGridViewCell selectedCell = selectedCells[i];

                    if (selectedCell.RowIndex < minRow)
                        minRow = selectedCell.RowIndex;
                    if (selectedCell.RowIndex > maxRow)
                        maxRow = selectedCell.RowIndex;
                    if (selectedCell.ColumnIndex < minCol)
                        minCol = selectedCell.ColumnIndex;
                    if (selectedCell.ColumnIndex > maxCol)
                        maxCol = selectedCell.ColumnIndex;
                }

                // Throw exception if selectedcell is not serial
                return (maxRow - minRow + 1) * (maxCol - minCol + 1) == selectedCells.Count;
            }

            return false;
        }
    }
}