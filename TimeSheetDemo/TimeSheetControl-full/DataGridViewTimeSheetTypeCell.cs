using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTimeSheetTypeCell : DataGridViewImageCell
    {
        Form DataGridViewParentForm
        {
            get
            {
                return base.DataGridView.TopLevelControl as Form;
            }
        }        

        public DataGridViewTimeSheetTypeCell() : base()
        {           
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);            

            DataGridViewTimeSheetTypeEditingControl editingControl =
                this.DataGridView.EditingControl as DataGridViewTimeSheetTypeEditingControl;            

            if (this.Value == null)
            {
                editingControl.Value = this.DefaultNewRowValue as TimeSheetType;
            }
            else
            {
                editingControl.Value = this.Value as TimeSheetType;
            }

            //TimeSheetTypeEditorForm editingControl =
            //    this.DataGridView.EditingControl as TimeSheetTypeEditorForm;

            //if (editingControl.Parent == null)
            //{
            //    this.DataGridView.EditingControl.CausesValidation = this.DataGridView.CausesValidation;
            //    this.DataGridView.EditingPanel.CausesValidation = this.DataGridView.CausesValidation;

            //    if (this.Value == null)
            //    {
            //        editingControl.Value = this.DefaultNewRowValue as TimeSheetType;
            //    }
            //    else
            //    {
            //        editingControl.Value = this.Value as TimeSheetType;
            //    }

            //    Debug.WriteLine("Before: {0}", this.Value);

            //    this.DataGridViewParentForm.AddOwnedForm(editingControl);
            //    editingControl.StartPosition = FormStartPosition.Manual;
            //    editingControl.PopupShowing = true;
            //    editingControl.Show();
            //    editingControl.Activate();

            //    editingControl.ValueUpdated += editingControl_ValueUpdated;
            //}

        }
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (base.DataGridView != null)
            {
                Point point1 = base.DataGridView.CurrentCellAddress;
                if (((point1.X == e.ColumnIndex) && (point1.Y == e.RowIndex)) && (e.Button == MouseButtons.Left))
                {
                    if (base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
                    {
                        base.DataGridView.BeginEdit(true);
                    }
                }
            }
        }

        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)
        {
            //return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
            return formattedValue;
        }

        void editingControl_ValueUpdated(object sender, EventArgs e)
        {            
            Debug.WriteLine("After: {0}", this.Value);
        }

        //public override void DetachEditingControl()
        //{
        //    TimeSheetTypeEditorForm editingControl =
        //        this.DataGridView.EditingControl as TimeSheetTypeEditorForm;

        //    if (editingControl != null)
        //    {
        //        editingControl.Value = new TimeSheetType();
        //        editingControl.ValueUpdated -= editingControl_ValueUpdated;
        //        editingControl.Hide();
        //    }

        //    base.DetachEditingControl();
        //}

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewTimeSheetTypeEditingControl);
                //return typeof(TimeSheetTypeEditorForm);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(TimeSheetType);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return new TimeSheetType();
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(TimeSheetType);
            }       
        
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            Bitmap resultImage = new Bitmap(this.OwningColumn.Width, this.OwningRow.Height);

            using (Graphics g = Graphics.FromImage(resultImage))
            {
                var rect = new Rectangle(1, 1, resultImage.Width - 3, resultImage.Height - 3);

                TimeSheetType data = value as TimeSheetType;
                if (data != null)
                {                    
                    Renderer.DrawBoxWithText(g, rect, cellStyle.BackColor, false, data.ToString(), cellStyle.Font, ContentAlignment.MiddleLeft);
                }
            }

            return resultImage;
        }

        //public override void PositionEditingControl(bool setLocation, bool setSize, System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        //{
        //    var loc = cellBounds.Location;
        //    loc.Y += cellBounds.Height;
        //    var size = new System.Drawing.Size(this.DataGridView.EditingControl.Width, this.DataGridView.EditingControl.Height); 
        //    var rect = new System.Drawing.Rectangle(loc, size);

        //    base.PositionEditingControl(setLocation, setSize, rect, rect, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        //}

        public override System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            var loc = cellBounds.Location;
            loc.Y += cellBounds.Height;
            var size = new System.Drawing.Size(this.DataGridView.EditingControl.Width, this.DataGridView.EditingControl.Height);
            var rect = new System.Drawing.Rectangle(loc, size);

            return base.PositionEditingPanel(rect, rect, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        }

        //public override System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, System.Windows.Forms.DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        //{
        //    return base.PositionEditingPanel(cellBounds, cellClip, cellStyle, true, true, isFirstDisplayedColumn, isFirstDisplayedRow);
        //}

        //public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        //{
        //    Point p = base.DataGridView.CurrentCellAddress;

        //    Rectangle bounds = base.DataGridView.RectangleToScreen(base.DataGridView.GetCellDisplayRectangle(p.X, p.Y, true));
        //    bounds.Y += cellBounds.Height;
        //    base.DataGridView.EditingControl.Location = bounds.Location;
        //}

    }
}
