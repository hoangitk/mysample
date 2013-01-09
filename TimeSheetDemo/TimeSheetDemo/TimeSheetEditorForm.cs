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

            var catalogDataSource = ExtendMethodHelper.EnumToListKeyValuePair<TimeSheetCatalog>();
            var statusDataSource = ExtendMethodHelper.EnumToListKeyValuePair<TimeSheetStatus>();

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

            this.timeSheetDayBindingSource.DataSource = _data;

        }

        void ShiftItemsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == this.shiftItemsDataGridView.NewRowIndex)
            {
                this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[0].Value = this.dayDateTimePicker.Value;
                this.shiftItemsDataGridView.Rows[e.RowIndex].Cells[1].Value = this.dayDateTimePicker.Value;
            }
        }

        private void OnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
