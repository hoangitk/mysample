/*
 * Created by SharpDevelop.
 * User: HoangITK
 * Date: 12/29/2012
 * Time: 12:02 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TimeSheetControl
{
	
	/// <summary>
	/// Description of DataGridViewTimeSheetCell.
	/// </summary>
	public class DataGridViewTimeSheetCell : DataGridViewImageCell
	{
		public DataGridViewTimeSheetCell() : base()
		{
		}

        //protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
        //                              int rowIndex, DataGridViewElementStates elementState,
        //                              object value, object formattedValue, string errorText,
        //                              DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
        //                              DataGridViewPaintParts paintParts)
        //{            
        //    TimeSheetDay data = value as TimeSheetDay;
        //    if (data != null)
        //    {
        //        // Draw background
        //        using (Brush fillBrush = new SolidBrush(TimeSheetRender.GetColor(data.Catalog)))
        //        {
        //            Rectangle backgroundRec = cellBounds;
        //            //backgroundRec.Width -= 2;
        //            //backgroundRec.Height -= 2;
        //            graphics.FillRectangle(fillBrush, backgroundRec);
        //        }

        //        using (Pen gridLinePen = new Pen(Color.Gray))
        //        {
        //            // Draw the grid lines (only the right and bottom lines; 
        //            // DataGridView takes care of the others).
        //            graphics.DrawLine(gridLinePen, cellBounds.Left,
        //                cellBounds.Bottom - 1, cellBounds.Right - 1,
        //                cellBounds.Bottom - 1);
        //            graphics.DrawLine(gridLinePen, cellBounds.Right - 1,
        //                cellBounds.Top, cellBounds.Right - 1,
        //                cellBounds.Bottom);
        //        }

        //        float rate = cellBounds.Width / 24;

        //        // Draw Shift
        //        if (data.PlannedItems != null && data.PlannedItems.Count > 0)
        //        {
        //            foreach (var shift in data.PlannedItems)
        //            {
        //                int barX = (int)(shift.FromTime.Hour * rate);
        //                int barY = 0;
        //                int barWidth = (int)(shift.TotalHours() * rate);
        //                int barHeight = (cellBounds.Height - 2) / 3 * 2;
        //                Rectangle shiftBar = new Rectangle(cellBounds.X + barX, cellBounds.Y + barY, barWidth, barHeight);

        //                // Draw timeline bar
        //                Color backColor = TimeSheetRender.GetColor(shift.TimeSheetType.Catalog);
        //                using (Brush fillBrush = new SolidBrush(backColor))
        //                {
        //                    graphics.FillRectangle(fillBrush, shiftBar);
        //                }

        //                // Draw border
        //                graphics.DrawRectangle(new Pen(ControlPaint.Dark(backColor)), shiftBar);

        //                // Draw Code
        //                int textWidth = MeasureTextWidth(graphics, shift.TimeSheetType.Code, cellStyle.Font, shiftBar.Height, TextFormatFlags.SingleLine);
        //                using (Brush textBrush = new SolidBrush(TimeSheetRender.InvertColor(backColor)))
        //                {
        //                    graphics.DrawString(shift.TimeSheetType.Code, cellStyle.Font, textBrush,
        //                        shiftBar.X + ((shiftBar.Width - textWidth) / 2), shiftBar.Y);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
        //        paintParts);
        //    }
        //}
		
		public override Type ValueType 
		{
			get { return typeof(TimeSheetDay); }
			set { base.ValueType = value; }
		}
		
		public override Type FormattedValueType 
		{
			get { return typeof(TimeSheetDay); }
		}
		
		public override object DefaultNewRowValue 
		{
			get { return default(TimeSheetDay); }
		}

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            //return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);

            Bitmap resultImage = new Bitmap(this.OwningColumn.Width,
                this.OwningRow.Height);

            using (Graphics g = Graphics.FromImage(resultImage))
            {
                var rect = new Rectangle(1, 1, resultImage.Width - 3, resultImage.Height - 3);

                TimeSheetDay data = value as TimeSheetDay;
                if (data != null)
                {
                    // Draw backgroup
                    using (SolidBrush bgBrush = new SolidBrush(TimeSheetRender.GetColor(data.Catalog)))
                    {
                        g.FillRectangle(bgBrush, rect);
                    }

                    // Draw a comment point in top-right corner
                    TimeSheetRender.DrawCommentIcon(g, rect);
                   
                    // set tooltip
                    this.ToolTipText = data.ToString();
                }

            }

            return resultImage;
        }
	}
}
