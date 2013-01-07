using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            var catalogDataSource = SampleData.EnumToList<TimeSheetCatalog>();
            var statusDataSource = SampleData.EnumToList<TimeSheetStatus>();

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

            this.btnUpdate.Click += OnButtonClick;
            this.btnCancel.Click += OnButtonClick;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
