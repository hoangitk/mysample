using System.Drawing;
using System.Windows.Forms;
namespace TimeSheetControl
{
    public class DataGridViewTextAndImageColumn : DataGridViewTextBoxColumn
    {
        private Image imageValue;
        private Size imageSize;

        public DataGridViewTextAndImageColumn()
        {
            this.CellTemplate = new DataGridViewTextAndImageCell();
        }

        public override object Clone()
        {
            DataGridViewTextAndImageColumn c = base.Clone() as DataGridViewTextAndImageColumn;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get { return this.imageValue; }
            set
            {
                if (this.Image != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                            inheritedPadding.Top, inheritedPadding.Right,
                            inheritedPadding.Bottom);
                    }
                }
            }
        }

        private DataGridViewTextAndImageCell TextAndImageCellTemplate
        {
            get { return this.CellTemplate as DataGridViewTextAndImageCell; }
        }

        internal Size ImageSize
        {
            get { return imageSize; }
        }
    }
}