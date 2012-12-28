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
    public partial class TimeSheetGrid : ScrollableControl, System.ComponentModel.ISupportInitialize
    {
        protected bool IsInitialising { get; set; }

        private DateTime _fromDate;

        [Browsable(false)]
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                UpdateParameters();
            }
        }

        private DateTime _toDate;

        [Browsable(false)]
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                UpdateParameters();
            }
        }

        private int _cellWidth;

        [Browsable(false)]
        public int CellWidth
        {
            get { return _cellWidth; }
            set
            {
                _cellWidth = value;
                UpdateParameters();
            }
        }

        private int _cellHeight;

        [Browsable(false)]
        public int CellHeight
        {
            get { return _cellHeight; }
            set { _cellHeight = value; }
        }


        private Color _lineColor;

        [Browsable(false)]
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        private int _itemCount;

        [Browsable(false)]
        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                UpdateParameters();
            }
        }  

        private int _dayCount;
        private int _height;
        private int _width;        

        public TimeSheetGrid()
        {
            AutoScroll = true;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Default values            
            _fromDate = DateTime.MinValue;
            _toDate = DateTime.MinValue;
            _cellWidth = Constants.MIN_CELL_WIDTH;
            this.LineColor = Constants.DEFAULT_LINE_COLOR;

            UpdateParameters();
        }

        private void UpdateParameters()
        {
            _dayCount = (_toDate - _fromDate).Days + 1;                        
            
            _height = this.ItemCount * this.CellHeight;
            _width = _dayCount * this.CellWidth;

            //HorizontalScroll.SmallChange = this.CellWidth;

            AutoScrollMinSize = new Size(_width, _height);

            Debug.WriteLine("Grid width: " + _width);
            Debug.WriteLine("Horizontal width: " + HorizontalScroll.Maximum);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (_dayCount > 0 && _height > 0)
            {
                Graphics g = pe.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Point pt = AutoScrollPosition;
                g.TranslateTransform(pt.X, pt.Y);

                DrawGrid(g);
            }

            base.OnPaint(pe);            
        }

        private void DrawGrid(Graphics g)
        {
            // Draw grid            
            Pen gridPen = new Pen(this.LineColor);

            // Draw vertical lines
            for (int x = 0; x <= _width; x += this.CellWidth)
            {
                g.DrawLine(gridPen, x, 0, x, _height);
            }

            // Draw horizontal lines
            for (int y = 0; y <= _height; y += this.CellHeight)
            {
                g.DrawLine(gridPen, 0, y, _width, y);
            }
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
