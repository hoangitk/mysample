using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class Renderer
    {        
        #region Get Color
        
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
            Renderer.DrawString(graphics, text, rect, font, brush, ContentAlignment.MiddleCenter);
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
        public static void DrawBoxWithText(Graphics graphics, Rectangle boxRect, Color color, bool drawBorder, string text, Font font, ContentAlignment textAlign)
        {
            // Draw bar
            if (drawBorder)
                Renderer.DrawBox(graphics, boxRect, color, ControlPaint.Dark(color));
            else
                Renderer.DrawBox(graphics, boxRect, color, Color.Empty);

            // Text
            if (!string.IsNullOrEmpty(text))
            {
                using (Brush textBrush = new SolidBrush(Renderer.InvertColor(color)))
                {
                    Renderer.DrawString(graphics, text, boxRect, font, textBrush, textAlign);
                }
            }

        }
                
        public static void DrawBox(Graphics graphics, Rectangle boxRect, Color backgroundColor, Color borderColor)
        {
            DrawBox(graphics, boxRect, backgroundColor, borderColor, 1, DashStyle.Solid);
        }

        public static void DrawBox(Graphics graphics, Rectangle boxRect, Color backgroundColor, Color borderColor, float borderWidth, DashStyle dashStyle)
        {
            // Fill background
            if (backgroundColor != Color.Empty)
            {
                using (Brush fillBrush = new SolidBrush(backgroundColor))
                {
                    graphics.FillRectangle(fillBrush, boxRect);
                }
            }

            // Border
            if (borderColor != Color.Empty)
            {
                using (Pen borderPen = new Pen(borderColor, borderWidth))
                {
                    borderPen.DashStyle = dashStyle;
                    graphics.DrawRectangle(borderPen, boxRect);
                }
            }
        }


        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="image">The image.</param>
        /// <param name="imageAlign">The image align.</param>
        public static void DrawImage(Graphics graphics, Rectangle rect, Image image, ContentAlignment imageAlign)
        {
            if (image != null)
            {
                // Draw the image clipped to the cell.
                System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();

                graphics.SetClip(rect);

                int x = rect.X;
                int y = rect.Y;
                                
                switch (imageAlign)
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
            Bitmap commentIcon = new Bitmap(rect.Height / 2, rect.Height / 2);

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
