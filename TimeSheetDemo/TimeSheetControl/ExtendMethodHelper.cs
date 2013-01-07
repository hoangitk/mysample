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
    }
}
