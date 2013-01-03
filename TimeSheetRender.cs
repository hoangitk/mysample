using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TimeSheetControl
{
    public class TimeSheetRender
    {
        public static readonly int MIN_HEADER_HEIGHT = 40;

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
    }
}
