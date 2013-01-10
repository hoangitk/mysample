using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetDemo
{
    public partial class Demo2 : Form
    {
        public Demo2()
        {
            InitializeComponent();

            this.Load += (s, e) =>
            {
                this.dataGridView1.DataSource = 
                    SampleData.Default.GenerateTimeSheetItemsBindingList(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(15));
            };

            this.dataGridView1.CellContentDoubleClick += (s, e) =>
            {
                var selectedRow = this.dataGridView1.Rows[e.RowIndex];

                Debug.WriteLine(selectedRow.DataBoundItem);
            };

        }
    }
}
