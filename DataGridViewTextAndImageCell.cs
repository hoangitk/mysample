using System.Drawing;
using System.Windows.Forms;

namespace TimeSheetControl
{
    public class DataGridViewTextAndImageCell : DataGridViewTextBoxCell
    {
        private Image _imageValue;
        private Size _imageSize;
        private ContentAlignment _imageAlign;

        public override object Clone()
        {
            DataGridViewTextAndImageCell c = base.Clone() as DataGridViewTextAndImageCell;
            c._imageValue = this._imageValue;
            c._imageSize = this._imageSize;
            c._imageAlign = this._imageAlign;

            return c;
        }

        public Image Image
        {
            get
            {
                if (this.OwningColumn == null ||
                    this.OwningTextAndImageColumn == null)
                {
                    return _imageValue;
                }
                else if (this._imageValue != null)
                {
                    return this._imageValue;
                }
                else
                {
                    return this.OwningTextAndImageColumn.Image;
                }
            }
            set
            {
                if (this._imageValue != value)
                {
                    this._imageValue = value;
                    this._imageSize = value.Size;

                    Padding inheritedPadding = this.InheritedStyle.Padding;
                    this.Style.Padding = new Padding(_imageSize.Width,
                        inheritedPadding.Top, inheritedPadding.Right,
                        inheritedPadding.Bottom);
                }
            }
        }

        public ContentAlignment ImageAlign
        {
            get
            {
                return _imageAlign;
            }

            set
            {
                _imageAlign = value;
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
            Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
            object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            Renderer.DrawImage(graphics, cellBounds, this.Image, this.ImageAlign);
        }

        private DataGridViewTextAndImageColumn OwningTextAndImageColumn
        {
            get { return this.OwningColumn as DataGridViewTextAndImageColumn; }
        }
    }
}