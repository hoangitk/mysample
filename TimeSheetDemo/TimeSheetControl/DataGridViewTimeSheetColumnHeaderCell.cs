using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTimeSheetColumnHeaderCell : DataGridViewColumnHeaderCell
    {
        public DataGridViewTimeSheetColumnHeaderCell()
        {

        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, 
            System.Drawing.Rectangle cellBounds, int rowIndex, 
            DataGridViewElementStates dataGridViewElementState, 
            object value, object formattedValue, string errorText, 
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, 
            DataGridViewPaintParts paintParts)
        {
            cellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // Draw seperate line
            graphics.DrawLine(new Pen(cellStyle.ForeColor),
                cellBounds.X, cellBounds.Y + cellBounds.Height / 2,
                cellBounds.X + cellBounds.Width, cellBounds.Y + cellBounds.Height / 2);

            // Draw "00:00"
            graphics.DrawString("00:00", cellStyle.Font,
                new SolidBrush(cellStyle.ForeColor), cellBounds.X, cellBounds.Y + cellBounds.Height / 2 + 5);

            int len2300 = MeasureTextWidth(graphics, "23:00", cellStyle.Font, cellBounds.Height / 2, TextFormatFlags.SingleLine);
            // Draw "23:00"
            graphics.DrawString("23:00", cellStyle.Font,
                new SolidBrush(cellStyle.ForeColor), cellBounds.X + cellBounds.Width - len2300, cellBounds.Y + cellBounds.Height / 2 + 5);
        }
    }
}
