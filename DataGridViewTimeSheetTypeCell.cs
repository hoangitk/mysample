using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTimeSheetTypeCell : DataGridViewTextBoxCell
    {
        public DataGridViewTimeSheetTypeCell() : base()
        {

        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewTimeSheetTypeEditingControl editingControl =
                this.DataGridView.EditingControl as DataGridViewTimeSheetTypeEditingControl;

            if (this.Value == null)
            {
                editingControl.Value = this.DefaultNewRowValue as TimeSheetType;
            }
            else
            {
                editingControl.Value = this.Value as TimeSheetType;
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewTimeSheetTypeEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(TimeSheetType);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return new TimeSheetType();
            }
        }
    }
}
