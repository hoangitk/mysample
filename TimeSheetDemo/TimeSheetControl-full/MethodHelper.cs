using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public static class MethodHelper
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

        /// <summary>
        /// Extend method to get cell bound
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check a point (mouse) is in a corner of the rectangle or not
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="aligment"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Raise the PropertyChangedEvent if value is changed
        /// </summary>
        /// <param name="changedEventHandler"></param>
        /// <param name="expression"></param>
        public static void ChangeAndNotify<T>(this PropertyChangedEventHandler changedEventHandler, ref T field, T value, Expression<Func<T>> expression)
        {
            field = value;

            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                if (changedEventHandler == null)
                    return;

                var lambda = expression as LambdaExpression;

                MemberExpression memberExp;

                if (lambda.Body is UnaryExpression)
                {
                    var unaryExp = lambda.Body as UnaryExpression;
                    memberExp = unaryExp.Operand as MemberExpression;
                }
                else
                {
                    memberExp = lambda.Body as MemberExpression;
                }

                var constantExp = memberExp.Expression as ConstantExpression;
                var propertyName = memberExp.Member.Name;// as PropertyInfo;

                foreach (var dele in changedEventHandler.GetInvocationList())
                {
                    dele.DynamicInvoke(new object[] {
                        constantExp.Value,
                        new PropertyChangedEventArgs(propertyName)} 
                    );
                }
            }
        }

        /// <summary>
        /// Enums to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList EnumToListKeyValuePair<T>()
        {
            var results = new ArrayList();
            var values = (T[])Enum.GetValues(typeof(T));
            var names = Enum.GetNames(typeof(T));

            for (int i = 0; i < values.Length; i++)
            {
                results.Add(new KeyValuePair<string, T>(names[i], values[i]));
            }            

            return results;
        }

        /// <summary>
        /// Filters the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static IList Filter(this IList list, Predicate<object> condition)
        {
            if (list == null)
                return null;

            var results = new ArrayList();

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (condition(item))
                    results.Add(item);
            }

            return results;
        }

        /// <summary>
        /// To the list key value pair.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="getKey">The get key.</param>
        /// <returns></returns>
        public static IList ToIListKeyValuePair<T>(this IEnumerable<T> list, Func<T, string> getKey)
        {
            var results = new ArrayList();

            foreach (var item in list)
            {
                results.Add(new KeyValuePair<string, T>(getKey(item), item));
            }

            return results;
        }

        public static string ToJoinString<T>(this IList<T> list, string seperator, string format = "")
        {
            StringBuilder sb = new StringBuilder();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(format))
                    {
                        sb.Append(list[i]);
                    }
                    else
                    {
                        sb.AppendFormat(format, list[i]);
                    }
                }
            }

            return sb.ToString();
        }

        public static string Print<T>(this T[,] array, Func<T, string> formatOuput)
        {
            StringBuilder sb = new StringBuilder();
            int rm = array.GetUpperBound(0) + 1;
            int cm = array.Length / rm;

            for (int i = 0; i < rm; i++)
            {
                for (int j = 0; j < cm; j++)
                {
                    sb.Append(formatOuput(array[i, j]));
                    sb.Append("\t");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }        
    }
}
