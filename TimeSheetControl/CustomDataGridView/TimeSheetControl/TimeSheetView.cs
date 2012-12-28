using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TimeSheetControl
{
    public partial class TimeSheetView : UserControl
    {
        private DateTime _fromDate;

        [Browsable(true)]
        [Category("TimeSheet")]
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                this.ToDate = _fromDate;

                this.tsHeader.FromDate = _fromDate;
                this.tsGrid.FromDate = _fromDate;
            }
        }

        private DateTime _toDate;

        [Browsable(true)]
        [Category("TimeSheet")]
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                int _dayCount = (_toDate - _fromDate).Days + 1;
                if (_dayCount <= 0)
                    throw new ArgumentOutOfRangeException("ToDate must be great than or equal FromDate");

                this.tsHeader.ToDate = _toDate;
                this.tsGrid.ToDate = _toDate;

                Invalidate();
            }
        }

        private int _cellWidth;

        [Browsable(true)]
        [Category("TimeSheet")]
        public int CellWidth
        {
            get { return _cellWidth; }
            set
            {
                _cellWidth = value;

                if (_cellWidth < Constants.MIN_CELL_WIDTH)
                    throw new ArgumentOutOfRangeException("CellWidth must be great than or equal " + Constants.MIN_CELL_WIDTH);

                this.tsHeader.CellWidth = _cellWidth;
                this.tsGrid.CellWidth = _cellWidth;

                Invalidate();
            }
        }

        private int _cellHeight;

        [Browsable(true)]
        [Category("TimeSheet")]
        public int CellHeight
        {
            get { return _cellHeight; }
            set { 
                _cellHeight = value;

                if (_cellHeight < Constants.MIN_CELL_HEIGHT)
                    throw new ArgumentOutOfRangeException("CellHeight must be greate than or equal " + Constants.MIN_CELL_HEIGHT);
                
                this.tsGrid.CellHeight = _cellHeight;

                Invalidate();
            }
        }

        private Color _lineColor;

        [Browsable(true)]
        [Category("TimeSheet")]
        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                _lineColor = value;

                this.tsHeader.LineColor = _lineColor;
                this.tsGrid.LineColor = _lineColor;

                Invalidate();
            }
        }

        
        private int _itemCount;

        [Browsable(true)]
        [Category("TimeSheet")]
        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                if (_itemCount < 0)
                    throw new ArgumentOutOfRangeException("ItemCount must be great than 0");
                               
                this.tsGrid.ItemCount = _itemCount;

                Invalidate();
            }
        }


        private Color _headerColor;

        [Browsable(true)]
        [Category("TimeSheet")]
        public Color HeaderColor
        {
            get { return _headerColor; }
            set
            {
                _headerColor = value;

                this.tsHeader.HeaderColor = _headerColor;

                Invalidate();
            }
        }

        private int _headerHeight;

        [Browsable(true)]
        [Category("TimeSheet")]
        public int HeaderHeight
        {
            get { return _headerHeight; }
            set
            {
                _headerHeight = value;

                if (_headerHeight < Constants.MIN_HEADER_HEIGHT)
                    throw new ArgumentOutOfRangeException("HeaderHeight must be great than or equal" + Constants.MIN_HEADER_HEIGHT);

                this.tsHeader.HeaderHeight = _headerHeight;

                Invalidate();
            }
        }


        public TimeSheetView()
        {
            InitializeComponent();

            this.tsGrid.Scroll += (s, e) =>
            {
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    this.tsHeader.HorizontalScroll.Value = e.NewValue;

                    Debug.WriteLine("Horizontall value: " + e.NewValue);
                }
            };
        }
    }
}
