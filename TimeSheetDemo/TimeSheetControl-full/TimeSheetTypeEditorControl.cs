using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public partial class TimeSheetTypeEditorControl : UserControl
    {
        private TimeSheetType _value;

        public TimeSheetType Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public event EventHandler ValueUpdated;
        protected virtual void OnValueUpdated()
        {
            EventHandler handler = ValueUpdated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public TimeSheetTypeEditorControl(TimeSheetType tsType)
        {
            InitializeComponent();
            
            this.Value = tsType;            

            this.Load += (s, e) =>
            {
                var catalogDataSource = MethodHelper
                    .EnumToListKeyValuePair<TimeSheetCatalog>()
                    .Filter( kv => ((KeyValuePair<string, TimeSheetCatalog>)kv).Value == this.Value.Catalog);
                this.catalogComboBox.DataSource = catalogDataSource;
                this.catalogComboBox.DisplayMember = "Key";
                this.catalogComboBox.ValueMember = "Value";                        

                this.timeSheetTypeBindingSource.DataSource = this.Value;
            };

            this.btnUpdate.Click += (s, e) =>
            {
                OnValueUpdated();
                this.Hide();
            };
        }

        public TimeSheetTypeEditorControl() 
            : this(new TimeSheetType())
        {
        }
    }
}
