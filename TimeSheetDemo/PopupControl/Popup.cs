using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.Drawing.Drawing2D;
using VS = System.Windows.Forms.VisualStyles;
using System.Text.RegularExpressions;

namespace PopupControl
{
    public partial class Popup : ToolStripDropDown
    {
        private ToolStripControlHost _host;
        private Control _opener;
        private Popup _ownerPopup;
        private Popup _childPopup;

        // Note: use Control for custom popup
        private Control _content;
        public Control Content
        {
            get { return _content; }            
        }

        private bool _resizableTop;
        private bool _resizableLeft;

        private bool _resizable;
        public bool Resizable
        {
            get { return _resizable; }
            set { _resizable = value; }
        }

        private bool _nonInteractive;
        public bool NonInteractive
        {
            get { return _nonInteractive; }
            set
            {
                if (value != _nonInteractive)
                {
                    _nonInteractive = value;
                    if (IsHandleCreated) RecreateHandle();
                }
            }
        }

        private Size _minimumSize;

        [Category("Popup")]
        public new Size MinimumSize
        {
            get { return _minimumSize; }
            set { _minimumSize = value; }
        }

        private Size _maximumSize;

        [Category("Popup")]
        public new Size MaximumSize
        {
            get { return _maximumSize; }
            set { _maximumSize = value; }
        }

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= NativeMethods.WS_EX_NOACTIVATE;
                if (NonInteractive) cp.ExStyle |= NativeMethods.WS_EX_TRANSPARENT | NativeMethods.WS_EX_LAYERED | NativeMethods.WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        public Popup(Control content)
        {
            _content = content;
                                 
            InitializeComponent();
            
            AutoSize = true;
            DoubleBuffered = true;
            ResizeRedraw = true;
            _host = new ToolStripControlHost(Content);
            Padding = Margin = _host.Padding = _host.Margin = Padding.Empty;

            MinimumSize = _content.MinimumSize;
            _content.MinimumSize = _content.Size;
            //MaximumSize = _content.MaximumSize;
            //_content.MaximumSize = _content.Size;            
            Size = _content.Size;

            TabStop = _content.TabStop = true;
            Content.Location = Point.Empty;
            
            Items.Add(_host);

            this.Content.Disposed += delegate(object sender, EventArgs e)
            {
                _content = null;
                this.Dispose(true);
            };

            this.Content.RegionChanged += delegate(object sender, EventArgs e)
            {
                UpdateRegion();
            };

            UpdateRegion();
        }

        protected void UpdateRegion()
        {
            if (Region != null)
            {
                Region.Dispose();
                Region = null;
            }
            if (Content.Region != null)
            {
                Region = Content.Region.Clone();
            }
        }

        public void Show(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            this.Size = control.Size;
            Show(control, control.ClientRectangle);
        }

        public void Show(Rectangle area)
        {
            _resizableTop = _resizableLeft = false;
            Point location = new Point(area.Left, area.Top + area.Height);
            Rectangle screen = Screen.FromControl(this).WorkingArea;
            if (location.X + Size.Width > (screen.Left + screen.Width))
            {
                _resizableLeft = true;
                location.X = (screen.Left + screen.Width) - Size.Width;
            }
            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                _resizableTop = true;
                location.Y -= Size.Height + area.Height;
            }
            //location = control.PointToClient(location);
            Show(location, ToolStripDropDownDirection.BelowRight);
        }

        public void Show(Control control, Rectangle area)
        {
            this.Size = control.Size;

            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            SetOwnerItem(control);

            _resizableTop = _resizableLeft = false;
            Point location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
            Rectangle screen = Screen.FromControl(control).WorkingArea;
            if (location.X + Size.Width > (screen.Left + screen.Width))
            {
                _resizableLeft = true;
                location.X = (screen.Left + screen.Width) - Size.Width;
            }
            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                _resizableTop = true;
                location.Y -= Size.Height + area.Height;
            }
            location = control.PointToClient(location);
            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        private void SetOwnerItem(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (control is Popup)
            {
                Popup popupControl = control as Popup;
                _ownerPopup = popupControl;
                _ownerPopup._childPopup = this;
                OwnerItem = popupControl.Items[0];
                return;
            }
            else if (_opener == null)
            {
                _opener = control;
            }
            if (control.Parent != null)
            {
                SetOwnerItem(control.Parent);
            }
        }

        public static Size GetSizeOfTextOfControl(Control control)
        {
            Size textSize = new Size();

            if (string.IsNullOrEmpty(control.Text))
            {
                textSize = TextRenderer.MeasureText("", control.Font);
            }
            else
            {
                // split to multi-line
                string[] lines = Regex.Split(control.Text, "\r\n|\r|\n");

                if (lines.Length > 0)
                {
                    // get max length column
                    int max = 0;

                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (lines[i].Length > lines[max].Length)
                            max = i;
                    }

                    Size maxStringSize = TextRenderer.MeasureText(lines[max], control.Font);

                    textSize = new Size(maxStringSize.Width, maxStringSize.Height * lines.Length);
                }
            }

            return textSize;
        }

    }
}