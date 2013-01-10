using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeSheetControl;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += (s, e) =>
            {
                var timeSheetItemDataSource = SampleData.Default.GenerateTimeSheetItemsBindingList(
                    DateTime.Now.AddDays(-15), DateTime.Now.AddDays(15));

                this.dataGridView1.DataSource = timeSheetItemDataSource;
            };

            this.dataGridView1.CellContentDoubleClick += (s, e) =>
            {
                var selectedCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                Debug.WriteLine(selectedCell.Value);
            };
        }
    }
}
