using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeSheetControl;

namespace TimeSheetDemo
{
    public partial class TimeSheetEditorForm : Form
    {
        private TimeSheetDay _data;

        public TimeSheetDay Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public TimeSheetEditorForm(TimeSheetDay tsDay)
        {
            InitializeComponent();

            _data = tsDay;

            
            this.btnUpdate.Click += OnButton_Click;
            this.btnCancel.Click += OnButton_Click;

            this.Load += OnForm_Load;
        }

        private void OnForm_Load(object sender, EventArgs e)
        {
            this.shiftItemsDataGridView.CellBeginEdit += ShiftItemsDataGridView_CellBeginEdit;
            this.shiftItemsDataGridView.CellEndEdit += ShiftItemsDataGridView_CellEndEdit;
            this.shiftItemsDataGridView.CellValidating += ShiftItemsDataGridView_CellValidating;
            this.shiftItemsDataGridView.CellValidated += shiftItemsDataGridView_CellValidated;
            this.shiftItemsDataGridView.RowValidating += ShiftItemsDataGridView_RowValidating;

            var catalogDataSource = MethodHelper.EnumToListKeyValuePair<TimeSheetCatalog>();
            var statusDataSource = MethodHelper.EnumToListKeyValuePair<TimeSheetStatus>();
            var timeSheetTypeDataSource = new List<TimeSheetType>();
            timeSheetTypeDataSource.Add(default(TimeSheetType));
            timeSheetTypeDataSource.AddRange(SampleData.Default.SampleTimeSheetTypeList);            

            this.catalogComboBox.DataSource = catalogDataSource;
            this.catalogComboBox.DisplayMember = "Key";
            this.catalogComboBox.ValueMember = "Value";

            this.statusComboBox.DataSource = statusDataSource;
            this.statusComboBox.DisplayMember = "Key";
            this.statusComboBox.ValueMember = "Value";

            this.shiftStatusColumn.DataSource = statusDataSource;
            this.shiftStatusColumn.DisplayMember = "Key";
            this.shiftStatusColumn.ValueMember = "Value";

            this.leaveStatusColumn.DataSource = statusDataSource;
            this.leaveStatusColumn.DisplayMember = "Key";
            this.leaveStatusColumn.ValueMember = "Value";

            this.shiftTimeSheetTypeColumn.DataSource = timeSheetTypeDataSource.ToIListKeyValuePair(t => t == default(TimeSheetType) ? "(null)" : t.ToString());
            //.Where(t =>
            //    t == default(TimeSheetType) ||
            //    t.Catalog == TimeSheetCatalog.Shift ||
            //    t.Catalog == TimeSheetCatalog.Overtime)
            //    .ToIListKeyValuePair(t => t == default(TimeSheetType) ? "(null)" : t.ToString());
            this.shiftTimeSheetTypeColumn.DisplayMember = "Key";
            this.shiftTimeSheetTypeColumn.ValueMember = "Value";

            //this.leaveTimeSheetTypeColumn.DataSource = timeSheetTypeDataSource.ToIListKeyValuePair(t => t == default(TimeSheetType) ? "(null)" : t.ToString());
            //                                            //.Where(t =>
            //                                            //    t == default(TimeSheetType) ||
            //                                            //    t.Catalog == TimeSheetCatalog.Leave)
            //                                            //.ToIListKeyValuePair(t => t == default(TimeSheetType) ? "(null)" : t.ToString());
            //this.leaveTimeSheetTypeColumn.DisplayMember = "Key";
            //this.leaveTimeSheetTypeColumn.ValueMember = "Value";

            this.timeSheetDayBindingSource.DataSource = _data;
        }

        void ShiftItemsDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.shiftItemsDataGridView.IsCurrentRowDirty)
            {
                System.Windows.Forms.DataGridViewRow editingRow = this.shiftItemsDataGridView.Rows[e.RowIndex];
                

                TimeSheetRecordValidation tsValidation = new TimeSheetRecordValidation();
                var tsRecord = editingRow.DataBoundItem as ShiftRecord;

                if (tsRecord != null)
                {
                    var validResult = tsValidation.Validate(tsRecord);

                    if (validResult.IsValid)
                    {
                        editingRow.ErrorText = string.Empty;
                    }
                    else
                    {
                        editingRow.ErrorText = validResult.Errors.ToJoinString(Environment.NewLine);
                    }
                }
            }
        }

        void ShiftItemsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //this.shiftItemsDataGridView.EndEdit(); // Trick to update changed_value to cell.Value
            
            // No cell validation for new rows. New rows are validated on Row Validation.
            if (this.shiftItemsDataGridView.Rows[e.RowIndex].IsNewRow) { return; }

            var editingCell = this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            editingCell.ErrorText = "";

            if (this.shiftItemsDataGridView.IsCurrentCellDirty)
            {
                if (e.FormattedValue == null)
                {
                    editingCell.ErrorText = "Value is null";
                }
            }
        }

        void shiftItemsDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            var editingCell = this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Debug.WriteLine(editingCell.Value);
        }

        void ShiftItemsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == this.shiftItemsDataGridView.NewRowIndex)
            {
                this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[0].Value = this.dayDateTimePicker.Value;
                this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[1].Value = this.dayDateTimePicker.Value;
            }
        }

        void ShiftItemsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
            //this.shiftItemsDataGridView.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void OnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
