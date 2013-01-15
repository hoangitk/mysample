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
			get { return TimeSheetDay.Empty; }
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
                if (data != null && data != TimeSheetDay.Empty)
                {
                    Color catColor = this.OwnTimeSheetGridView.GetColorByTimeSheetCatalog(data.Catalog);
                    Color statusColor = this.OwnTimeSheetGridView.GetColorByTimeSheetStatus(data.Status);
                    Renderer.DrawBox(g, rect, catColor, statusColor, 1, DashStyle.Solid);
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
                        Renderer.DrawBoxWithText(graphics, barRect, color, true, plannedItem.TimeSheetType.Code, this.DataGridView.DefaultCellStyle.Font, ContentAlignment.MiddleCenter);

                        // Draw status
                        Color statusColor = this.OwnTimeSheetGridView.GetColorByTimeSheetStatus(plannedItem.Status);
                        Renderer.DrawStatusIcon(graphics, barRect, statusColor);
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
                        Renderer.DrawBoxWithText(graphics, barRect, color, true, realtimeItem.TimeSheetType.Code, this.DataGridView.DefaultCellStyle.Font, ContentAlignment.MiddleCenter);
                        
                    }
                }

                #endregion
            }
        }// Draw        
	}   
}
