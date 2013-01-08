using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTimeSheetTypeColumn : DataGridViewColumn
    {
        public DataGridViewTimeSheetTypeColumn()
            : base(new DataGridViewTimeSheetTypeCell())
        {

        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell. 
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewTimeSheetTypeCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewTimeSheetTypeCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
