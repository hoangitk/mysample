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
		
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, 
		                              int rowIndex, DataGridViewElementStates elementState, 
		                              object value, object formattedValue, string errorText, 
		                              DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
		                              DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
			paintParts);	
			
			
			TimeSheetDay data = value as TimeSheetDay;
			if(data != null)
			{			
				// Draw background
				using (Brush fillBrush = new SolidBrush(TimeSheetRender.GetColor(data.TimeSheetType)))
				{
					Rectangle backgroundRec = cellBounds;
					backgroundRec.Width -= 2;
					backgroundRec.Height -= 2;
					graphics.FillRectangle(fillBrush, backgroundRec);
				}
				
				float rate = cellBounds.Width / 24;
				
                // Draw Shift
				if(data.ShiftItems != null && data.ShiftItems.Count > 0)
				{
					foreach (var shift in data.ShiftItems) 
					{
						int barWidth = (int)((shift.ToTime - shift.FromTime).Hours * rate);
						int barX = (int)(shift.FromTime.Hour * rate);
						int barY = 0;
						int barHeight = (cellBounds.Height-2)/3*2;
						Rectangle shiftBar = cellBounds;
						shiftBar.X += barX;
						shiftBar.Y += barY;
						shiftBar.Height = barHeight;
						shiftBar.Width = barWidth;

                        // Draw timeline bar
                        Color backColor = TimeSheetRender.GetColor(shift.TimeSheet.Catalog);
                        using (Brush fillBrush = new SolidBrush(backColor)) 
						{
							graphics.FillRectangle(fillBrush, shiftBar);
						}

                        // Draw border
                        graphics.DrawRectangle(new Pen(ControlPaint.Dark(backColor)), shiftBar);

                        // Draw Code
                        int textWidth = MeasureTextWidth(graphics, shift.TimeSheet.Code, cellStyle.Font, shiftBar.Height, TextFormatFlags.SingleLine);
                        using(Brush textBrush = new SolidBrush(TimeSheetRender.InvertColor(backColor)))
                        {
                            graphics.DrawString(shift.TimeSheet.Code, cellStyle.Font, textBrush, 
                                shiftBar.X + ((shiftBar.Width - textWidth) / 2), shiftBar.Y);
                        }
					}
				}
			}
		}
		
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
	}
}
