using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class TimeSheetGridHeader : ScrollableControl, System.ComponentModel.ISupportInitialize
    {
        protected bool IsInitialising { get; set; }

        private DateTime _fromDate;

        [Browsable(false)]
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { 
                _fromDate = value;                
                UpdateParameters();
            }
        }

        private DateTime _toDate;

        [Browsable(false)]
        public DateTime ToDate
        {
            get { return _toDate; }
            set {
                _toDate = value;
                UpdateParameters();
            }
        }

        private int _cellWidth;

        [Browsable(false)]
        public int CellWidth
        {
            get { return _cellWidth; }
            set { 
                _cellWidth = value;
                UpdateParameters();
            }
        }

        private int _headerHeight;

        [Browsable(false)]
        public int HeaderHeight
        {
            get { return _headerHeight; }
            set { 
                _headerHeight = value;
                this.Height = _headerHeight;
                UpdateParameters();
            }
        }

        private Color _headerColor;

        [Browsable(false)]
        public Color HeaderColor
        {
            get { return _headerColor; }
            set { 
                _headerColor = value;
                _lightColor = ControlPaint.Light(_headerColor);
                _darkColor = ControlPaint.Dark(_headerColor);
            }
        }

        private Color _lineColor;

        [Browsable(false)]
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        private int _dayCount;
        private int _height;
        private int _width;
        private Color _lightColor;
        private Color _darkColor;
        
        public TimeSheetGridHeader()
        {
            AutoScroll = false;            
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _fromDate = DateTime.MinValue;
            _toDate = DateTime.MinValue;
            _headerHeight = Constants.MIN_HEADER_HEIGHT;
            _cellWidth = Constants.MIN_CELL_WIDTH;
            this.HeaderColor = Constants.DEFAULT_HEADER_COLOR;
            this.LineColor = Constants.DEFAULT_LINE_COLOR;

            UpdateParameters();
        }

        private void UpdateParameters()
        {
            _dayCount = (_toDate - _fromDate).Days + 1;
            _height = this.HeaderHeight;
            _width = _dayCount * this.CellWidth;

            //this.HorizontalScroll.SmallChange = this.CellWidth;
            //this.HorizontalScroll.Maximum = _width;
            AutoScrollMinSize = new Size(_width, _height);

            Debug.WriteLine("Header width: " + _width);
            Debug.WriteLine("Horizontal width: " + HorizontalScroll.Maximum);
        }

        private void DrawColumnHeader(Graphics g)
        {   
            // Draw full background
            Rectangle columnHeaderBackground = new Rectangle(0, 0, _width, _height);
            using (Brush gradientBrush = new LinearGradientBrush(
                columnHeaderBackground, _lightColor, _darkColor, LinearGradientMode.Vertical))
            {
                g.FillRectangle(gradientBrush, columnHeaderBackground);
            }

            Pen headerPen = new Pen(this.LineColor);
            // Draw header
            int middleLine = _height / 2;
            // Draw vertical line
            for (int x = 0; x <= _width; x+= this.CellWidth)
            {
                g.DrawLine(headerPen, x, 0, x, _height);
            }

            // Draw horizontal line
            g.DrawLine(headerPen, 0, middleLine, _width, middleLine);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (_dayCount > 0)
            {
                Graphics g = pe.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Point pt = AutoScrollPosition;
                g.TranslateTransform(pt.X, pt.Y);

                DrawColumnHeader(g);
            }

            base.OnPaint(pe);
        }


        public void BeginInit()
        {
            IsInitialising = true;
            SuspendLayout();
        }

        public void EndInit()
        {
            IsInitialising = false;
            ResumeLayout(false);
        }
    }
}
