using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public partial class TimeSheetTypeEditorForm : Form, IDataGridViewEditingControl
    {
        private TimeSheetType _value;

        public TimeSheetType Value
        {
            get { return _value; }
            set { _value = value; }
        }       

        DataGridView _dataGridView;
        int _rowIndex;
        bool _valueChanged = false;

        public event EventHandler ValueUpdated;
        protected virtual void OnValueUpdated()
        {
            EventHandler handler = ValueUpdated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public TimeSheetTypeEditorForm(TimeSheetType tsType)
        {
            InitializeComponent();

            var catalogDataSource = ExtendMethodHelper.EnumToList<TimeSheetCatalog>();
            this.catalogComboBox.DataSource = catalogDataSource;
            this.catalogComboBox.DisplayMember = "Key";
            this.catalogComboBox.ValueMember = "Value";

            this.Value = tsType;

            this.Load += (s, e) =>
            {
                this.timeSheetTypeBindingSource.DataSource = this.Value;
            };

            this.btnUpdate.Click += (s, e) =>
            {
                ClosePopup();

                _valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);

                OnValueUpdated();
            };
        }

        public TimeSheetTypeEditorForm() : this(new TimeSheetType())
        {

        }

        #region Unmanaged Code

        [DllImport("user32", CharSet = CharSet.Auto)]

        private extern static int SendMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]

        private extern static int PostMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

        private const int WM_ACTIVATE = 0x006;

        private const int WM_ACTIVATEAPP = 0x01C;

        private const int WM_NCACTIVATE = 0x086;

        [DllImport("user32")]

        private extern static void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;

        #endregion

        private bool popupShowing = false;

        public bool PopupShowing
        {
            get
            {
                return popupShowing;
            }

            set
            {
                popupShowing = value;
            }
        }

        public Form DataGridViewParentForm
        {
            get
            {
                return _dataGridView.TopLevelControl as Form;
            }
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (this.popupShowing)
            {
                // check for WM_ACTIVATE and WM_NCACTIVATE
                if (m.Msg == WM_NCACTIVATE)
                {
                    // Check if the title bar will active:
                    if (((int)m.WParam) == 1)
                    {
                        // If so reactivate it.
                        SendMessage(DataGridViewParentForm.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);

                        // Note it's no good to try and consume this message;
                        // if you try to do that you'll end up with windows
                        // that don't respond.
                    }
                }

                else if (m.Msg == WM_ACTIVATEAPP)
                {
                    // Check if the application is being deactivated.
                    if ((int)m.WParam == 0)
                    {
                        // It is so cancel the popup:
                        ClosePopup();

                        // And put the title bar into the inactive state:
                        PostMessage(DataGridViewParentForm.Handle, WM_NCACTIVATE, 0, IntPtr.Zero);
                    }
                }
            }
        }

        public void ClosePopup()
        {
            if (this.popupShowing)
            {
                this.popupShowing = false;

                this.Hide();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Tab)
            {
                Point pt = _dataGridView.CurrentCellAddress;

                //if this column is not the last column
                if ((keyData & Keys.Shift) == Keys.Shift)
                {
                    if (pt.X > 0)
                    {
                        _dataGridView.CurrentCell = _dataGridView[pt.X - 1, pt.Y];
                    }
                }
                else
                {
                    if (pt.X < _dataGridView.Columns.Count - 1)
                    {
                        _dataGridView.CurrentCell = _dataGridView[pt.X + 1, pt.Y];
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        #region IDataGridViewEditingControl
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {

        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return _dataGridView;
            }
            set
            {
                _dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToString();
            }
            set
            {                
                //this.Value = value as TimeSheetType;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                _rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return _valueChanged;
            }
            set
            {
                _valueChanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return !dataGridViewWantsInputKey;
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {

        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }
        #endregion
    }
}
