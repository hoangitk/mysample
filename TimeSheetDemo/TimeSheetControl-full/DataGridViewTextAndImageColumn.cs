using System.Drawing;
using System.Windows.Forms;
namespace TimeSheetControl
{
    public class DataGridViewTextAndImageColumn : DataGridViewTextBoxColumn
    {
        private Image _imageValue;
        private Size _imageSize;
        private ContentAlignment _imageAlign;

        public DataGridViewTextAndImageColumn()
        {
            this.CellTemplate = new DataGridViewTextAndImageCell();
        }

        public override object Clone()
        {
            DataGridViewTextAndImageColumn c = base.Clone() as DataGridViewTextAndImageColumn;
            c._imageValue = this._imageValue;
            c._imageSize = this._imageSize;
            c._imageAlign = this._imageAlign;

            return c;
        }

        public Image Image
        {
            get { return this._imageValue; }
            set
            {
                if (this.Image != value)
                {
                    this._imageValue = value;
                    this._imageSize = value.Size;
                    this.TextAndImageCellTemplate.Image = this.Image;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(_imageSize.Width,
                            inheritedPadding.Top, inheritedPadding.Right,
                            inheritedPadding.Bottom);
                    }
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
                this.TextAndImageCellTemplate.ImageAlign = _imageAlign;
            }
        }

        private DataGridViewTextAndImageCell TextAndImageCellTemplate
        {
            get { return this.CellTemplate as DataGridViewTextAndImageCell; }
        }

        internal Size ImageSize
        {
            get { return _imageSize; }
        }
    }
}