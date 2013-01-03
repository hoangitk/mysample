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
        public static readonly int MIN_CELL_HEIGHT = 25;

        public static Color GetColor(TimeSheetCatalog tsType)
        {
            switch (tsType)
            {
                case TimeSheetCatalog.WorkingDay:
                    return Color.FromArgb(255, 255, 255);

                case TimeSheetCatalog.Holiday:
                    return Color.FromArgb(252, 213, 180);

                case TimeSheetCatalog.WeekendOff:
                    return Color.FromArgb(255, 190, 0);

                case TimeSheetCatalog.WeekendOffHalf:
                    return Color.FromArgb(178, 161, 199);

                case TimeSheetCatalog.Leave:
                    return Color.FromArgb(0, 112, 192);

                case TimeSheetCatalog.BusinessTrip:
                    return Color.FromArgb(165, 165, 165);

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

        public static void DrawTimeSheetDay(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle cellStyle, TimeSheetDay data)
        {            
            if (data != null)
            {
                float rate = cellBounds.Width / 24;

                // Draw Shift
                if (data.PlannedItems != null && data.PlannedItems.Count > 0)
                {
                    foreach (var shift in data.PlannedItems)
                    {
                        int barX = (int)(shift.FromTime.Hour * rate);
                        int barY = 0;
                        int barWidth = (int)(shift.TotalHours() * rate);
                        int barHeight = (cellBounds.Height - 2) / 3 * 2;
                        Rectangle shiftBar = new Rectangle(cellBounds.X + barX, cellBounds.Y + barY, barWidth, barHeight);

                        // Draw timeline bar
                        Color backColor = TimeSheetRender.GetColor(shift.TimeSheetType.Catalog);
                        using (Brush fillBrush = new SolidBrush(backColor))
                        {
                            graphics.FillRectangle(fillBrush, shiftBar);
                        }

                        // Draw border
                        graphics.DrawRectangle(new Pen(ControlPaint.Dark(backColor)), shiftBar);

                        // Draw Code
                        int textWidth = DataGridViewCell.MeasureTextWidth(graphics, shift.TimeSheetType.Code, cellStyle.Font, shiftBar.Height, TextFormatFlags.SingleLine);
                        using (Brush textBrush = new SolidBrush(TimeSheetRender.InvertColor(backColor)))
                        {
                            graphics.DrawString(shift.TimeSheetType.Code, cellStyle.Font, textBrush,
                                shiftBar.X + ((shiftBar.Width - textWidth) / 2), shiftBar.Y);
                        }
                    }
                }
            }
        }
    }
}
