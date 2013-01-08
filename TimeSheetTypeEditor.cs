using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cadena.WinForms;

namespace TimeSheetControl
{
    public partial class TimeSheetTypeEditor : UserControl
    {
        private TimeSheetType _value;

        public TimeSheetType Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public TimeSheetTypeEditor()
        {
            InitializeComponent();

            this.Value = new TimeSheetType();

            var catalogDataSource = ExtendMethodHelper.EnumToList<TimeSheetCatalog>();
            this.catalogComboBox.DataSource = catalogDataSource;
            this.catalogComboBox.DisplayMember = "Key";
            this.catalogComboBox.ValueMember = "Value";            

            this.btnClose.Click += (s, e) =>
            {
                this.Hide();
            };

            this.Load += (s, e) =>
            {
                this.timeSheetTypeBindingSource.DataSource = this.Value;
            };
        }
    }
}
