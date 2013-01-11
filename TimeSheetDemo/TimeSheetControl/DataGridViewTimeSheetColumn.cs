using System;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTimeSheetColumn : DataGridViewColumn
    {
        public DateTime PresentDay { get; private set; }

        private DataGridViewTimeSheetColumnHeaderCell headerCell = new DataGridViewTimeSheetColumnHeaderCell();

        public DataGridViewTimeSheetColumn(DateTime presentDay)
            : base(new DataGridViewTimeSheetCell())
        {
            this.HeaderCell = headerCell;            
            this.PresentDay = presentDay;
            this.Name = this.PresentDay.ToString(TimeSheetGridView.COLUMN_TIMESHEET_NAME_ID_FORMAT);
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