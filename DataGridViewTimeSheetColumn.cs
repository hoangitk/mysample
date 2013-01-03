using System;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTimeSheetColumn : DataGridViewColumn
    {
        private DataGridViewTimeSheetColumnHeaderCell headerCell = new DataGridViewTimeSheetColumnHeaderCell();

        public DataGridViewTimeSheetColumn()
            : base(new DataGridViewTimeSheetCell())
        {
            this.HeaderCell = headerCell;
        }

        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if (!(value is DataGridViewTimeSheetCell))
                    throw new InvalidCastException("CellTemplate must be DataGridViewTimeSheetCell");

                base.CellTemplate = value;
            }
        }
    }
}