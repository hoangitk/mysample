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
                    return Color.FromArgb(255, 0, 0);

                case TimeSheetControl.TimeSheetStatus.ValidTS:
                    return Color.FromArgb(0, 255, 0);

                case TimeSheetControl.TimeSheetStatus.UnApprovedOT:
                    return Color.FromArgb(255, 0, 0);

                case TimeSheetControl.TimeSheetStatus.ApprovedOT:
                    return Color.FromArgb(0, 255, 0);

                case TimeSheetControl.TimeSheetStatus.ApprovedLeave:
                    return Color.FromArgb(0, 255, 0);

                case TimeSheetControl.TimeSheetStatus.Locked:
                    return Color.FromArgb(255, 0, 0);
                default:
                    return Color.Transparent;
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


        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="text">The text.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="textAlign">The text align.</param>
        public static void DrawString(Graphics graphics, string text, Rectangle rect, Font font, Brush brush, ContentAlignment textAlign)
        {
            Size textSize = DataGridViewCell.MeasureTextSize(graphics, text, font, TextFormatFlags.SingleLine);

            float x = rect.X + 3;
            float y = rect.Y;

            switch (textAlign)
            {
                case ContentAlignment.BottomCenter:
                    x += (rect.Width - textSize.Width) / 2;
                    y += rect.Height - textSize.Height;
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
        /// Draws the time sheet bar.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="boxRect">The box rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="drawBorder">if set to <c>true</c> [draw border].</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="textAlign">The text align.</param>
        public static void DrawTimeSheetBar(Graphics graphics, Rectangle boxRect, Color color, bool drawBorder, string text, Font font, ContentAlignment textAlign)
        {
            // Draw bar
            TimeSheetRender.DrawBox(graphics, boxRect, color, true, ControlPaint.Dark(color), true);

            // Text
            if (!string.IsNullOrEmpty(text))
            {
                using (Brush textBrush = new SolidBrush(TimeSheetRender.InvertColor(color)))
                {
                    TimeSheetRender.DrawString(graphics, text, boxRect, font, textBrush, textAlign);
                }
            }

        }

        /// <summary>
        /// Draws the box.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="boxRect">The box rect.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="drawBackground">if set to <c>true</c> [draw background].</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <param name="drawBorder">if set to <c>true</c> [draw border].</param>
        public static void DrawBox(Graphics graphics, Rectangle boxRect,
            Color backgroundColor, bool drawBackground,
            Color borderColor, bool drawBorder)
        {
            // Fill background
            if (drawBackground)
            {
                using (Brush fillBrush = new SolidBrush(backgroundColor))
                {
                    graphics.FillRectangle(fillBrush, boxRect);
                } 
            }

            // Border
            if (drawBorder)
            {
                using (Pen borderPen = new Pen(ControlPaint.DarkDark(borderColor)))
                {
                    graphics.DrawRectangle(borderPen, boxRect);
                }
            }
        }

        /// <summary>
        /// Draws the icon.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="image">The image.</param>
        /// <param name="iconAlign">The icon align.</param>
        public static void DrawImage(Graphics graphics, Rectangle rect, Image image, ContentAlignment iconAlign)
        {
            if (image != null)
            {
                // Draw the image clipped to the cell.
                System.Drawing.Drawing2D.GraphicsContainer container =
                graphics.BeginContainer();

                graphics.SetClip(rect);

                int x = rect.X;
                int y = rect.Y;
                                
                switch (iconAlign)
                {
                    case ContentAlignment.BottomCenter:
                        x += (rect.Width - image.Width) / 2;
                        y += rect.Height - image.Height;
                        break;
                    case ContentAlignment.BottomLeft:
                        y += rect.Height - image.Height;
                        break;
                    case ContentAlignment.BottomRight:
                        x += rect.Width - image.Width;
                        y += rect.Height - image.Height;
                        break;
                    case ContentAlignment.MiddleCenter:
                        x += (rect.Width - image.Width) / 2;
                        y += (rect.Height - image.Height) / 2;
                        break;
                    case ContentAlignment.MiddleLeft:
                        y += (rect.Height - image.Height) / 2;
                        break;
                    case ContentAlignment.MiddleRight:
                        x += rect.Width - image.Width;
                        break;
                    case ContentAlignment.TopCenter:
                        x += (rect.Width - image.Width) / 2;
                        break;
                    case ContentAlignment.TopLeft:
                        break;
                    case ContentAlignment.TopRight:
                        x += rect.Width - image.Width;
                        break;
                    default:
                        break;
                }

                graphics.DrawImageUnscaled(image, x, y);                

                graphics.EndContainer(container);
            }
        }

        public static void DrawStatusIcon(Graphics graphics, Rectangle rect, Color color)
        {
            Bitmap commentIcon = new Bitmap(5, 5);

            using (Graphics g = Graphics.FromImage(commentIcon))
            {
                DrawSquareTriangle(g, new Rectangle(0, 0, commentIcon.Width, commentIcon.Height), 
                    color, Direction.LeftUp);
            }

            rect.X += 1;
            rect.Y += 1;

            DrawImage(graphics, rect, commentIcon, ContentAlignment.TopLeft);
        }

        /// <summary>
        /// Draws the triangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="direction">The direction.</param>
        public static void DrawTriangle(Graphics g, Rectangle rect, Color color, Direction direction)
        {
            int halfWidth = rect.Width / 2;
            int halfHeight = rect.Height / 2;
            Point p0 = Point.Empty;
            Point p1 = Point.Empty;
            Point p2 = Point.Empty;

            switch (direction)
            {
                case Direction.Up:
                    p0 = new Point(rect.Left + halfWidth, rect.Top);
                    p1 = new Point(rect.Left, rect.Bottom);
                    p2 = new Point(rect.Right, rect.Bottom);
                    break;
                case Direction.Down:
                    p0 = new Point(rect.Left + halfWidth, rect.Bottom);
                    p1 = new Point(rect.Left, rect.Top);
                    p2 = new Point(rect.Right, rect.Top);
                    break;
                case Direction.Left:
                    p0 = new Point(rect.Left, rect.Top + halfHeight);
                    p1 = new Point(rect.Right, rect.Top);
                    p2 = new Point(rect.Right, rect.Bottom);
                    break;
                case Direction.Right:
                    p0 = new Point(rect.Right, rect.Top + halfHeight);
                    p1 = new Point(rect.Left, rect.Bottom);
                    p2 = new Point(rect.Left, rect.Top);
                    break;
                default:
                    break;
            }

            using (Brush brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, new Point[] { p0, p1, p2 });
            }
        }

        /// <summary>
        /// Draws the square triangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// <param name="direction">The direction.</param>
        public static void DrawSquareTriangle(Graphics g, Rectangle rect, Color color, Direction direction)
        {           
            Point p0 = Point.Empty;
            Point p1 = Point.Empty;
            Point p2 = Point.Empty;

            switch (direction)
            {                
                case Direction.LeftDown:
                    p0 = new Point(rect.Left, rect.Top);
                    p1 = new Point(rect.Right, rect.Bottom);
                    p2 = new Point(rect.Left, rect.Bottom);
                    break;
                case Direction.LeftUp:
                    p0 = new Point(rect.Left, rect.Bottom);
                    p1 = new Point(rect.Left, rect.Top);
                    p2 = new Point(rect.Right, rect.Top);
                    break;
                case Direction.RightDown:
                    p0 = new Point(rect.Right, rect.Top);
                    p1 = new Point(rect.Right, rect.Bottom);
                    p2 = new Point(rect.Left, rect.Bottom);
                    break;
                case Direction.RightUp:
                    p0 = new Point(rect.Right, rect.Bottom);
                    p1 = new Point(rect.Left, rect.Top);
                    p2 = new Point(rect.Right, rect.Top);
                    break;
                default:
                    break;
            }

            using (Brush brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, new Point[] { p0, p1, p2 });
            }
        }
                
    }

    [Flags]
    public enum Direction
    {
        /// <summary>
        /// ▲
        /// </summary>
        Up = 1,
        /// <summary>
        /// ▼
        /// </summary>
        Down = 2,
        /// <summary>
        /// ◄
        /// </summary>
        Left = 4,        
        /// <summary>
        /// ►
        /// </summary>
        Right = 8,
        LeftDown = Left | Down,
        LeftUp = Left | Up,
        RightDown = Right | Down,
        RightUp = Right | Up
    }
}
