using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public static class ExtendMethodHelper
    {
        /// <summary>
        /// Helper method to gets the title of a TimeSheetDay.
        /// Support for showing comment tooltip
        /// </summary>
        public static string GetTitle(this TimeSheetDay tsDay)
        {
            return string.Format("{0} [{1:yyyy/MM/dd}]", tsDay.Catalog, tsDay.Day);
        }

        /// <summary>
        /// Helper method to gets the content of a TimeSheetDay.
        /// Support for showing comment tooltip
        /// </summary>
        public static string GetContent(this TimeSheetDay tsDay)
        {
            StringBuilder sb = new StringBuilder();

            if (tsDay.ShiftItems != null && tsDay.ShiftItems.Count > 0)
            {
                sb.AppendLine("Planned: ");
                for (int i = 0; i < tsDay.ShiftItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", tsDay.ShiftItems[i]);
                    sb.AppendLine();
                }
            }

            if (tsDay.LeaveItems != null && tsDay.LeaveItems.Count > 0)
            {
                sb.AppendLine("Real time:");
                for (int i = 0; i < tsDay.LeaveItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", tsDay.LeaveItems[i]);                   
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public static Rectangle GetCellBoundRectangle(this DataGridViewCell cell)
        {
            var gridView = cell.DataGridView;
            if (gridView != null)
            {
                var cellRect = gridView.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, false);
                if (!cellRect.IsEmpty)
                {
                    var cellWidth = cell.Size.Width;
                    cellRect.X = (cellRect.X + cellRect.Width) - cellWidth;
                    cellRect.Width = cellWidth;
                }

                return cellRect;
            }

            return Rectangle.Empty;
        }

        public static bool CheckPointInCorner(this Rectangle rect, int x, int y, ContentAlignment aligment)
        {
            int deltaX = rect.Width / 3;
            int deltaY = rect.Height / 3;
            int x0 = 0;
            int y0 = 0;
            switch (aligment)
            {
                case ContentAlignment.BottomCenter:
                    return (x0 + deltaX) < x && x <= (x0 + deltaX * 2)
                        && (y0 + deltaY * 2) < y && y <= (y0 + rect.Height);
                case ContentAlignment.BottomLeft:
                    return (x0) < x && x <= (x0 + deltaX)
                        && (y0 + deltaY * 2) < y && y <= (y0 + rect.Height);
                case ContentAlignment.BottomRight:
                    return (x0 + deltaX * 2) < x && x <= (x0 + rect.Width)
                        && (y0 + deltaY * 2) < y && y <= (y0 + rect.Height);
                case ContentAlignment.MiddleCenter:
                    return (x0 + deltaX) < x && x <= (x0 + deltaX * 2)
                        && (y0 + deltaY) < y && y <= (y0 + deltaY * 2);
                case ContentAlignment.MiddleLeft:
                    return (x0) < x && x <= (x0 + deltaX * 2)
                        && (y0 + deltaY) < y && y <= (y0 + deltaY * 2);
                case ContentAlignment.MiddleRight:
                    return (x0+ deltaX * 2) < x && x <= (x0 + rect.Width)
                        && (y0 + deltaY) < y && y <= (y0 + deltaY * 2);
                case ContentAlignment.TopCenter:
                    return (x0 + deltaX) < x && x <= (x0 + deltaX * 2)
                        && (y0) < y && y <= (y0 + deltaY);
                case ContentAlignment.TopLeft:
                    return (x0) < x && x <= (x0 + deltaX * 2)
                        && (y0) < y && y <= (y0 + deltaY);
                case ContentAlignment.TopRight:
                    return (x0 + deltaX * 2) < x && x <= (y0 + rect.Width)
                        && (y0) < y && y <= (y0 + deltaY);
                default:
                    return false;
            }
        }
    }
}
