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
	public class DataGridViewTimeSheetColumn : DataGridViewColumn
	{
		public DataGridViewTimeSheetColumn()
			: base(new DataGridViewTimeSheetCell())
		{
			
		}
		
		public override DataGridViewCell CellTemplate 
		{
			get { return base.CellTemplate; }
			set 
			{
				if(!(value is DataGridViewTimeSheetCell))
					throw new InvalidCastException("CellTemplate must be DataGridViewTimeSheetCell");
				
				base.CellTemplate = value; 
			}
		}
	}
	
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
				using (Brush fillBrush = new SolidBrush(TimeSheetDay.GetColor(data.TimeSheetType)))
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
						int barWidth = (int)((shift.ToTime - shift.FromtTime).Hours * rate);
						int barX = (int)(shift.FromtTime.Hour * rate);
						int barY = 0;
						int barHeight = (cellBounds.Height-2)/2;
						Rectangle shiftBar = cellBounds;
						shiftBar.X += barX;
						shiftBar.Y += barY;
						shiftBar.Height = barHeight;
						shiftBar.Width = barWidth;
						
						using (Brush fillBrush = new SolidBrush(TimeSheetDay.GetColor(shift.TSType))) 
						{
							graphics.FillRectangle(fillBrush, shiftBar);
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
