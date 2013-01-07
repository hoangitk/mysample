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

        private TimeSheetGridView OwnTimeSheetGridView
        {
            get { return this.DataGridView as TimeSheetGridView; }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            Bitmap resultImage = new Bitmap(this.OwningColumn.Width, this.OwningRow.Height);

            using (Graphics g = Graphics.FromImage(resultImage))
            {
                var rect = new Rectangle(1, 1, resultImage.Width - 3, resultImage.Height - 3);

                TimeSheetDay data = value as TimeSheetDay;
                if (data != null)
                {
                    Color catColor = this.OwnTimeSheetGridView.GetColorByTimeSheetCatalog(data.Catalog);
                    Color statusColor = this.OwnTimeSheetGridView.GetColorByTimeSheetStatus(data.Status);
                    Render.DrawBox(g, rect, catColor, statusColor, 1, DashStyle.Solid);
                }

            }

            return resultImage;
        }

        public virtual void Draw(Graphics graphics)
        {
            var data = this.Value as TimeSheetDay;
            var cellBounds = this.GetCellBoundRectangle();

            if (data != null && !cellBounds.IsEmpty)
            {
                float rate = cellBounds.Width / 24;

                #region Draw the first line

                int plannedItemBarHeight = (cellBounds.Height - 4) / 2;
                int plannedItemBarWidth = 0;
                int plannedItemBarX = 0;
                int plannedItemBarY = 1;

                if (data.ShiftItems != null && data.ShiftItems.Count > 0)
                {
                    foreach (var plannedItem in data.ShiftItems)
                    {
                        plannedItemBarX = (int)(plannedItem.FromTime.Hour * rate);
                        plannedItemBarWidth = (int)(plannedItem.TotalHours() * rate);
                        Rectangle barRect = new Rectangle(cellBounds.X + plannedItemBarX, cellBounds.Y + plannedItemBarY,
                            plannedItemBarWidth, plannedItemBarHeight);

                        // Draw timeline bar
                        Color color = this.OwnTimeSheetGridView.GetColorByTimeSheetCatalog(plannedItem.TimeSheetType.Catalog);
                        Render.DrawBoxWithText(graphics, barRect, color, true, plannedItem.TimeSheetType.Code, this.DataGridView.DefaultCellStyle.Font, ContentAlignment.MiddleCenter);

                        // Draw status
                        Color statusColor = this.OwnTimeSheetGridView.GetColorByTimeSheetStatus(plannedItem.Status);
                        Render.DrawStatusIcon(graphics, barRect, statusColor);
                    }
                }

                #endregion Draw the first line

                #region Draw the second line

                int realtimeItemBarHeight = (cellBounds.Height - 4) / 2;
                int realtimeItemBarWidth = 0;
                int realtimeItemBarX = 1;
                int realtimeItemBarY = 1 + realtimeItemBarHeight;

                if (data.LeaveItems != null && data.LeaveItems.Count > 0)
                {
                    foreach (var realtimeItem in data.LeaveItems)
                    {
                        realtimeItemBarX = (int)(realtimeItem.FromTime.Hour * rate);
                        realtimeItemBarWidth = (int)(realtimeItem.TotalHours() * rate);
                        Rectangle barRect = new Rectangle(cellBounds.X + realtimeItemBarX, cellBounds.Y + realtimeItemBarY,
                            realtimeItemBarWidth, realtimeItemBarHeight);

                        // Draw timeline bar
                        Color color = this.OwnTimeSheetGridView.GetColorByTimeSheetCatalog(realtimeItem.TimeSheetType.Catalog);
                        Render.DrawBoxWithText(graphics, barRect, color, true, realtimeItem.TimeSheetType.Code, this.DataGridView.DefaultCellStyle.Font, ContentAlignment.MiddleCenter);
                        
                    }
                }

                #endregion
            }
        }
	}
}
