using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cadena.WinForms;

namespace TimeSheetControl
{
    [ToolboxItem(false)]
    internal partial class DataGridViewTimeSheetTypeEditingControl : UserControl, IDataGridViewEditingControl
    {
        private TimeSheetType _value;

        public TimeSheetType Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
            }
        }

        DataGridView _dataGridView;
        int _rowIndex;
        bool _valueChanged = false;

        PopupControl.Popup _popup;
        TimeSheetTypeEditorControl tsEditor;

        public DataGridViewTimeSheetTypeEditingControl()
        {
            InitializeComponent();

            this.Value = new TimeSheetType();            
            
            _popup = new PopupControl.Popup(tsEditor = new TimeSheetTypeEditorControl())
            {
                AutoClose = false
            };

            tsEditor.Closed += (s, e) =>
            {
                _popup.Hide();
                
                _valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);                
            };

            this.Load += (s, e) =>
            {
                ShowEditor();
            };
        }

        private void ShowEditor()
        {
            var p = this.Location;
            p.Y += this.Height;
            tsEditor.Value = this.Value;
            _popup.Show(this, p);
        }

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
                return string.Empty;
            }
            set
            {

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
            return false;
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
    }
}
