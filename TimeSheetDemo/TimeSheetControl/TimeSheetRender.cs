using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class TimeSheetRender
    {
        public static readonly int MIN_HEADER_HEIGHT = 40;
        public static readonly int MIN_HEADER_WIDTH = 100;
        public static readonly int MIN_CELL_HEIGHT = 30;        

        #region Get Color
        public static Color GetColor(TimeSheetCatalog tsType)
        {
            switch (tsType)
            {
                case TimeSheetCatalog.WorkingDay:
                    return Color.FromArgb(165, 165, 165);

                case TimeSheetCatalog.Holiday:
                    return Color.FromArgb(252, 213, 180);

                case TimeSheetCatalog.WeekendOff:
                    return Color.FromArgb(255, 190, 0);

                case TimeSheetCatalog.WeekendOffHalf:
                    return Color.FromArgb(178, 161, 199);

                case TimeSheetCatalog.Leave:
                    return Color.FromArgb(0, 112, 192);

                case TimeSheetCatalog.BusinessTrip:
                    return Color.FromArgb(255, 255, 255);

                case TimeSheetCatalog.Overtime:
                    return Color.FromArgb(192, 0, 0);

                case TimeSheetCatalog.Shift:
                    return Color.FromArgb(182, 221, 232);

                default:
                    throw new Exception("Invalid value for TimeSheetType");
            }
        }

        public static Color GetColor(TimeSheetStatus tsStatus)
        {
            switch (tsStatus)
            {
                case TimeSheetControl.TimeSheetStatus.InvalidTS:
                    return Color.FromArgb(182, 221, 232);

                case TimeSheetControl.TimeSheetStatus.ValidTS:
                    return Color.FromArgb(182, 221, 232);

                case TimeSheetControl.TimeSheetStatus.UnApprovedOT:
                    return Color.FromArgb(182, 221, 232);

                case TimeSheetControl.TimeSheetStatus.ApprovedOT:
                    return Color.FromArgb(182, 221, 232);

                case TimeSheetControl.TimeSheetStatus.ApprovedLeave:
                    return Color.FromArgb(182, 221, 232);

                case TimeSheetControl.TimeSheetStatus.Locked:
                    return Color.FromArgb(182, 221, 232);
                default:
                    throw new Exception("Invalid value for TimeSheetStatus");
            }
        }

        public static Color InvertColor(Color color)
        {
            return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }
        #endregion

        /// <summary>
        /// Draws the string inside the specific rectangle.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="text">The text.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        public static void DrawString(Graphics graphics, string text, Rectangle rect, Font font, Brush brush)
        {
            TimeSheetRender.DrawString(graphics, text, rect, font, brush, ContentAlignment.MiddleCenter);
        }


        public static void DrawString(Graphics graphics, string text, Rectangle rect, Font font, Brush brush, ContentAlignment textAlign)
        {
            Size textSize = DataGridViewCell.MeasureTextSize(graphics, text, font, TextFormatFlags.SingleLine);

            float x = rect.X;
            float y = rect.Y;

            switch (textAlign)
            {
                case ContentAlignment.BottomCenter:
                    x += (rect.Width - textSize.Width) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    y += rect.Height - textSize.Height;
                    break;
                case ContentAlignment.BottomRight:
                    x += rect.Width - textSize.Width;
                    y += rect.Height - textSize.Height;
                    break;
                case ContentAlignment.MiddleCenter:
                    x += (rect.Width - textSize.Width) / 2;
                    y += (rect.Height - textSize.Height) / 2;
                    break;
                case ContentAlignment.MiddleLeft:
                    y += (rect.Height - textSize.Height) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    x += rect.Width - textSize.Width;
                    break;
                case ContentAlignment.TopCenter:
                    x += (rect.Width - textSize.Width) / 2;
                    break;
                case ContentAlignment.TopLeft:
                    break;
                case ContentAlignment.TopRight:
                    x += rect.Width - textSize.Width;
                    break;
                default:
                    break;
            }            

            graphics.DrawString(text, font, brush, x, y);
        }

        /// <summary>
        /// Draws the time sheet day.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="cellBounds">The cell bounds.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="data">The data.</param>
        public static void DrawTimeSheetDay(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle cellStyle, TimeSheetDay data)
        {            
            if (data != null)
            {
                float rate = cellBounds.Width / 24;

                // Draw Shift
                int plannedItemBarHeight = (cellBounds.Height - 4) / 2;
                int plannedItemBarWidth = 0;
                int plannedItemBarX = 0;
                int plannedItemBarY = 1;
                
                if (data.PlannedItems != null && data.PlannedItems.Count > 0)
                {
                    
                    foreach (var plannedItem in data.PlannedItems)
                    {
                        plannedItemBarX = (int)(plannedItem.FromTime.Hour * rate);                        
                        plannedItemBarWidth = (int)(plannedItem.TotalHours() * rate);
                        Rectangle barRect = new Rectangle(cellBounds.X + plannedItemBarX, cellBounds.Y + plannedItemBarY, 
                            plannedItemBarWidth, plannedItemBarHeight);

                        // Draw timeline bar
                        Color backColor = TimeSheetRender.GetColor(plannedItem.TimeSheetType.Catalog);

                        using (Brush fillBrush = new SolidBrush(backColor))
                        {
                            graphics.FillRectangle(fillBrush, barRect);
                        }

                        // Draw border
                        graphics.DrawRectangle(new Pen(ControlPaint.Dark(backColor)), barRect);

                        // Draw Code                        
                        using (Brush textBrush = new SolidBrush(TimeSheetRender.InvertColor(backColor)))
                        {
                            TimeSheetRender.DrawString(graphics, plannedItem.TimeSheetType.Code, barRect, cellStyle.Font, textBrush);
                        }
                    }
                }

                // Draw realtime                
                int realtimeItemBarHeight = (cellBounds.Height - 4) / 2;
                int realtimeItemBarWidth = 0;
                int realtimeItemBarX = 1;
                int realtimeItemBarY = 1 + realtimeItemBarHeight;

                if (data.RealTimeItems != null && data.RealTimeItems.Count > 0)
                {
                    foreach (var realtimeItem in data.RealTimeItems)
                    {
                        realtimeItemBarX = (int)(realtimeItem.FromTime.Hour * rate);
                        realtimeItemBarWidth = (int)(realtimeItem.TotalHours() * rate);
                        Rectangle barRect = new Rectangle(cellBounds.X + realtimeItemBarX, cellBounds.Y + realtimeItemBarY, 
                            realtimeItemBarWidth, realtimeItemBarHeight);

                        // Draw timeline bar
                        Color backColor = ControlPaint.Light(TimeSheetRender.GetColor(realtimeItem.TimeSheetType.Catalog));
                        using (Brush fillBrush = new SolidBrush(backColor))
                        {
                            graphics.FillRectangle(fillBrush, barRect);
                        }

                        // Draw border
                        graphics.DrawRectangle(new Pen(ControlPaint.Dark(backColor)), barRect);

                        // Draw Code
                        
                        using (Brush textBrush = new SolidBrush(TimeSheetRender.InvertColor(backColor)))
                        {
                            TimeSheetRender.DrawString(graphics, realtimeItem.TimeSheetType.Code, barRect, cellStyle.Font, textBrush);
                        }
                    }
                }
            }
        }
    }
}
