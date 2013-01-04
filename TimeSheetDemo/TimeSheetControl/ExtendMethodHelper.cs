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

            if (tsDay.PlannedItems != null && tsDay.PlannedItems.Count > 0)
            {
                sb.AppendLine("Planned: ");
                for (int i = 0; i < tsDay.PlannedItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", tsDay.PlannedItems[i]);
                    sb.AppendLine();
                }
            }

            if (tsDay.RealTimeItems != null && tsDay.RealTimeItems.Count > 0)
            {
                sb.AppendLine("Real time:");
                for (int i = 0; i < tsDay.RealTimeItems.Count; i++)
                {
                    sb.AppendFormat("+ {0}", tsDay.RealTimeItems[i]);                   
                    sb.AppendLine();
                }
            }

            if (tsDay.Statuses != null && tsDay.Statuses.Count > 0)
            {
                sb.AppendLine("Status:");
                for (int i = 0; i < tsDay.Statuses.Count; i++)
                {
                    sb.AppendFormat("+ {0}", tsDay.Statuses[i]);
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
