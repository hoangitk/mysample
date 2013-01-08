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

        //public override void PositionEditingControl(bool setLocation, bool setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        //{
        //    var loc = cellBounds.Location;
        //    loc.Y += cellBounds.Height;
        //    var size = new System.Drawing.Size(this.DataGridView.EditingControl.Width, this.DataGridView.EditingControl.Height); 
        //    var rect = new System.Drawing.Rectangle(loc, size);

        //    base.PositionEditingControl(setLocation, setSize, rect, rect, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        //}

        public override System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            var loc = cellBounds.Location;
            loc.Y += cellBounds.Height;
            var size = new System.Drawing.Size(this.DataGridView.EditingControl.Width, this.DataGridView.EditingControl.Height);
            var rect = new System.Drawing.Rectangle(loc, size);

            return base.PositionEditingPanel(rect, rect, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        }
    }
}
